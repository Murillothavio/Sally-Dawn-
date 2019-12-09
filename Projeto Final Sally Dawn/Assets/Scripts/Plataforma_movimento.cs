using UnityEngine;

public class Plataforma_movimento : MonoBehaviour
{
    #region Variavel
   
    private Rigidbody rb;
    [HideInInspector]
    public GameObject MeuPai;
    public float precisao = 1;
    private bool Estado = true;
    public bool Alternado = false;
    public bool TaNoPonto, Movendo,Junto;
    public Vector3 Ponto0;
    public Vector3 PontoAtual;
    public Vector3 PontoGoTo;
    private float Angu;


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
    public Plat AaCaminho;
    public Plat BbCaminho;
    public Plat NwCaminho;

    public bool LookAt;
    public Plat CaminhoRed;
    public Vector3 DirecaoRed = Vector3.right;
    public float TamanhoRightRed = 20, TamanhoLeftRed = 10, VelociadadeRed = 8, RaioRed = 0;

    public bool Dupla = true;
    public Plat CaminhoBlue;
    public Vector3 DirecaoBlue = Vector3.up;
    public float TamanhoRightBlue = 20, TamanhoLeftBlue = 10, VelociadadeBlue = 15, RaioBlue = 0;

    public Vector3 ddddirecao , cccccccen, ddddddta;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Ponto0 = transform.position;
        PontoAtual = Ponto0;
        Movendo = true;
        Junto = true;


     //   CaminhoRed = new Plat(DirecaoRed, TamanhoRightRed, TamanhoLeftRed, VelociadadeRed, RaioRed);
     //   CaminhoBlue = new Plat(DirecaoBlue, TamanhoRightBlue, TamanhoLeftBlue, VelociadadeBlue, RaioBlue);


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

        DirecaoRed = AaCaminho.Direcao;
        TamanhoRightRed = Vector3.Dot(DirecaoRed, AaCaminho.PontoInicial);
        TamanhoLeftRed = Vector3.Dot(DirecaoRed, AaCaminho.PontoFinal);
        RaioRed = AaCaminho.Raio;

        DirecaoBlue = BbCaminho.Direcao;
        TamanhoRightBlue = Vector3.Dot(DirecaoBlue, BbCaminho.PontoInicial);
        TamanhoLeftBlue = Vector3.Dot(DirecaoBlue, BbCaminho.PontoFinal);
        RaioBlue = BbCaminho.Raio;

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

        if (NwCaminho.Direcao.y != 0) Angu = Mathf.Asin(NwCaminho.Direcao.y) * Mathf.Rad2Deg;
        else if (NwCaminho.Direcao.x != 0) Angu = Mathf.Acos(NwCaminho.Direcao.x) * Mathf.Rad2Deg;
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
        lr.SetPosition(0, PontoLeft);
        lr.SetPosition(3, PontoRight);

        PontoDelta = PontoRight - PontoLeft;
        ddddddta = PontoDelta;
        float ang = Mathf.Atan(PontoDelta.y / PontoDelta.x) * Mathf.Rad2Deg;
        AngDirecao = Mathf.Abs(ang);

        Vector3 PClinha = new Vector3(Mathf.Abs(PontoCentro.x), Mathf.Abs(PontoCentro.y), 0);
        if (PontoDelta != Vector3.zero)
            _direcao = new Vector3((AngDirecao < 60 ? 1 : 0) * (ang / AngDirecao), AngDirecao > 30 ? 1 : 0, 0);
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
        ddddirecao = _direcao;
        cccccccen = PontoCentro;


        lr.SetPosition(0, PontoLeft);
        lr.SetPosition(1, _direcao * _tamanholeft);
        lr.SetPosition(2, _direcao * _tamanhoright);
        lr.SetPosition(3, PontoRight);
        //   _tamanhoright *= Mathf.Sqrt(1 + (AngRaiz < 15 ? 1 : 0));
        //   _tamanholeft *= Mathf.Sqrt(1 + (AngRaiz < 15 ? 1 : 0));


        //DirecaoInicial = _direcao;
        //TamanhoRightInicial = _tamanhoright;
        //TamanhoLeftInicial = _tamanholeft;
        //VelociadadeInicial = _velociadade;
        //RaioInicial = _raio;


        //   new Plat(DirecaoInicial, TamanhoRightInicial, TamanhoLeftInicial, VelociadadeInicial, RaioInicial);
        /*
         public Vector3 PontoLeft, PontoRight, PontoDelta, PontoCentro;
       public float AngDirecao, AngRaiz;
       public Vector3 _direcl, _direcao, _ndirec;
       public    float _tamanhoright, _tamanholeft, _raio;

            */
        return new Plat(_direcao, _tamanhoright, -_tamanholeft, _velociadade, _raio);

    }
    void Update()
    {
        TaNoPonto = (Mathf.Abs(PontoAtual.y - Ponto0.y) < precisao && Mathf.Abs(PontoAtual.z - Ponto0.z) <precisao && Mathf.Abs(PontoAtual.x - Ponto0.x) < precisao);
        if (TaNoPonto)
        {
            //if ((int)PontoAtual.y == (int)Ponto0.y && (int)PontoAtual.z == (int)Ponto0.z && (int)PontoAtual.x == (int)Ponto0.x)
            if (Alternado)
            {
                Alternado = false;
                if (Estado) Estado = false;
                else Estado = true;
                if (Estado) NwCaminho = AaCaminho;
                else NwCaminho = BbCaminho;
                PontoGoTo = NwCaminho.PontoFinal;
                if (NwCaminho.Direcao.y != 0) Angu = Mathf.Asin(NwCaminho.Direcao.y) * Mathf.Rad2Deg;
                else if (NwCaminho.Direcao.x != 0) Angu = Mathf.Acos(NwCaminho.Direcao.x) * Mathf.Rad2Deg;
                transform.position = Ponto0;
            }
            //if (MeuPai != null && Junto)
            //    MeuPai.GetComponent<Plataforma_quadupla>().AtualizaBool(gameObject);
        }

        if (Movendo)
        {
            Junto = true;
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
                    transform.Rotate(0, 0, transform.rotation.x + NwCaminho.Velocidade * Time.deltaTime);
                    #endregion
                }
            }
            else
            {
                #region Translação
                Angu += NwCaminho.Velocidade * Time.deltaTime;
                NwCaminho.Direcao.y = Mathf.Sin(Angu * Mathf.Deg2Rad);
                NwCaminho.Direcao.x = Mathf.Cos(Angu * Mathf.Deg2Rad);
                PontoAtual = NwCaminho.CentroPonto + NwCaminho.Raio * NwCaminho.Direcao;
                if (LookAt)
                    transform.RotateAround(NwCaminho.CentroPonto, Vector3.forward, NwCaminho.Velocidade * Time.deltaTime);
                #endregion
            }
        }


        transform.position = PontoAtual;


    }
}

