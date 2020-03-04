using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CriarFases))]
[RequireComponent(typeof(AndarPlayer))]
public class Ambiente : MonoBehaviour
{
    public enum Fase { Neutro, Alegre, Triste, Raiva, Nojo, Medo, Etereo }
    public Fase ambiente;
    [HideInInspector]
    public Color CorAmbiente;
    public float NumFases;
    public AudioClip[] som = new AudioClip[7];
    private AudioSource AudSrc;
    private CriarFases cfs;
    public bool Manual = true;
    public MoveConfig Atual = new MoveConfig();
    [SerializeField]
    private MoveConfig[] ConfgFases = new MoveConfig[7];
    public GameObject[] Skins = new GameObject[7];

    void Start()
    {
        cfs = GetComponent<CriarFases>();

        AudSrc = GetComponent<AudioSource>();
        for (int i = 0; i < ConfgFases.Length; i++)
        {
            ConfgFases[i].ModeloName = Skins[i];
            ConfgFases[i].QlAnimaOcioso = i;
        }
        TrocaConfig();

    }

    void Update()
    {
        TrocaSom(0);
        #region FixedUpdate
        if (!Manual)
            Atual = ConfgFases[(int)NumFases];

        AudSrc.enabled = true;//mantm som ligado
        GetComponent<AndarPlayer>().SetConfigFase(Atual);
        if (Atual.ModeloName != null)
        {
            for (int i = 0; i < ConfgFases.Length; i++)
                if (ConfgFases[i].ModeloName != null)
                    (ConfgFases[i].ModeloName).SetActive(ConfgFases[i].ModeloName == Atual.ModeloName);
            GetComponent<Eventos>().SkinAtiva = Atual.ModeloName.transform;
        }
        #region Cor
        if (ambiente == Fase.Neutro)
            CorAmbiente = Color.white;
        else if (ambiente == Fase.Alegre)
            CorAmbiente = new Color(1, 1, 0);
        else if (ambiente == Fase.Triste)
            CorAmbiente = Color.blue;
        else if (ambiente == Fase.Raiva)
            CorAmbiente = Color.red;
        else if (ambiente == Fase.Nojo)
            CorAmbiente = Color.green;
        else if (ambiente == Fase.Medo)
            CorAmbiente = new Color(0.3f, 0, 0.6f);
        else if (ambiente == Fase.Etereo)
            CorAmbiente = Color.magenta;
        #endregion
        #endregion
    }
    public void TrocaAmbiente()
    {
        if (ambiente == Fase.Neutro)
            ambiente = Fase.Alegre;
        else if (ambiente == Fase.Alegre)
            ambiente = Fase.Triste;
        else if (ambiente == Fase.Triste)
            ambiente = Fase.Raiva;
        else if (ambiente == Fase.Raiva)
            ambiente = Fase.Nojo;
        else if (ambiente == Fase.Nojo)
            ambiente = Fase.Medo;
        else if (ambiente == Fase.Medo)
            ambiente = Fase.Etereo;
        else if (ambiente == Fase.Etereo)
            ambiente = Fase.Neutro;
        AudSrc.enabled = false;
    }
    public void TrocaConfig()
    {
        NumFases = 0;
        if (ambiente == Fase.Neutro)
            NumFases = 0;
        else if (ambiente == Fase.Alegre)
            NumFases = 1;
        else if (ambiente == Fase.Triste)
            NumFases = 2;
        else if (ambiente == Fase.Raiva)
            NumFases = 3;
        else if (ambiente == Fase.Nojo)
            NumFases = 4;
        else if (ambiente == Fase.Medo)
            NumFases = 5;
        else if (ambiente == Fase.Etereo)
            NumFases = 6;
        // return NumFases;
        Atual = ConfgFases[(int)NumFases];
        cfs.faseIndex = NumFases;
    }
    void TrocaSom(int IdSom)
    {
        if (ambiente == Fase.Neutro)
            IdSom = 0;
        else if (ambiente == Fase.Alegre)
            IdSom = 1;
        else if (ambiente == Fase.Triste)
            IdSom = 2;
        else if (ambiente == Fase.Raiva)
            IdSom = 3;
        else if (ambiente == Fase.Nojo)
            IdSom = 4;
        else if (ambiente == Fase.Medo)
            IdSom = 5;
        else if (ambiente == Fase.Etereo)
            IdSom = 6;

        AudSrc.clip = som[IdSom];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LocazicaoFase")
            ambiente = (Fase)other.gameObject.GetComponent<OndeEstaMarca>().ambiente;
        if (other.gameObject.tag == "ChangeSkin")
            TrocaConfig();

        gameObject.GetComponent<CriarFases>().Criar = (other.gameObject.tag == "CriarFase");
    }
}
