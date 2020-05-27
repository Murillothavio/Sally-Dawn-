using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eventos : MonoBehaviour
{
    [System.Serializable]
    public class PrtMigO
    {
      //  [HideInInspector]
        public string name;
        public bool IsActive;
        public GameObject Equip, System;
    }

    [System.Serializable]
    public class CineEventos
    {
        public string name;
        public GameObject ColiderEvento;
        public GameObject[] Ativars;
        public float LifeTime;

        public void Init()
        {
            if (ColiderEvento != null)
            {
                name = ColiderEvento.name;
                Ativars = new GameObject[ColiderEvento.transform.childCount];
                for (int i = 0; i < Ativars.Length; i++)
                    Ativars[i] = ColiderEvento.transform.GetChild(i).gameObject;

            }
        }
    }
    public Emocoes Memorias = new Emocoes();
    [HideInInspector]
    public Emocoes PwrUp = new Emocoes();
    public enum Fase { Neutro, Alegre, Triste, Raiva, Nojo, Medo, Etereo }
    public Fase ambiente;

    [HideInInspector] public Text txt;
    [SerializeField] private string stg;

    public Rigidbody Tronco;
    [HideInInspector]
    public Transform SkinAtiva;
   // [HideInInspector]
//    public string[] Equips;
    [System.Serializable]
    public class NomeEquipamentos
    {
        public string name, sistema;
    }
    public NomeEquipamentos[] Equipamentos = new NomeEquipamentos[6];
    public PrtMigO[] MigO = new PrtMigO[6];

    public CineEventos[] Cine;

    private void Start()
    {
        for (int i = 0; i < Cine.Length; i++)
            Cine[i].Init();

        // MigO = new PrtMigO[Equipamentos.Length];
        for (int i = 0; i < MigO.Length; i++)
            if (MigO[i] != null && Equipamentos[i] != null)
                MigO[i].name = Equipamentos[i].name;
            else
                Debug.LogError("null" + MigO[i] + " " + Equipamentos[i]);
    }
    void Update()
    {
        #region Txt
        stg = "E:";
        stg += Memorias.Neutro ? 1 : 0;
        stg += Memorias.Alegre ? 1 : 0;
        stg += Memorias.Triste ? 1 : 0;
        stg += Memorias.Raiva ? 1 : 0;
        stg += Memorias.Nojo ? 1 : 0;
        stg += Memorias.Medo ? 1 : 0;
        stg += Memorias.Etereo ? 1 : 0;
        stg += "// P: ";
        stg += PwrUp.Neutro ? 1 : 0;
        stg += PwrUp.Alegre ? 1 : 0;
        stg += PwrUp.Triste ? 1 : 0;
        stg += PwrUp.Raiva ? 1 : 0;
        stg += PwrUp.Nojo ? 1 : 0;
        stg += PwrUp.Medo ? 1 : 0;
        stg += PwrUp.Etereo ? 1 : 0;

        if (txt != null)
            txt.text = stg;
        #endregion
        if (SkinAtiva != null)
            //    if (MigO.Length == 0)
            //        MigO = new PrtMigO[Equipamentos.Length];
            //  else
            //    if(MigO!=null)
            for (int i = 0; i < Equipamentos.Length; i++)
            {
                if (MigO[i] == null || Equipamentos[i] == null)
                    Debug.LogError("null" + MigO[i] + " " + Equipamentos[i]);

                #region EditMig0
                Debug.Log(MigO[i].name);
                MigO[i].name = Equipamentos[i].name;

                MigO[i].Equip = null;
                for (int j = 0; j < SkinAtiva.childCount; j++)
                    if (SkinAtiva.GetChild(j).name == MigO[i].name)
                        MigO[i].Equip = SkinAtiva.GetChild(j).gameObject;

                MigO[i].System = null;
                for (int j = 0; j < transform.childCount; j++)
                    if (Equipamentos[i].sistema != "")
                        if (transform.GetChild(j).name == Equipamentos[i].sistema)
                            MigO[i].System = transform.GetChild(j).gameObject;
                #endregion
                #region BoolAticves
                switch (MigO[i].name)
                {
                    case "Antenas":
                        MigO[i].IsActive = PwrUp.Triste;
                        break;
                    case "Bombas":
                        MigO[i].IsActive = PwrUp.Raiva;
                        break;
                    case "Detector":
                        MigO[i].IsActive = PwrUp.Alegre;
                        break;
                    case "Lanterna":
                        MigO[i].IsActive = PwrUp.Medo;
                        break;
                    case "Jatos":
                        MigO[i].IsActive = PwrUp.Nojo;
                        break;
                    case "Migo":
                        MigO[i].IsActive = PwrUp.Neutro;
                        break;
                }
                #endregion
                #region AtivaPwrUp
                if (MigO[i].Equip != null)
                    MigO[i].Equip.SetActive(MigO[i].IsActive);

                if (MigO[i].System != null)
                    MigO[i].System.SetActive(MigO[i].IsActive);
                #endregion
            }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItsItem>() != null)
        {
            ambiente = (Fase)GetComponent<Ambiente>().ambiente;
            if (other.GetComponent<ItsItem>().typeItem == ItsItem.TypeItem.Emotion)
                Coletar(Memorias);
            else
            if (other.GetComponent<ItsItem>().typeItem == ItsItem.TypeItem.PowerUp)
                Coletar(PwrUp);
            Destroy(other.gameObject, other. GetComponent<ItsItem>().LifeTime);
        }
        if (other.gameObject.tag == "Eventos")
        {
            for (int i = 0; i < Cine.Length; i++)
                if (Cine[i].name == other.gameObject.name)
                {
                    for (int j = 0; j < Cine[i].Ativars.Length; j++)
                        Cine[i].Ativars[j].SetActive(!Cine[i].Ativars[j].active);
                    switch (Cine[i].name)
                    {
                        case "Mig0Tornado":
                            if (Cine[i].Ativars[0].name == "Tornados")
                                Cine[i].Ativars[0].GetComponent<Animator>().speed = 3 / Cine[i].LifeTime;
                            else Debug.LogError("animator tornado errado");
                            GetComponent<AndarPlayer>().CanWalk = false;
                            Invoke("ColocarMigO", Cine[i].LifeTime / 2);
                            Invoke("FimTornado", Cine[i].LifeTime);
                            break;
                        case "DerrubarTronco":
                            Tronco.constraints = RigidbodyConstraints.None;
                            break;
                    }
                }
        }
    }

    private void ColocarMigO()
    {
        Debug.Log("Mig-0");
        GetComponent<Ambiente>().TrocaAmbiente();
        GetComponent<Ambiente>().TrocaConfig();

    }
    private void FimTornado()
    {
        GetComponent<AndarPlayer>().CanWalk = true;

    }
    private void Coletar(Emocoes eme)
    {

        if (ambiente == Fase.Neutro)
            eme.Neutro = true;
        else if (ambiente == Fase.Alegre)
            eme.Alegre = true;
        else if (ambiente == Fase.Triste)
            eme.Triste = true;
        else if (ambiente == Fase.Raiva)
            eme.Raiva = true;
        else if (ambiente == Fase.Nojo)
            eme.Nojo = true;
        else if (ambiente == Fase.Medo)
            eme.Medo = true;
        else if (ambiente == Fase.Etereo)
            eme.Etereo = true;
    }
}
