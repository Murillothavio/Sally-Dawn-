using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FINAL : MonoBehaviour
{
    public int QntdMemorias, MinMemoria = 1, MaxMemoria = 6, FinalNum=4;
    public bool[] Memorias = new bool[7];
    public bool MigO, Completou;
    public float TempAparecerObj, TempFraseFade;
    public GameObject[] MemoriasColetadas = new GameObject[7];
    public Transform TpFim;

    [System.Serializable]
    public class UmFim
    {
      //  [HideInInspector]
        public string name;
        public GameObject ParteUM, ParteDOIS;
        public AudioClip Musicas;
    }
    public UmFim[] Final = new UmFim[4];

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //if coolider
            VerColetados();
            Debug.Log("look at");
            GameMaster.gm.Player.GetComponent<AndarPlayer>().CanWalk = false;
            
            Invoke("LevarFim", TempAparecerObj);
        }
    }
    void VerColetados()
    {
        MigO = GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Neutro;
        Memorias[0] = GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Neutro;
        Memorias[1] = GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Alegre;
        Memorias[2] = GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Triste;
        Memorias[3] = GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Raiva;
        Memorias[4] = GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Nojo;
        Memorias[5] = GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Medo;

        QntdMemorias = 0;
        for (int i = 0; i < Memorias.Length; i++)
        {
            if (Memorias[i])
                QntdMemorias++;
            if (MemoriasColetadas[i] != null)
                MemoriasColetadas[i].SetActive(Memorias[i]);
        }

        if (Completou)
        {
            if (QntdMemorias < MinMemoria)
                FinalNum = 0;
            else if (QntdMemorias > MaxMemoria)
                FinalNum = 2;
            else FinalNum = 1;
        }
        else FinalNum = 3;
    }

    void LevarFim()
    {
        Invoke("Libera", 6 + TempFraseFade);
        if (FinalNum < Final.Length)
        {
            GameMaster.gm.FadeIN(6, TempFraseFade);

            Final[FinalNum].ParteUM.GetComponent<TextoControle>().TimeLife = TempFraseFade - 2;
            Invoke("LigaPoema", 4);

            //tp   daley
            if (TpFim != null)
            {
                Vector3 tp = TpFim.transform.position;
                tp.z = GameMaster.gm.Player.transform.position.z;
                GameMaster.gm.Player.transform.position = tp;
                Debug.Log("look de volta");
            }
        }
    }


    void LigaPoema()
    {
        Final[FinalNum].ParteUM.GetComponent<TextoControle>().Aparecer = true;
    }

    void Libera()
    {
        
        // GameMaster.gm.LiberaPlayer();
        if (FinalNum!=0)
        Debug.Log("libare");
        //Pt 2
        Final[FinalNum].ParteDOIS.GetComponent<Collider>().enabled = true;
        Debug.Log("ParteDOIS");
    }
}