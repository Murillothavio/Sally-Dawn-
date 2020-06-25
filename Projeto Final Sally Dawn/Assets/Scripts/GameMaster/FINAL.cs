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
        [HideInInspector]
        public string name;
        public GameObject ParteUM, ParteDOIS;
        public AudioClip Musicas;
    }
    public UmFim[] Final = new UmFim[4];

    void Update()
    {
        //if coolider
        VerColetados();
        LevarFim();
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
            QntdMemorias++;
            if (MemoriasColetadas != null)
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
        Debug.Log("Parar de andar // look at");
        Invoke("Libera", 6 + TempFraseFade);
        if (FinalNum < Final.Length)
        {
            GameMaster.gm.FadeIN(6, TempFraseFade);

            Final[FinalNum].ParteUM.GetComponent<TextoControle>().TimeLife = TempFraseFade - 2;
            Invoke("LigaPoema", 4);

            //tp
            Debug.Log("TO DO TP");


            //Pt 2
            Debug.Log("ParteDOIS");
        }
    }


    void LigaPoema()
    {
        Final[FinalNum].ParteUM.GetComponent<TextoControle>().Aparecer = true;
    }

    void Libera()
    {
        // GameMaster.gm.LiberaPlayer();
        Debug.Log("libare");
    }
}