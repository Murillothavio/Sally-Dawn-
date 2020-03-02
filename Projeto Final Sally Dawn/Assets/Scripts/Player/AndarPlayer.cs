using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controles))]
public class AndarPlayer : MonoBehaviour
{
    public Transform SaveTarget;
    private Vector3 SafeZonePosition;
    private bool Kjump, Kbomba, Kinteragir, Kbaixo;
    private bool pular, Kagarrar, Ksegurar;

    [HideInInspector]
    public Controles cntr;

    private bool Zona_agarrar, Zona_segurar, Zona_interagir, Zona_morrer;
    public enum Zonas { Free, Agarrar, segurar, interagir, morrer, ItsSafe}
    public enum StateMachine { Walk, Agachado, Empurrando, Escalando, Pulando, Dancando, Caindo, Ocioso}
    [HideInInspector]
    public bool CameraPressa;
    [HideInInspector]
    public GameObject NewFocus;

    public Zonas OndeTo;
    public StateMachine stateAnimacao;
    [HideInInspector] public float TempoOcioso, moveSpeed;
    public bool Segurando, Escalando, Caindo;//*
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public GameObject ActiveSkin;
    private GameObject caixote;
    [HideInInspector]
    public Animator Acao;
    [Range(0, 2)]
    public float DeathDelay = .2f;
    private float CurrentDeathDelay = 0;
    #region Ground
    public bool isGrounded;
    private Vector3 GroundSize = new Vector3(1.16f, 1.5f, 0);
    private Vector3 GroundCenter = new Vector3(0, .3f, 0);
    #endregion
    [Range(1,10)] public float AjustDez;
    public float horizontal, Arrastar, vertical;
    public LayerMask mask;
    private const int MaxJump = 1;
    private int currentJump = 0;
    private float DeltaY;

    [HideInInspector]
    public MoveConfig AtualConfig = new MoveConfig();

    #region Collider
    [HideInInspector]
    public CapsuleCollider Corpo;
    private float HeightCollider, CenterCollider, RadiusCollider;
    private float HeightColliderCrawl = 2.13f;
    private float CenterColliderCrawl = 0.71f;
    private float RadiusColliderCrawl = 1;
    [Range(0, 5)]
    public float CurrentCollider = .5f;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Acao = ActiveSkin.GetComponent<Animator>();
        Corpo = GetComponent<CapsuleCollider>();
        cntr = GetComponent<Controles>();
        HeightCollider = Corpo.height;
        CenterCollider = Corpo.center.y;
        RadiusCollider = Corpo.radius;
    }
    private void FixedUpdate()
    {
        
        if (isGrounded && OndeTo==Zonas.ItsSafe) 
            SafeZonePosition = transform.position;
        SaveTarget.position = SafeZonePosition;

        CameraPressa = false; 
        OndeTo = Zonas.Free;
        Zona_agarrar = false;
        Zona_interagir = false;
        Zona_segurar = false;
    }
    public void SetConfigFase(MoveConfig config)
    {
        AtualConfig = config;
        if (AtualConfig.ModeloName!=null)
            ActiveSkin = (AtualConfig.ModeloName);
        if (ActiveSkin==null)
            ActiveSkin = GameObject.Find("SD@Neutro");
        Acao = ActiveSkin.GetComponent<Animator>();
    }
    void Update()
    {
        Atualiza();
        Estados();
        ColliderTamanho();
        Andar();
    }
    void Atualiza()
    {
        horizontal = cntr.GetPressAxis(cntr.ControleAtual.HORIZONTAL_AXIS);
        vertical = cntr.GetPressAxis(cntr.ControleAtual.VERTICAL_AXIS);

        for (int i = 0; i < cntr.Acoes.Length; i++)
        {
            bool ItsPress = cntr.GetPressButton(cntr.Acoes[i]);
            switch (cntr.Acoes[i])
            {
                case "AGACHAR_BUTTON":
                    Kbaixo = ItsPress;
                    break;
                case "AGARRAR_BUTTON":
                    Kagarrar = ItsPress;
                    break;
                case "BOMBA_BUTTON":
                    Kbomba = ItsPress;
                    break;
                case "INTERAGIR_BUTTON":
                    Kinteragir = ItsPress;
                    break;
                case "PULAR_BUTTON":
                    Kjump = ItsPress;
                    break;
                case "SEGURAR_BUTTON":
                    Ksegurar = ItsPress;
                    break;
            }
        }
    }
    void Estados()
    {
        if ((stateAnimacao == StateMachine.Walk || stateAnimacao == StateMachine.Ocioso)
            && (vertical == 0 && horizontal == 0))
            TempoOcioso += Time.deltaTime;
        else
            TempoOcioso = 0;
        if (TempoOcioso > AtualConfig.MaxOcioso + AtualConfig.AnimaOcioso)
            TempoOcioso = 0;

        Segurando = (OndeTo == Zonas.segurar && Ksegurar);
        Escalando = (OndeTo == Zonas.Agarrar && Kagarrar);
        if (OndeTo == Zonas.segurar && Ksegurar)
            stateAnimacao = StateMachine.Empurrando;
        if (OndeTo == Zonas.Agarrar && Kagarrar)
            stateAnimacao = StateMachine.Escalando;

        if (OndeTo == Zonas.segurar && Ksegurar)
            stateAnimacao = StateMachine.Empurrando;
        else if (OndeTo == Zonas.Agarrar && Kagarrar)
            stateAnimacao = StateMachine.Escalando;
        else if (Kbaixo)
            stateAnimacao = StateMachine.Agachado;
       else
            stateAnimacao = StateMachine.Walk;

        if (stateAnimacao == StateMachine.Walk) 
            if (TempoOcioso > AtualConfig.MaxOcioso)
                stateAnimacao = StateMachine.Ocioso;

        if (!Segurando && (horizontal < -0.5f || horizontal > .5f))
            Arrastar = horizontal;
    }
    void ColliderTamanho()
    {
        if (stateAnimacao==StateMachine.Agachado)
        {
            Corpo.radius = Mathf.MoveTowards(Corpo.radius, RadiusColliderCrawl, CurrentCollider*2);
            Corpo.center = new Vector3(Corpo.center.x, Mathf.MoveTowards(Corpo.center.y, CenterColliderCrawl, CurrentCollider), 0);
            Corpo.height = Mathf.MoveTowards(Corpo.height, HeightColliderCrawl, CurrentCollider/2);
        }
        else
        {
            Corpo.radius = RadiusCollider;
            Corpo.center = new Vector3(Corpo.center.x, CenterCollider, 0);
            Corpo.height = HeightCollider;
        }
    }
    void Andar()
    {
        if (stateAnimacao == StateMachine.Escalando)
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, AtualConfig.climbSpeed, AtualConfig.currentSpeed);

            Vector3 v = Vector3.up * vertical * moveSpeed * Time.deltaTime * (Mathf.Pow(AjustDez, 2));
            //   v.y = rb.velocity.y;
            rb.velocity = v;

            Debug.Log(moveSpeed);

            Vector3 targetOlhar = transform.position;
            targetOlhar.x += horizontal * moveSpeed;
            transform.LookAt(targetOlhar);

        }
        else if (stateAnimacao == StateMachine.Empurrando) 
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, AtualConfig.PullshSpeed, AtualConfig.currentSpeed * 3.5f);
            Vector3 v = Vector3.right * horizontal * moveSpeed;
            v.y = rb.velocity.y;
            rb.velocity = v;
            caixote.GetComponent<Rigidbody>().velocity = v;
        }
        else if (OndeTo == Zonas.morrer)
        {
            CurrentDeathDelay += Time.deltaTime;
            if (CurrentDeathDelay > DeathDelay)
            {
                transform.position = SafeZonePosition;
                rb.velocity = Vector3.zero;
                CurrentDeathDelay = 0;
            }

        }
        else
        {
            #region andar
            if (horizontal != 0 && !Kbaixo)
                moveSpeed = Mathf.MoveTowards(moveSpeed, AtualConfig.runSpeed, AtualConfig.currentSpeed);
            else if (Kbaixo)
                moveSpeed = Mathf.MoveTowards(moveSpeed, AtualConfig.crawlSpeed, AtualConfig.currentSpeed);
            else
                moveSpeed = Mathf.MoveTowards(moveSpeed, AtualConfig.walkSpeed, AtualConfig.currentSpeed * 3.5f);

            Vector3 v = Vector3.right * horizontal * moveSpeed * Time.deltaTime *  (Mathf.Pow(AjustDez,2));
            v.y = rb.velocity.y;
            if (!Caindo)
                rb.velocity = v;



            Vector3 targetOlhar = transform.position;
            targetOlhar.x += horizontal * moveSpeed;
            transform.LookAt(targetOlhar);
            //Debug.Log(Virar == targetOlhar);
            //if (horizontal != 0)
            //    Acao.SetBool("Virar", Virar == targetOlhar);
            //Virar = transform.position;
            //Virar.x += horizontal * moveSpeed;

            #endregion
        }
        #region Pulo
        var vel = rb.velocity;
        //Collider[] Grounds = Physics.OverlapBox(GroundCenter, GroundSize / 2, Quaternion.identity, mask);
        //isGrounded = Grounds != nulll;
        Vector3 origem = transform.position + GroundCenter;
        isGrounded = Physics.Raycast(origem, Vector3.down, GroundSize.y, mask);
        if (stateAnimacao == StateMachine.Walk || stateAnimacao == StateMachine.Ocioso || stateAnimacao == StateMachine.Escalando)
        {
            if (isGrounded) currentJump = 0;
            if (Kjump && !Kbaixo && (isGrounded || MaxJump > currentJump))
            {
                pular = true;
                currentJump++;
            }
            else
                pular = false;
            if (pular)
            {
                stateAnimacao = StateMachine.Pulando;
                //      moveSpeed = AtualConfig.walkSpeed;
                vel.y = AtualConfig.jumpforce;
                rb.velocity = vel;
            }
        }
        if (vel.y < 0)
        {
            //  Debug.Log("funciona esse caralho");
            vel.y += Physics.gravity.y * (AtualConfig.fallMultiplier) * Time.deltaTime;
        }
        else if (vel.y > 0 && !Input.GetButton("Jump"))
        {
            //   Debug.Log("entai vai tomar no cu");
            vel.y += Physics.gravity.y * (AtualConfig.lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //  vel.y += Physics.gravity.y / 10;
        Caindo = (vel.y < 0) && !isGrounded;
        if (Caindo)
            stateAnimacao = StateMachine.Caindo;
        rb.velocity = vel;
        // if (!isGrounded)
        // {
        // Debug.Log("-------------");
        //     Debug.Log( (AtualConfig.fallMultiplier ));
        // Debug.Log((AtualConfig.lowJumpMultiplier - 1));
        // Debug.Log(DeltaY - vel.y);
        ////     Debug.Log(Physics.gravity);
        // }
        DeltaY = vel.y;

        #endregion

        SetAnimacoes();

    }
    void SetAnimacoes()
    {
        Acao.SetBool("Agachado", Kbaixo);
        Acao.SetFloat("Queda", rb.velocity.y);
        if (pular)
            Acao.SetTrigger("Jump");
        Acao.SetBool("Pular", pular);
        Acao.SetBool("Segura", stateAnimacao == StateMachine.Empurrando);// Segurando);
        Acao.SetBool("Agarra", (stateAnimacao == StateMachine.Escalando));// Escalando);
        Acao.SetBool("Dance", stateAnimacao == StateMachine.Ocioso);
        Acao.SetFloat("Dancas", AtualConfig.QlAnimaOcioso);
        Acao.SetFloat("Wspeed", horizontal * horizontal * moveSpeed / AtualConfig.runSpeed);
        Acao.SetFloat("Arrastar", horizontal * Arrastar);
        Acao.SetBool("IsGround", isGrounded);
        Acao.SetFloat("VerticalSpeed", vertical);
        float sadwalk = (AtualConfig.QlAnimaOcioso == 2) ? 1 : 0;
        Acao.SetFloat("SadWalk", sadwalk);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag== "CameraFocus")
        {
            CameraPressa = true;
            NewFocus = other.gameObject;
        }
        if (other.gameObject.tag == "Zona_agarrar")
            OndeTo = Zonas.Agarrar;
        //Zona_agarrar = true;
        if (other.gameObject.tag == "Zona_morte")
            OndeTo = Zonas.morrer;
        //Zona_morrer = true;
        if (other.gameObject.tag == "Zona_empurrar")
        {
            OndeTo = Zonas.segurar;
            Zona_segurar = true;
            caixote = other.gameObject;
        }
        if (other.gameObject.tag == "Zona_itssafe" && OndeTo==Zonas.Free)
            OndeTo = Zonas.ItsSafe;

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TODO: Prender");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("TODO: Soltar");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .5f);

        Gizmos.DrawCube(new Vector2(transform.position.x + GroundCenter.x, transform.position.y + GroundCenter.y),
            GroundSize);

        Gizmos.color = new Color(0, 0, 1, .9f);
        Gizmos.DrawCube(new Vector2(transform.position.x + GroundCenter.x, transform.position.y + GroundCenter.y),
            new Vector2(0.01f, 1f));
        Gizmos.DrawCube(new Vector2(transform.position.x + GroundCenter.x, transform.position.y + GroundCenter.y),
             new Vector2(1f, 0.01f));


    }

}
