using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FINAL : MonoBehaviour
{
    public int QntdMemorias, MinMemoria = 1, MaxMemoria = 6, FinalNum=4;
    public bool[] Memorias = new bool[7];
    public bool MigO, Completou;
    public float TempAparecerObj, TempFraseFade, TempBadEnd = 10;
    public GameObject[] MemoriasColetadas = new GameObject[7];
    public Transform TpFim;

    [System.Serializable]
    public class UmFim
    {
        [HideInInspector]
        public string name;
        public GameObject ParteUM, ParteDOIS;
        public AudioClip Musicas;
    }
    public UmFim[] Final = new UmFim[4];

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MigO = GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Neutro;
            if (MigO || Completou)
            {
                GameMaster.gm.Player.GetComponent<AndarPlayer>().CanWalk = false;
                GameMaster.gm.Player.transform.LookAt(transform.position);

                Invoke("VerColetados", TempAparecerObj / 3);
                Invoke("LevarFim", TempAparecerObj);
            }
        }
    }
    
    void VerColetados()
    {
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
            {
                MemoriasColetadas[i].SetActive(true);
                if (MemoriasColetadas[i].transform.childCount == 2)
                {
                    MemoriasColetadas[i].transform.GetChild(0).gameObject.SetActive(Memorias[i]);
                    MemoriasColetadas[i].transform.GetChild(1).gameObject.SetActive(!Memorias[i]);
                }
            }
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

        if (Final[FinalNum].Musicas != null)
        {
            GameMaster.gm.Player.GetComponent<AudioChange>().acEvento = Final[FinalNum].Musicas;
            GameMaster.gm.Player.GetComponent<AudioChange>().Trocando = true;
        }
    }

    void LevarFim()
    {
        GameMaster.gm.Cam.GetComponent<CameraFollow>().NovoAfastamento = 25;
        Invoke("Libera", 6 + TempFraseFade);
        if (FinalNum < Final.Length)
        {
            GameMaster.gm.FadeIN(6, TempFraseFade);

            Final[FinalNum].ParteUM.GetComponent<TextoControle>().TimeLife = TempFraseFade - 2;
            Invoke("LigaPoema", 4);
        }
    }

    void LigaPoema()
    {
        Final[FinalNum].ParteUM.GetComponent<TextoControle>().Aparecer = true;
        //tp   daley
        if (TpFim != null)
        {
            Vector3 tp = TpFim.transform.position;
            tp.z = GameMaster.gm.Player.transform.position.z;
            GameMaster.gm.Player.transform.position = tp;
            GameMaster.gm.Player.transform.LookAt(TpFim.transform.position);

            GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Neutro = (FinalNum == 2);//Mig-0
            GameMaster.gm.Player.GetComponent<AndarPlayer>().Acao.SetBool("Morta", (FinalNum == 0));//Matar
            GameMaster.gm.Cam.GetComponent<CameraFollow>().NovoAfastamento = 25;
        }
        if (FinalNum == 3)
            BadEnd();
    }

    void Libera()
    {
        if (FinalNum != 0)
            GameMaster.gm.LiberaPlayer();
        else
            Invoke("BadEnd", TempBadEnd);
        //Pt 2
        if (Final[FinalNum].ParteDOIS != null)
            Final[FinalNum].ParteDOIS.GetComponent<Collider>().enabled = true;
        Debug.Log("ParteDOIS");
    }
    void BadEnd()
    {
        GameMaster.gm.DoCreditos();
    }
}