using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_movimento : MonoBehaviour
{
    #region Variavel

    public Quaternion look;
    private Rigidbody rb;
    private bool Estado = true;
    public bool Alternado = false;
    private Vector3 Ponto0;
    private Vector3 PontoAtual;
    private Vector3 PontoGoTo;
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
            this.Direcao = _Direcao;
            this.PontoFinal = _TamanP * _Direcao;
            this.PontoInicial = _TamanN * _Direcao * -1;
            this.Velocidade = _velo;
            this.Raio = -_raio;
            this.CentroPonto = _raio * _Direcao;
        }

    }

    public Plat Horizontalc = new Plat(Vector3.right, 20, 10, 8, 0);
    public Plat Verticalc = new Plat(Vector3.up, 20, 10, 15, 0);
    public Plat Crescentec = new Plat(new Vector3(0, 1, 1), 20, 10, 8, 0);
    public Plat Decrescentec = new Plat(new Vector3(0, 1, -1), 20, 10, 8, 0);
    public Plat Translacaoc = new Plat(Vector3.up, 0, 0, 190, 10);
    public Plat Horarioc = new Plat(Vector3.zero, 0, 0, 8, 0);
    public Plat Anhorarioc = new Plat(Vector3.zero, 0, 0, -8, 0);

    public Plat AaCaminho;
    public Plat BbCaminho;
    public Plat NwCaminho;

    public bool LookAt;
    public Plat CaminhoInicial;
    public Vector3 DirecaoInicial = Vector3.right;
    public float TamanhoRightInicial = 20, TamanhoLeftInicial = 10, VelociadadeInicial = 8, RaioInicial = 0;

    public bool Dupla = true;
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
        PontoAtual = Ponto0;

        CaminhoInicial = new Plat(DirecaoInicial, TamanhoRightInicial, TamanhoLeftInicial, VelociadadeInicial, RaioInicial);
        CaminhoFinal = new Plat(DirecaoFinal, TamanhoRightFinal, TamanhoLeftFinal, VelociadadeFinal, RaioFinal);
        AaCaminho = CaminhoInicial;
        if (Dupla)
            BbCaminho = CaminhoFinal;
        else
            BbCaminho = CaminhoInicial;
        

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

    void Update()
    {
        if (Mathf.Abs(PontoAtual.y - Ponto0.y) < 0.5f && Mathf.Abs(PontoAtual.z - Ponto0.z) < 0.5f && Mathf.Abs(PontoAtual.x - Ponto0.x) < 0.5f)
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
                transform.Rotate(0,0,transform.rotation.x + NwCaminho.Velocidade * Time.deltaTime);
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
            {
                transform.LookAt(NwCaminho.CentroPonto);
                var  zzz = transform.rotation.z;
                transform.Rotate(0, 0, 0);
                look = transform.rotation;

            }

          //  transform.RotateAround(NwCaminho.CentroPonto, Vector3.forward, NwCaminho.Velocidade * Time.deltaTime);

            #endregion
        }



        transform.position = PontoAtual;
        
       
    }
}

