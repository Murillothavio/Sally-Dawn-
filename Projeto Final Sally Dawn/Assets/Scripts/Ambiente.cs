using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambiente : MonoBehaviour
{
    public enum Fase { Neutro, Alegre, Triste, Raiva, Nojo, Medo, Etereo}
    public Fase ambiente;
    public AudioClip[] som= new AudioClip[7];
    private AudioSource AudSrc;
    private MoveConfig Atual, Pf, Af, Tf, Rf, Nf, Mf, Ef;
    private MoveConfig[] FaseConfig = new MoveConfig[7];
    public bool Manual = true;
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
    public float PullshSpeed;
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
        FasesConfig();

    }

    // Update is called once per frame
    void Update()
    {
        #region FasesConfig();
        #region Neutro
        //animaçao Idle
        Pf.MaxOcioso = MaxOcioso;
        Pf.QlAnimaOcioso = 0;
        Pf.AnimaOcioso = AnimaOcioso;
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
        //animaçao Idle
        Af.MaxOcioso = MaxOcioso;
        Af.QlAnimaOcioso = 1;
        Af.AnimaOcioso = AnimaOcioso;
        //velocidades
        Af.walkSpeed = walkSpeed;
        Af.crawlSpeed = crawlSpeed;
        Af.climbSpeed = climbSpeed;
        Af.runSpeed = runSpeed;
        Af.PullshSpeed = PullshSpeed;
        //pulo
        Af.jumpforce = jumpforce;
        Af.fallMultiplier = fallMultiplier;
        Af.lowJumpMultiplier = lowJumpMultiplier;
        #endregion
        #region Tristeza
        //animaçao Idle
        Tf.MaxOcioso = MaxOcioso;
        Tf.QlAnimaOcioso = 2;
        Tf.AnimaOcioso = AnimaOcioso;
        //velocidades
        Tf.walkSpeed = walkSpeed;
        Tf.crawlSpeed = crawlSpeed;
        Tf.climbSpeed = climbSpeed;
        Tf.runSpeed = runSpeed;
        Tf.PullshSpeed = PullshSpeed;
        //pulo
        Tf.jumpforce = jumpforce;
        Tf.fallMultiplier = fallMultiplier;
        Tf.lowJumpMultiplier = lowJumpMultiplier;
        #endregion
        #region Raiva
        //animaçao Idle
        Rf.MaxOcioso = MaxOcioso;
        Rf.QlAnimaOcioso = 3;
        Rf.AnimaOcioso = AnimaOcioso;
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
        //animaçao Idle
        Nf.MaxOcioso = MaxOcioso;
        Nf.QlAnimaOcioso = 4;
        Nf.AnimaOcioso = AnimaOcioso;
        //velocidades
        Nf.walkSpeed = walkSpeed;
        Nf.crawlSpeed = crawlSpeed;
        Nf.climbSpeed = climbSpeed;
        Nf.runSpeed = runSpeed;
        Nf.PullshSpeed = PullshSpeed;
        //pulo
        Nf.jumpforce = jumpforce;
        Nf.fallMultiplier = fallMultiplier;
        Nf.lowJumpMultiplier = lowJumpMultiplier;
        #endregion
        #region Medo
        //animaçao Idle
        Mf.MaxOcioso = MaxOcioso;
        Mf.QlAnimaOcioso = 5;
        Mf.AnimaOcioso = AnimaOcioso;
        //velocidades
        Mf.walkSpeed = walkSpeed;
        Mf.crawlSpeed = crawlSpeed;
        Mf.climbSpeed = climbSpeed;
        Mf.runSpeed = runSpeed;
        Mf.PullshSpeed = PullshSpeed;
        //pulo
        Mf.jumpforce = jumpforce;
        Mf.fallMultiplier = fallMultiplier;
        Mf.lowJumpMultiplier = lowJumpMultiplier;
        #endregion
        #region Etereo
        //animaçao Idle
        Ef.MaxOcioso = MaxOcioso;
        Ef.QlAnimaOcioso = 6;
        Ef.AnimaOcioso = AnimaOcioso;
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
        #region Not Null
        if (Atual == null)
            Atual = new MoveConfig();
        #endregion
        if (Manual)
            PublicMove();
        else
            AutomaticoMove();
        TrocaAmbiente();
        TrocaConfig();
        TrocaSom(0);
        AudSrc.enabled = true;//mantm som ligado
        GetComponent<AndarPlayer>().SetConfigFase(Atual);

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
    void TrocaAmbiente()
    {
        if (Input.GetKeyDown(KeyCode.O))
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
            Debug.Log("oi");
        }

    }
    void TrocaConfig()
    {
        if (ambiente == Fase.Neutro)
            Atual = Pf;
        else if (ambiente == Fase.Alegre)
            Atual = Af;
        else if (ambiente == Fase.Triste)
            Atual = Tf;
        else if (ambiente == Fase.Raiva)
            Atual = Rf;
        else if (ambiente == Fase.Nojo)
            Atual = Nf;
        else if (ambiente == Fase.Medo)
            Atual = Mf;
        else if (ambiente == Fase.Etereo)
            Atual = Ef;
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
    void FasesConfig()
    {
        
    }
}
