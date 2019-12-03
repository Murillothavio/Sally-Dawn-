using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_movimento : MonoBehaviour
{
    #region Variavel
    private Rigidbody rb;
    private bool Atv = true;
    public bool VaiMudar = false;
    private Vector3 Ponto0;
    private Vector3 PontoP;
    private Vector3 PontoM;
    private float Angu;
    private float aa = 90;


    public struct Plat
    {
        public Vector3 Direcao;
        public Vector3 PontoF;
        public Vector3 PontoI;
        public Vector3 CPonto;
        public float velo;
        public float raio;
        //   public float angu;

        public Plat(Vector3 _Direcao, float _TamanP, float _TamanN, float _velo, float _raio)
        {
            this.Direcao = _Direcao;
            this.PontoF = _TamanP * _Direcao;
            this.PontoI = _TamanN * _Direcao * -1;
            this.velo = _velo;
            this.raio = -_raio;
            this.CPonto = _raio * _Direcao;
        }

    }

    public Plat Horizontalc = new Plat(Vector3.forward, 20, 10, 8, 0);
    public Plat Verticalc = new Plat(Vector3.up, 20, 10, 15, 0);
    public Plat Crescentec = new Plat(new Vector3(0, 1, 1), 20, 10, 8, 0);
    public Plat Decrescentec = new Plat(new Vector3(0, 1, -1), 20, 10, 8, 0);
    public Plat Translacaoc = new Plat(Vector3.up, 0, 0, 190, 10);
    public Plat Horarioc = new Plat(Vector3.zero, 0, 0, 8, 0);
    public Plat Anhorarioc = new Plat(Vector3.zero, 0, 0, -8, 0);

    public Plat AaCaminho;
    public Plat BbCaminho;
    public Plat NwCaminho;

    public Plat CaminhoInicial;
    public Vector3 DirecaoInicial = Vector3.forward;
    public float TamanhoRightInicial = 20, TamanhoLeftInicial = 10, VelociadadeInicial = 8, RaioInicial = 0;

    public Plat CaminhoFinal;
    public Vector3 DirecaoFinal = Vector3.up;
    public float TamanhoRightFinal = 20, TamanhoLeftFinal = 10, VelociadadeFinal = 15, RaioFinal = 0;


    /*Colocar ponto 0
Direção a
Direção b
Translação
Rotação
Tamanho do caminho
Velocidade*/
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Ponto0 = transform.position;
        PontoP = Ponto0;

        CaminhoInicial = new Plat(DirecaoInicial, TamanhoRightInicial, TamanhoLeftInicial, VelociadadeInicial, RaioInicial);
        CaminhoFinal = new Plat(DirecaoFinal, TamanhoRightFinal, TamanhoLeftFinal, VelociadadeFinal, RaioFinal);
        AaCaminho = CaminhoInicial;
        BbCaminho = CaminhoFinal;
        ;

        AaCaminho.PontoF += Ponto0;
        AaCaminho.PontoI += Ponto0;
        AaCaminho.CPonto += Ponto0;
        BbCaminho.PontoF += Ponto0;
        BbCaminho.PontoI += Ponto0;
        BbCaminho.CPonto += Ponto0;

        if (Atv)
            NwCaminho = AaCaminho;
        else
            NwCaminho = BbCaminho;
        PontoM = NwCaminho.PontoF;


        if (NwCaminho.Direcao.y != 0) Angu = Mathf.Asin(NwCaminho.Direcao.y) * Mathf.Rad2Deg;
        else if (NwCaminho.Direcao.z != 0) Angu = Mathf.Acos(NwCaminho.Direcao.z) * Mathf.Rad2Deg;

        Vector3 qqqqqq = Vector3.forward;

        if (qqqqqq.y != 0) aa = Mathf.Asin(qqqqqq.y) * Mathf.Rad2Deg;
        else if (qqqqqq.z != 0) aa = Mathf.Acos(qqqqqq.z) * Mathf.Rad2Deg;
        //  aa = Mathf.Sin(aa * Mathf.Deg2Rad);
        //aa = Mathf.Asin(aa) * Mathf.Rad2Deg;

    }

    // Update is called once per frame
    void Update()
    {

        if ((int)PontoP.y == (int)Ponto0.y && (int)PontoP.z == (int)Ponto0.z)
        {
            if (VaiMudar)
            {
                VaiMudar = false;
                if (Atv) Atv = false;
                else Atv = true;
                if (Atv) NwCaminho = AaCaminho;
                else NwCaminho = BbCaminho;
                PontoM = NwCaminho.PontoF;
                if (NwCaminho.Direcao.y != 0) Angu = Mathf.Asin(NwCaminho.Direcao.y) * Mathf.Rad2Deg;
                else if (NwCaminho.Direcao.z != 0) Angu = Mathf.Acos(NwCaminho.Direcao.z) * Mathf.Rad2Deg;
            }
        }



        //   aa = Mathf.Sin(aa * Mathf.Deg2Rad);
        // aa = Mathf.Asin(aa) * Mathf.Rad2Deg;

        aa += 10;




        //        Debug.Log(NwCaminho.Direcao);
        //      Debug.Log(new Vector2(Mathf.Acos(NwCaminho.Direcao.y), Mathf.Acos(NwCaminho.Direcao.z)) * Mathf.Rad2Deg);
        //    Debug.Log(new Vector2(Mathf.Asin(NwCaminho.Direcao.y), Mathf.Asin(NwCaminho.Direcao.z)) * Mathf.Rad2Deg);




        //      Angu += NwCaminho.velo * Time.deltaTime;
        //       NwCaminho.Direcao.y = Mathf.Sin(Angu * Mathf.Deg2Rad);
        //     NwCaminho.Direcao.z = Mathf.Cos(Angu * Mathf.Deg2Rad);
        //    Debug.Log(NwCaminho.Direcao);
        //  Debug.Log(Angu);
        if (NwCaminho.CPonto == Ponto0)
        {
            if (NwCaminho.Direcao != Vector3.zero)
            {
                #region Andar Reto
                if ((int)PontoP.z == (int)NwCaminho.PontoF.z
                 && (int)PontoP.y == (int)NwCaminho.PontoF.y)
                    PontoM = NwCaminho.PontoI;

                else if ((int)PontoP.z == (int)NwCaminho.PontoI.z
                      && (int)PontoP.y == (int)NwCaminho.PontoI.y)
                    PontoM = NwCaminho.PontoF;

                PontoP.z = Mathf.MoveTowards(PontoP.z, PontoM.z, NwCaminho.velo * Time.deltaTime);
                PontoP.y = Mathf.MoveTowards(PontoP.y, PontoM.y, NwCaminho.velo * Time.deltaTime);
                #endregion
            }
            else
            {
                #region Rotação
                transform.Rotate(transform.rotation.x + NwCaminho.velo * Time.deltaTime, 0, 0);
                #endregion
            }
        }
        else
        {
            #region Translação
            Angu += NwCaminho.velo * Time.deltaTime;
            NwCaminho.Direcao.y = Mathf.Sin(Angu * Mathf.Deg2Rad);
            NwCaminho.Direcao.z = Mathf.Cos(Angu * Mathf.Deg2Rad);
            PontoP = NwCaminho.CPonto + NwCaminho.raio * NwCaminho.Direcao;
            #endregion
        }



        transform.position = PontoP;
        
       
    }
}

