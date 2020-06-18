using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controles))]
public class AndarPlayer : MonoBehaviour
{
    public Transform SaveTarget;
    private Vector3 SafeZonePosition;
    [HideInInspector]
    public bool Kjump, Kbomba, Kinteragir, Kbaixo;
    private bool pular, Kagarrar, Ksegurar;

    [HideInInspector]
    public Controles cntr;

    private float TempQueda;
    public float AttPSeg = 10;

    private bool Zona_agarrar, Zona_segurar, Zona_interagir, Zona_morrer;
    public enum Zonas { Free, Agarrar, segurar, interagir, morrer, ItsSafe}
    public enum StateMachine { None, Walk, Agachado, Empurrando, Escalando, Pulando, Dancando, Caindo, Ocioso}
    public bool CanWalk;
    [HideInInspector]
    public bool CameraPressa;
    [HideInInspector]
    public GameObject NewFocus;

    public Zonas OndeTo;
    public StateMachine stateAnimacao;
    [HideInInspector] public float TempoOcioso;

    [Header("Velocidade Atual e Velocidade Animacao")]
    [Range(0, 30)] public float moveSpeed;
    [Range(0, 2)] public float SpeedAnimation;
    [Header("Peso/Correcao Time.DeltaTime")]
    [Range(10, 40)] public float PesoSpAnimation;
    [Range(1, 10)] public float AjustDez;



    public bool Segurando, Escalando, Caindo;//*


    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public GameObject ActiveSkin;
    public GameObject caixote;
    [HideInInspector]
    public Animator Acao;
    [Range(0, 2)]
    public float DeathDelay = .2f;
    private float CurrentDeathDelay = 0;

    #region Ground
    [Header("region Ground")]
    public bool isGrounded;
    //[HideInInspector]
    public Vector3 GroundSize = new Vector3(1.16f, 1.75f, 0);
    private Vector3 GroundCenter = new Vector3(0, .3f, 0);
    [HideInInspector] public bool StopVelocidade;
    private Vector3 Velocity;
    #endregion


    public float horizontal, Arrastar, vertical;
    public LayerMask Floor;
    private const int MaxJump = 1;
    [SerializeField]
    private int currentJump = 0;
    private float DeltaY;

    [HideInInspector]
    public MoveConfig AtualConfig = new MoveConfig();

    #region Collider
    [Header("Region Collider")]
    [HideInInspector]
    public CapsuleCollider Corpo;
    private BoxCollider CollidAgachado; 
    private float HeightCollider, CenterCollider, RadiusCollider;
    private float HeightColliderCrawl = 2.13f;
    private float CenterColliderCrawl = 0.71f;
    private float RadiusColliderCrawl = 1;
    [Range(0, 5)]
    public float CurrentCollider = .5f;
    #endregion

    [Header("Corda")]
    public GameObject CentroCorda;
    public float Angulo, Distancia;
    public Vector3 Coeficiente, DeltaPosi, CorrecaoPosi;
    public bool Balancando, Torto;
   
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Acao = ActiveSkin.GetComponent<Animator>();
        Corpo = GetComponent<CapsuleCollider>();
        CollidAgachado = GetComponent<BoxCollider>();
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

        //CameraPressa = false; 
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
        if (!GameMaster.gm.PAUSADO)
            Atualiza();
        else    DesAtualiza();

        Estados();
        ColliderTamanho();
        CalcularAngulo();

        if (CanWalk)
            Andar();
        else
            rb.velocity = Vector3.zero;
        DeltaMovimentaçao();
        SetAnimacoes();

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
    void DesAtualiza()
    {
        horizontal = 0;
        vertical = 0;

        bool ItsPress = false;
        Kbaixo = ItsPress;
        Kagarrar = ItsPress;
        Kbomba = ItsPress;
        Kinteragir = ItsPress;
        Kjump = ItsPress;
        Ksegurar = ItsPress;
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
       
        

        stateAnimacao = StateMachine.None;
        if (isGrounded && stateAnimacao != StateMachine.Pulando)
            stateAnimacao = StateMachine.Walk;

        if (stateAnimacao == StateMachine.Walk)  
            if (TempoOcioso > AtualConfig.MaxOcioso)
                stateAnimacao = StateMachine.Ocioso;

        if (Kbaixo && (stateAnimacao == StateMachine.Walk || stateAnimacao == StateMachine.Ocioso))
            stateAnimacao = StateMachine.Agachado;
        if (Caindo)
            stateAnimacao = StateMachine.Caindo;
        if (!pular)
        {
            if (OndeTo == Zonas.segurar && Ksegurar)
                stateAnimacao = StateMachine.Empurrando;
            if (OndeTo == Zonas.Agarrar && Kagarrar)
                stateAnimacao = StateMachine.Escalando;
        }
    
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
        
        Corpo.enabled = (stateAnimacao != StateMachine.Agachado);
        CollidAgachado.enabled = (stateAnimacao == StateMachine.Agachado);
    }
    void Andar()
    {
       // transform.eulerAngles = new Vector3(transform.localRotation.x, transform.localRotation.y, Angulo);
        if (stateAnimacao == StateMachine.Escalando)
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, AtualConfig.climbSpeed, AtualConfig.currentSpeed * 4);
            Vector3 k = Vector3.up;
            k = Coeficiente;
            k.y *= -1;
            Debug.Log(k);
            Vector3 v = k * vertical * moveSpeed /* Time.deltaTime * (Mathf.Pow(AjustDez, 2))*/;
            //   v.y = rb.velocity.y;
            rb.velocity = v;

            if (horizontal != 0)
            {
                if (Torto)
                {
                    Torto = false;
                    Vector3 corrigir = Vector3.zero;
                    corrigir.x += (CorrecaoPosi.x * Mathf.Sign(horizontal));
                //NOT TODAY    transform.position = corrigir;
                }
            }
            else
                Torto = true;

            Vector3 targetOlhar = transform.position;
            targetOlhar.x += horizontal * moveSpeed;
            transform.LookAt(targetOlhar);
        }
        else if (stateAnimacao == StateMachine.Empurrando) 
        {
            moveSpeed = Mathf.MoveTowards(moveSpeed, AtualConfig.PullshSpeed, AtualConfig.currentSpeed * 4f);
            Vector3 v = Vector3.right * horizontal * moveSpeed;
            v.y = rb.velocity.y;
            rb.velocity = v;
           // caixote.GetComponent<Rigidbody>().velocity = v;
        }
        else if (OndeTo == Zonas.morrer)
        {
            Morrer();
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

            Vector3 v = Vector3.right * horizontal * moveSpeed /* Time.deltaTime *  (Mathf.Pow(AjustDez,2))*/;
            v.y = rb.velocity.y;
          //  if (!Caindo && (stateAnimacao != StateMachine.Pulando && stateAnimacao != StateMachine.None))   teste pulo
                rb.velocity = v;

         //   Debug.Log((!Caindo) +" "+ (stateAnimacao != StateMachine.Pulando )+ ".");

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
        Vector3 origem = transform.position + GroundCenter, origemL = transform.position + GroundCenter, origemR = transform.position + GroundCenter;
        origemL.x -= GroundSize.x / 2;
        origemR.x += GroundSize.x / 2;

        bool[] Chaos = new bool[3];

        Chaos[0]= ((Physics.Raycast(origemL, Vector3.down, GroundSize.y, Floor)) || (stateAnimacao == StateMachine.Escalando));
        Chaos[1]= ((Physics.Raycast(origem, Vector3.down, GroundSize.y, Floor)) || (stateAnimacao == StateMachine.Escalando));
        Chaos[2]= ((Physics.Raycast(origemR, Vector3.down, GroundSize.y, Floor)) || (stateAnimacao == StateMachine.Escalando));

        isGrounded = (Chaos[0] || Chaos[1] || Chaos[2]);
        if (stateAnimacao == StateMachine.Walk || stateAnimacao == StateMachine.Ocioso || stateAnimacao == StateMachine.Escalando
            || stateAnimacao == StateMachine.Pulando || stateAnimacao == StateMachine.Caindo || stateAnimacao==StateMachine.None)
        {
            if (isGrounded) currentJump = 0;

            #region Jato
            bool TemJato = GetComponent<Eventos>().PwrUp.Nojo;
            float Extra;

            if (TemJato) Extra = 1;
            else Extra = 0;

            #endregion

            if (Kjump && !Kbaixo && (isGrounded || (MaxJump + Extra) > currentJump))
            {
                pular = true;
                currentJump++;
            }
            else
                pular = false;
            if (pular)
            {
                if (stateAnimacao == StateMachine.Escalando)
                {
                    Vector3 v = Vector3.right * horizontal * moveSpeed * Time.deltaTime * (Mathf.Pow(AjustDez, 2));
                    vel.x += v.x;
                }
                stateAnimacao = StateMachine.Pulando;
                //      moveSpeed = AtualConfig.walkSpeed;
                vel.y = AtualConfig.jumpforce;
                rb.velocity = vel;

                #region Efeito
                if (MaxJump >= currentJump)
                    GetComponent<EfeitosSally>().EfeitoPular();
                else
                    GetComponent<EfeitosSally>().EfeitoJato();
                #endregion
            }
        }
        Caindo = (vel.y < 0) && !isGrounded;
        if (Caindo)
            stateAnimacao = StateMachine.Caindo;
        if (vel.y < 0)
        {
            if (Caindo)
            {
                TempQueda -= Time.deltaTime;
                if (TempQueda < 0) {
                    vel.y += Physics.gravity.y * (AtualConfig.fallMultiplier / 4);
                    Debug.Log(vel.y);
                    TempQueda = 1 / AttPSeg;
                }
            }
        }
        else if (vel.y > 0 && !Input.GetButton("Jump"))
        {
 //              Debug.Log("entai vai tomar no cu");
            vel.y += Physics.gravity.y * (AtualConfig.lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //  vel.y += Physics.gravity.y / 10;
       
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

        if (!isGrounded)
            StopVelocidade = true;
        else if (StopVelocidade)
        {
            StopVelocidade = false;
            rb.velocity = Vector3.zero;
        }
        Velocity = rb.velocity;

    }
    public void Morrer()
    {
        CurrentDeathDelay += Time.deltaTime;
        if (CurrentDeathDelay > DeathDelay)
        {
            transform.position = SafeZonePosition;
            rb.velocity = Vector3.zero;
            CurrentDeathDelay = 0;
        }
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
       // SpeedAnimation = ((moveSpeed - AtualConfig.walkSpeed) / (AtualConfig.runSpeed)) + 1;
        SpeedAnimation = ((moveSpeed - 10) / PesoSpAnimation) + 1;
        Acao.speed = SpeedAnimation;
    }
    void DeltaMovimentaçao()
    {

        if ((stateAnimacao==StateMachine.Escalando && Distancia == 0) || vertical != 0)
            CalcDistancia();
  
        if (stateAnimacao == StateMachine.Escalando)
        {
            DeltaPosi = Coeficiente * Distancia;
            if (Balancando)
                transform.position = DeltaPosi + CentroCorda.transform.position;
        }
     
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Zona_agarrar")
            OndeTo = Zonas.Agarrar;
        if (other.gameObject.tag == "Zona_morte")
            OndeTo = Zonas.morrer;
        if (other.gameObject.tag == "Zona_empurrar")
        {
            OndeTo = Zonas.segurar;
            Zona_segurar = true;
            caixote = other.gameObject;
        }
        if (other.gameObject.tag == "Zona_itssafe" && OndeTo==Zonas.Free)
            OndeTo = Zonas.ItsSafe;

    }
    void CalcularAngulo()
    {
        Balancar b = null;
        if (CentroCorda != null)
            b = CentroCorda.GetComponent<Balancar>();
        else Distancia = 0;
        Balancando = (b != null);
        if (Balancando)
            Angulo = b.CurrentAng;
        else Angulo = 0;

        float Ang = Mathf.Deg2Rad * Angulo;
        Coeficiente = new Vector3(Mathf.Sin(Ang), -1 * Mathf.Cos(Ang), 0);
    }
    void CalcDistancia()
    {
        if (CentroCorda != null)
        {
            Vector3 Delta = transform.position - CentroCorda.transform.position;
            Debug.Log("calculou");
            Distancia = Mathf.Sqrt((Delta.x * Delta.x) + (Delta.y * Delta.y));
        }
        else Debug.Log("SemCorda");
    }
    private void OnTriggerEnter(Collider other)
    {
     //   Debug.Log("TODO: Prender");
        if (other.gameObject.tag == "CameraFocus")
        {
            CameraPressa = true;
            NewFocus = other.gameObject.transform.GetChild(0).gameObject;
        }
        if (other.gameObject.tag == "Zona_agarrar")
        {
            CentroCorda = other.gameObject.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
    //    Debug.Log("TODO: Soltar");
        if (other.gameObject.tag == "CameraFocus")
        {
            CameraPressa = false;
        }
        if (other.gameObject.tag == "Zona_agarrar")
        {
            CentroCorda = null;
        }
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, .85f);

        Gizmos.DrawCube(new Vector2(transform.position.x + GroundCenter.x, transform.position.y + GroundCenter.y),
            GroundSize);

        Gizmos.color = new Color(1, 1, 0, 1);
        Gizmos.DrawSphere(CorrecaoPosi+transform.position, 1);

        Gizmos.color = new Color(0, 0, 1, .9f);
        Gizmos.DrawCube(new Vector2(transform.position.x + GroundCenter.x, transform.position.y + GroundCenter.y),
            new Vector2(0.01f, 1f));
        Gizmos.DrawCube(new Vector2(transform.position.x + GroundCenter.x, transform.position.y + GroundCenter.y),
             new Vector2(1f, 0.01f));
    }
}
