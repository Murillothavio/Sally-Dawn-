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
    private MoveConfig Atual = new MoveConfig(), Pf = new MoveConfig(), Af = new MoveConfig(), Tf = new MoveConfig(), Rf = new MoveConfig(), Nf = new MoveConfig(), Mf = new MoveConfig(), Ef = new MoveConfig();
    public GameObject[] Skins = new GameObject[7];
    public bool Manual = true;
    public bool notnull;
    #region config
    public float MaxOcioso = 10;
    [Range(0, 6)]
    public float QlAnimaOcioso;
    public float AnimaOcioso = 5;
    [Range(5, 15)]
    public float jumpforce = 10;
    [Range(5, 15)]
    public float walkSpeed = 10;
    [Range(5, 10)]
    public float crawlSpeed = 7;
    [Range(5, 10)]
    public float climbSpeed = 8;
    [Range(10, 30)]
    public float runSpeed;
    [Range(2, 10)]
    public float PullshSpeed = 5;
    [Range(0, 2)]
    public float currentSpeed = .5f;
    [Range(1, 7)]
    public float fallMultiplier = 2.5f;
    [Range(1, 7)]
    public float lowJumpMultiplier = 2f;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        cfs = GetComponent<CriarFases>();
        Atual = new MoveConfig();
        Pf = new MoveConfig();
        Af = new MoveConfig();
        Tf = new MoveConfig();
        Rf = new MoveConfig();
        Nf = new MoveConfig();
        Mf = new MoveConfig();
        Ef = new MoveConfig();
        AudSrc = GetComponent<AudioSource>();
        PublicMove();

    }
    void NotNull()
    {
        if (notnull)
        {
            notnull = false;
            Atual = new MoveConfig();
            Pf = new MoveConfig();
            Af = new MoveConfig();
            Tf = new MoveConfig();
            Rf = new MoveConfig();
            Nf = new MoveConfig();
            Mf = new MoveConfig();
            Ef = new MoveConfig();
        }
        //if (Pf.ModeloName == null)
        //    Pf = new MoveConfig();
        //if (Af.ModeloName == null)
        //    Af = new MoveConfig();
        //if (Tf.ModeloName == null)
        //    Tf = new MoveConfig();
        //if (Rf.ModeloName == null)
        //    Rf = new MoveConfig();
        //if (Nf.ModeloName == null)
        //    Nf = new MoveConfig();
        //if (Mf.ModeloName == null)
        //    Mf = new MoveConfig();
        //if (Ef.ModeloName == null)
        //    Ef = new MoveConfig();
    }
    // Update is called once per frame
    void Update()
    {
        NotNull();
        #region Not Null
        if (Atual == null)
            Atual = new MoveConfig();
        #endregion
        if (Manual)
            PublicMove();
        else
        {
            AutomaticoMove();
            #region FasesConfig();
            #region Neutro
            Pf.ModeloName = Skins[0];
            //animaçao Idle
            Pf.MaxOcioso = 15;
            Pf.QlAnimaOcioso = 0;
            Pf.AnimaOcioso = 12;
            //velocidades
            Pf.walkSpeed = walkSpeed;
            Pf.crawlSpeed = crawlSpeed;
            Pf.climbSpeed = climbSpeed;
            Pf.runSpeed = runSpeed;
            Pf.PullshSpeed = PullshSpeed;
            //pulo
            Pf.jumpforce = jumpforce;
            Pf.fallMultiplier = fallMultiplier;
            Pf.lowJumpMultiplier = lowJumpMultiplier;
            #endregion
            #region Alegre
            Af.ModeloName = Skins[1];
            //animaçao Idle
            Af.MaxOcioso = 10;
            Af.QlAnimaOcioso = 1;
            Af.AnimaOcioso = 30;
            //velocidades
            Af.walkSpeed = 10;
            Af.crawlSpeed = 8;
            Af.climbSpeed = 10;
            Af.runSpeed = 25;
            Af.PullshSpeed = PullshSpeed;
            //pulo
            Af.jumpforce = jumpforce;
            Af.fallMultiplier = fallMultiplier;
            Af.lowJumpMultiplier = lowJumpMultiplier;
            #endregion
            #region Tristeza
            Tf.ModeloName = Skins[2];
            //animaçao Idle
            Tf.MaxOcioso = 15;
            Tf.QlAnimaOcioso = 2;
            Tf.AnimaOcioso = 6.5f;
            //velocidades
            Tf.walkSpeed = 7;
            Tf.crawlSpeed = 5;
            Tf.climbSpeed = 5;
            Tf.runSpeed = 15;
            Tf.PullshSpeed = PullshSpeed;
            //pulo
            Tf.jumpforce = jumpforce;
            Tf.fallMultiplier = fallMultiplier;
            Tf.lowJumpMultiplier = lowJumpMultiplier;
            #endregion
            #region Raiva
            Rf.ModeloName = Skins[3];
            //animaçao Idle
            Rf.MaxOcioso = 15;
            Rf.QlAnimaOcioso = 3;
            Rf.AnimaOcioso = 20;
            //velocidades
            Rf.walkSpeed = walkSpeed;
            Rf.crawlSpeed = crawlSpeed;
            Rf.climbSpeed = climbSpeed;
            Rf.runSpeed = runSpeed;
            Rf.PullshSpeed = PullshSpeed;
            //pulo
            Rf.jumpforce = jumpforce;
            Rf.fallMultiplier = fallMultiplier;
            Rf.lowJumpMultiplier = lowJumpMultiplier;
            #endregion
            #region Nojo
            Nf.ModeloName = Skins[4];
            //animaçao Idle
            Nf.MaxOcioso = 7;
            Nf.QlAnimaOcioso = 4;
            Nf.AnimaOcioso = 4;
            //velocidades
            Nf.walkSpeed = walkSpeed;
            Nf.crawlSpeed = 5;
            Nf.climbSpeed = 7;
            Nf.runSpeed = runSpeed;
            Nf.PullshSpeed = PullshSpeed;
            //pulo
            Nf.jumpforce = jumpforce;
            Nf.fallMultiplier = fallMultiplier;
            Nf.lowJumpMultiplier = lowJumpMultiplier;
            #endregion
            #region Medo
            Mf.ModeloName = Skins[5];
            //animaçao Idle
            Mf.MaxOcioso = 5;
            Mf.QlAnimaOcioso = 5;
            Mf.AnimaOcioso = 4.5f;
            //velocidades
            Mf.walkSpeed = 7;
            Mf.crawlSpeed = crawlSpeed;
            Mf.climbSpeed = climbSpeed;
            Mf.runSpeed = 30;
            Mf.PullshSpeed = PullshSpeed;
            //pulo
            Mf.jumpforce = jumpforce;
            Mf.fallMultiplier = fallMultiplier;
            Mf.lowJumpMultiplier = lowJumpMultiplier;
            #endregion
            #region Etereo
            Ef.ModeloName = Skins[6];
            //animaçao Idle
            Ef.MaxOcioso = 15;
            Ef.QlAnimaOcioso = 6;
            Ef.AnimaOcioso = 4;
            //velocidades
            Ef.walkSpeed = walkSpeed;
            Ef.crawlSpeed = crawlSpeed;
            Ef.climbSpeed = climbSpeed;
            Ef.runSpeed = runSpeed;
            Ef.PullshSpeed = PullshSpeed;
            //pulo
            Ef.jumpforce = jumpforce;
            Ef.fallMultiplier = fallMultiplier;
            Ef.lowJumpMultiplier = lowJumpMultiplier;
            #endregion
            #endregion

        }
   //     TrocaAmbiente();
    //    TrocaConfig();
        TrocaSom(0);
        #region FixedUpdate
        AudSrc.enabled = true;//mantm som ligado
        GetComponent<AndarPlayer>().SetConfigFase(Atual);
            if (Atual.ModeloName != null)
            {
                if (Pf.ModeloName != null)
                    (Pf.ModeloName).SetActive(Pf.ModeloName == Atual.ModeloName);
                if (Af.ModeloName != null)
                    (Af.ModeloName).SetActive(Af.ModeloName == Atual.ModeloName);
                if (Tf.ModeloName != null)
                    (Tf.ModeloName).SetActive(Tf.ModeloName == Atual.ModeloName);
                if (Rf.ModeloName != null)
                    (Rf.ModeloName).SetActive(Rf.ModeloName == Atual.ModeloName);
                if (Nf.ModeloName != null)
                    (Nf.ModeloName).SetActive(Nf.ModeloName == Atual.ModeloName);
                if (Mf.ModeloName != null)
                    (Mf.ModeloName).SetActive(Mf.ModeloName == Atual.ModeloName);
                if (Ef.ModeloName != null)
                    (Ef.ModeloName).SetActive(Ef.ModeloName == Atual.ModeloName);
            }
        //  GameObject.Find(Atual.ModeloName).SetActive(true);
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

    void PublicMove()
    {
        //animaçao Idle
        Atual.MaxOcioso = MaxOcioso;
        Atual.QlAnimaOcioso = (int)QlAnimaOcioso;
        Atual.AnimaOcioso = AnimaOcioso;
        //velocidades
        Atual.walkSpeed = walkSpeed;
        Atual.crawlSpeed = crawlSpeed;
        Atual.climbSpeed = climbSpeed;
        Atual.runSpeed = runSpeed;
        Atual.PullshSpeed = PullshSpeed;
        //pulo
        Atual.jumpforce = jumpforce;
        Atual.fallMultiplier = fallMultiplier;
        Atual.lowJumpMultiplier = lowJumpMultiplier;
    }
    void AutomaticoMove()
    {
        //animaçao Idle
        MaxOcioso = Atual.MaxOcioso;
        QlAnimaOcioso = (int)Atual.QlAnimaOcioso;
        AnimaOcioso = Atual.AnimaOcioso;
        //velocidades
        walkSpeed = Atual.walkSpeed;
        crawlSpeed = Atual.crawlSpeed;
        climbSpeed = Atual.climbSpeed;
        runSpeed = Atual.runSpeed;
        PullshSpeed = Atual.PullshSpeed;
        //pulo
        jumpforce = Atual.jumpforce;
        fallMultiplier = Atual.fallMultiplier;
        lowJumpMultiplier = Atual.lowJumpMultiplier;

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
        Debug.Log("AA");
        NumFases = 0;
        if (ambiente == Fase.Neutro)
        {
            Atual = Pf;
            NumFases = 0;
        }
        else if (ambiente == Fase.Alegre)
        {
            Atual = Af;
            NumFases = 1;
        }
        else if (ambiente == Fase.Triste)
        {
            Atual = Tf;
            NumFases = 2;
        }
        else if (ambiente == Fase.Raiva)
        {
            Atual = Rf;
            NumFases = 3;
        }
        else if (ambiente == Fase.Nojo)
        {
            Atual = Nf;
            NumFases = 4;
        }
        else if (ambiente == Fase.Medo)
        {
            Atual = Mf;
            NumFases = 5;
        }
        else if (ambiente == Fase.Etereo)
        {
            Atual = Ef;
            NumFases = 6;
        }
       // return NumFases;
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
        gameObject.GetComponent<CriarFases>().Criar = (other.gameObject.tag == "CriarFase");
    }
}
