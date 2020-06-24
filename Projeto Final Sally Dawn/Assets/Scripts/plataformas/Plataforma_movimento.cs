using UnityEngine;

public class Plataforma_movimento : MonoBehaviour
{
    #region Variavel
   // public bool THHHIS;
    private Rigidbody rb;
    public GameObject MeuPai;
    public float precisao = 1;
    private bool Estado = true;
    public bool Alternado = false;
    public bool TaNoPonto, Movendo;
    private Vector3 Ponto0, PontoAtual, PontoGoTo;
    private float Angle;

    [System.Serializable]
    public struct Plat
    {
        public Vector3 Direcao;
        public Vector3 PontoFinal;
        public Vector3 PontoInicial;
        public Vector3 CentroPonto;
        public float Velocidade;
        public float Raio;

        public Plat(Vector3 _Direcao, float _TamanP, float _TamanN, float _velo, float _raio)
        {
            if (_Direcao != Vector3.zero)
                _velo = Mathf.Abs(_velo);
            this.Direcao = _Direcao;
            this.PontoFinal = _TamanP * _Direcao;
            this.PontoInicial = _TamanN * _Direcao * -1;
            this.Velocidade = _velo;
            this.Raio = -_raio;
            this.CentroPonto = _raio * _Direcao;
        }
    }
    /*
    public Plat Horizontalc = new Plat(Vector3.right, 20, 10, 8, 0);
    public Plat Verticalc = new Plat(Vector3.up, 20, 10, 15, 0);
    public Plat Crescentec = new Plat(new Vector3(0, 1, 1), 20, 10, 8, 0);
    public Plat Decrescentec = new Plat(new Vector3(0, 1, -1), 20, 10, 8, 0);
    public Plat Translacaoc = new Plat(Vector3.up, 0, 0, 190, 10);
    public Plat Horarioc = new Plat(Vector3.zero, 0, 0, 8, 0);
    public Plat Anhorarioc = new Plat(Vector3.zero, 0, 0, -8, 0);*/
    //[HideInInspector]
    public Plat AaCaminho;
    //[HideInInspector]
    public Plat BbCaminho;
    //[HideInInspector]
    public Plat NwCaminho;

    public bool LookAt;
    public Plat CaminhoRed;
    public float VelociadadeRed = 8;

    public float PauseTime = 0;
    private float pausetime;
    [HideInInspector]
    public bool Parada, Espera;
    public bool Dupla = true;
    public Plat CaminhoBlue;
    public float VelociadadeBlue = 15;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Ponto0 = transform.position;
        PontoAtual = Ponto0;
        Movendo = true;

        if (transform.childCount >= 2)
        {
            CaminhoRed = GetCaminho(transform.GetChild(0).gameObject, VelociadadeRed);
            CaminhoBlue = GetCaminho(transform.GetChild(1).gameObject, VelociadadeBlue);
        }

        AaCaminho = CaminhoRed;
        if (Dupla)
            BbCaminho = CaminhoBlue;
        else
            BbCaminho = CaminhoRed;

        AaCaminho.PontoFinal += Ponto0;
        AaCaminho.PontoInicial += Ponto0;
        AaCaminho.CentroPonto += Ponto0;
        BbCaminho.PontoFinal += Ponto0;
        BbCaminho.PontoInicial += Ponto0;
        BbCaminho.CentroPonto += Ponto0;

        if (Estado)
            NwCaminho = AaCaminho;
        else
            NwCaminho = BbCaminho;
        PontoGoTo = NwCaminho.PontoFinal;
    }

    void Start()
    {
        bool tirar = GameObject.Find("Main Camera").GetComponent<Debugagem>().Desativar;
        bool destruir = GameObject.Find("Main Camera").GetComponent<Debugagem>().Deletar;
        GameObject gbRed = transform.GetChild(0).gameObject;
        GameObject gbBlue = transform.GetChild(1).gameObject;

        gbBlue.SetActive(tirar);
        gbRed.SetActive(tirar);

        if (destruir)
        {
            Destroy(gbBlue);
            Destroy(gbRed);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject addAttach = transform.GetChild(i).gameObject;
            Plataforma_Attach pa;
            if (addAttach != gbRed && addAttach != gbBlue)
                pa = addAttach.gameObject.AddComponent<Plataforma_Attach>() as Plataforma_Attach;
        }
    }
    Plat GetCaminho(GameObject gb, float _velociadade)
    {
        Vector3 PontoLeft, PontoRight, PontoDelta, PontoCentro;
        float AngDirecao, AngRaiz;
        Vector3 _direcl, _direcao;
        float _tamanhoright, _tamanholeft, _raio;
        LineRenderer lr = gb.GetComponent<LineRenderer>();

        
            PontoLeft = gb.transform.GetChild(0).transform.position - Ponto0;
            PontoRight = gb.transform.GetChild(1).transform.position - Ponto0;
            PontoCentro = gb.transform.GetChild(2).transform.position - Ponto0;
        
        

        PontoDelta = PontoRight - PontoLeft;
        float ang;
        if (PontoDelta.x != 0)
            ang = Mathf.Atan(PontoDelta.y / PontoDelta.x) * Mathf.Rad2Deg;
        else ang = 0;
        AngDirecao = Mathf.Abs(ang);

        Vector3 PClinha = new Vector3(Mathf.Abs(PontoCentro.x), Mathf.Abs(PontoCentro.y), 0);
        if (PontoDelta != Vector3.zero)
        {
            float XX;
            if (AngDirecao != 0)
                XX = (AngDirecao < 60 ? 1 : 0) * (ang / AngDirecao);
            else XX = 0;

            _direcao = new Vector3(XX, AngDirecao > 30 ? 1 : 0, 0);
        }
        else if (PontoCentro != Vector3.zero)
            _direcao = new Vector3(PClinha.y <= PClinha.x ? 1 : 0, PClinha.x <= PClinha.y ? 1 : 0, 0);
        else
            _direcao = Vector3.zero;

        AngRaiz = AngDirecao - 45;
        _direcl = new Vector3(AngRaiz < 0 ? 1 : 0, AngRaiz > 0 ? 1 : 0, 0);
        AngRaiz = Mathf.Abs(AngRaiz);

        _tamanhoright = Vector3.Dot(PontoRight, _direcl);
        _tamanholeft = Vector3.Dot(PontoLeft, _direcl);
        _raio = Vector3.Dot(_direcao, PontoCentro);

        lr.SetPosition(0, PontoLeft);
        lr.SetPosition(1, _direcao * _tamanholeft);
        lr.SetPosition(2, _direcao * _tamanhoright);
        lr.SetPosition(3, PontoRight);

        if (name== "PlataformaMovel (XXX)")
            Debug.Log(PontoCentro);
        return new Plat(_direcao, _tamanhoright, -_tamanholeft, _velociadade, _raio);
    }
    void Update()
    {
        TaNoPonto = (Mathf.Abs(PontoAtual.y - Ponto0.y) < precisao && Mathf.Abs(PontoAtual.z - Ponto0.z) <precisao && Mathf.Abs(PontoAtual.x - Ponto0.x) < precisao);
        if (TaNoPonto)
            if (Alternado)
            {
                Alternado = false;
                if (Estado) Estado = false;
                else Estado = true;
                if (Estado) NwCaminho = AaCaminho;
                else NwCaminho = BbCaminho;
                PontoGoTo = NwCaminho.PontoFinal;
                if (NwCaminho.Direcao.y != 0) Angle = Mathf.Asin(NwCaminho.Direcao.y) * Mathf.Rad2Deg;
                else if (NwCaminho.Direcao.x != 0) Angle = Mathf.Acos(NwCaminho.Direcao.x) * Mathf.Rad2Deg;
                transform.position = Ponto0;
            }

        if (Movendo)
        {
            if (NwCaminho.CentroPonto == Ponto0)
            {
                if (NwCaminho.Direcao != Vector3.zero)
                {
                    #region Andar Reto
                    if ((int)PontoAtual.z == (int)NwCaminho.PontoFinal.z
                     && (int)PontoAtual.y == (int)NwCaminho.PontoFinal.y
                     && (int)PontoAtual.x == (int)NwCaminho.PontoFinal.x)
                        PontoGoTo = NwCaminho.PontoInicial;

                    else if ((int)PontoAtual.z == (int)NwCaminho.PontoInicial.z
                          && (int)PontoAtual.y == (int)NwCaminho.PontoInicial.y
                          && (int)PontoAtual.x == (int)NwCaminho.PontoInicial.x)
                        PontoGoTo = NwCaminho.PontoFinal;

                    PontoAtual.z = Mathf.MoveTowards(PontoAtual.z, PontoGoTo.z, NwCaminho.Velocidade * Time.deltaTime);
                    PontoAtual.y = Mathf.MoveTowards(PontoAtual.y, PontoGoTo.y, NwCaminho.Velocidade * Time.deltaTime);
                    PontoAtual.x = Mathf.MoveTowards(PontoAtual.x, PontoGoTo.x, NwCaminho.Velocidade * Time.deltaTime);
                    #endregion
                }
                else
                {
                    #region Rotação
                    Angle = Quaternion.Angle(Quaternion.Euler(Vector3.zero), transform.rotation);
                    Angle = (Mathf.Abs(Mathf.Abs((Mathf.Abs(Angle - 90) - 45)) - 45)) / 4.5f;
                    Parada = (Angle < precisao);

                    if (!Parada)
                        Espera = true;
                    if (Espera && Parada)
                    {
                        pausetime += Time.deltaTime;
                        if (pausetime > PauseTime)
                        {
                            pausetime = 0;
                            Espera = false;
                        }
                    }
                    else
                        transform.Rotate(0, 0, transform.rotation.x + NwCaminho.Velocidade * Time.deltaTime);
                    #endregion
                }
            }
            else
            {
                #region Translação
                Angle += NwCaminho.Velocidade * Time.deltaTime;
                NwCaminho.Direcao.y = Mathf.Sin(Angle * Mathf.Deg2Rad);
                NwCaminho.Direcao.x = Mathf.Cos(Angle * Mathf.Deg2Rad);
                PontoAtual = NwCaminho.CentroPonto + NwCaminho.Raio * NwCaminho.Direcao;
                if (LookAt)
                    transform.RotateAround(NwCaminho.CentroPonto, Vector3.forward, NwCaminho.Velocidade * Time.deltaTime);
                #endregion
            }
        }

        transform.position = PontoAtual;
    }
}

