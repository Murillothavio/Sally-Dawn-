using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [System.Serializable]
    public class MenuLembrancas {
        [HideInInspector]
        public string name;
        public bool SeTem;
        public GameObject Descric, Raiz;
    }

    public enum Telas { TelasInicial, TelaPrincipal, TelaJogar, TelaNovo, TelaContinuar, TelaJogo, TelaMenu, JanelaLembranca, JanelaOpcoes, TelaCredito, TelaSair }
    public Telas tela;
    public enum Idiomas { Portugues, English}
    public Idiomas idoma;
    [SerializeField]
    private GameObject[] TxtIdioma;
    public bool load=true, Jogando,IsMenu, VaiResetar;

    public int nLembr;
    public Transform Raizes;
    [SerializeField]
    private Animator AnimiRaizes;
    private Emocoes lmb;
    public MenuLembrancas[] Lembr= new MenuLembrancas[7];

    public int nOpcoes;
    public GameObject[] JanOpcoes = new GameObject[4];
    public GameObject TelasInicial, TelaPrincipal, TelaJogar, TelaNovo, TelaContinuar, TelaJogo, TelaMenu, JanelaLembranca, JanelaOpcoes, TelaCredito, TelaSair;
    // Start is called before the first frame update
    void Start()
    {
        TrocarTela();
        if (GameMaster.gm.Player != null)
            lmb = GameMaster.gm.Player.GetComponent<Eventos>().Memorias;
        if (Raizes == null)
            Debug.LogError("Sem raizes transform menu");
        else
            AnimiRaizes = Raizes.GetChild(0).GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (tela == Telas.TelasInicial)
            if (Input.anyKeyDown)
                if (!load)
                {
                    tela = Telas.TelaPrincipal;
                    TrocarTela();
                }
                else
                {
                    load = false;
                    GameMaster.gm.testloadar = true;
                }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (tela == Telas.TelaJogo)
                tela = Telas.TelaMenu;
            else if (tela == Telas.TelaMenu)
                tela = Telas.TelaJogo;
            TrocarTela();
        }

        IsMenu = (tela != Telas.TelaJogo);
        if (IsMenu)
            TrocarTela();


        if (tela == Telas.JanelaLembranca)
            Lembrancas();
        for (int i = 0; i < Lembr.Length; i++)
        {
            bool VerTem = false;
            switch (Lembr[i].name)
            {
                case "Neutro":
                    VerTem = lmb.Neutro;
                    break;
                case "Alegre":
                    VerTem = lmb.Alegre;
                    break;
                case "Triste":
                    VerTem = lmb.Triste;
                    break;
                case "Raiva":
                    VerTem = lmb.Raiva;
                    break;
                case "Nojo":
                    VerTem = lmb.Nojo;
                    break;
                case "Medo":
                    VerTem = lmb.Medo;
                    break;
            }

            Lembr[i].SeTem = VerTem;
            if (i != 0)
                if (Lembr[i].Raiz != null)
                    Lembr[i].Raiz.SetActive(Lembr[i].SeTem);
                else Debug.LogError("Menu sem lembranca raiz");
        }
        AnimiRaizes.SetBool("Up", IsMenu);
        if (IsMenu)
            Raizes.position = GameMaster.gm.Player.transform.position;

        if (tela==Telas.JanelaOpcoes)
            for (int i = 0; i < JanOpcoes.Length; i++)
            {
                JanOpcoes[i].SetActive(i == nOpcoes);
            }

        if (tela == Telas.TelaPrincipal)
        {
            Jogando = false;
            VaiResetar = false;
        }
        else if (tela == Telas.TelaJogo)
            Jogando = true;

    }
    private void Lembrancas()
    {
        if (nLembr != 0)
            if (!Lembr[nLembr].SeTem)
                nLembr = 0;

        for (int i = 0; i < Lembr.Length; i++)
        {
            if (Lembr[i].Descric != null)
                Lembr[i].Descric.SetActive(i == nLembr);
            else Debug.LogError("Menu sem lembranca descricao");
        }
    }
    private void TrocarTela()
    {
        if (TelasInicial != null)
            TelasInicial.SetActive(tela == Telas.TelasInicial);
        if (TelaPrincipal != null)
            TelaPrincipal.SetActive(tela == Telas.TelaPrincipal);
        if (TelaJogar != null)
            TelaJogar.SetActive(tela == Telas.TelaJogar);
        if (TelaNovo != null)
            TelaNovo.SetActive(tela == Telas.TelaNovo);
        if (TelaContinuar != null)
            TelaContinuar.SetActive(tela == Telas.TelaContinuar);
        if (TelaJogo != null)
            TelaJogo.SetActive(tela == Telas.TelaJogo);
        if (TelaMenu != null)
            TelaMenu.SetActive(tela == Telas.TelaMenu);
        if (JanelaLembranca != null)
            JanelaLembranca.SetActive(tela == Telas.JanelaLembranca);
        if (JanelaOpcoes != null)
            JanelaOpcoes.SetActive(tela == Telas.JanelaOpcoes);
        if (TelaCredito != null)
            TelaCredito.SetActive(tela == Telas.TelaCredito);
        if (TelaSair != null)
            TelaSair.SetActive(tela == Telas.TelaSair);
        MudarIdioma();
    }
    private void MudarIdioma()
    {
        TxtIdioma = GameObject.FindGameObjectsWithTag("IdiomaPT");
        foreach (var item in TxtIdioma)
            if (item.GetComponent<TextMeshProUGUI>() != null)
                item.GetComponent<TextMeshProUGUI>().enabled = (idoma == Idiomas.Portugues);

        TxtIdioma = GameObject.FindGameObjectsWithTag("IdiomaENG");
        foreach (var item in TxtIdioma)
            if (item.GetComponent<TextMeshProUGUI>() != null)
                item.GetComponent<TextMeshProUGUI>().enabled = (idoma == Idiomas.English);
    }
    #region GoTo
    public void GoToTelaSair() { tela = Telas.TelaSair; }
    public void GoToTelaMenu() { tela = Telas.TelaMenu; }
    public void GoToJanelaLembranca() { tela = Telas.JanelaLembranca; }
    public void GoToTelasInicial() { tela = Telas.TelasInicial; }
    public void GoToTelaPrincipal() { tela = Telas.TelaPrincipal;GameMaster.gm.testsalvar = true; }
    public void GoToTelaJogar() { tela = Telas.TelaJogar; }
    public void GoToTelaNovo() { tela = Telas.TelaNovo;        if (VaiResetar) GameMaster.gm.Resetar = true;    }
    public void GoToTelaContinuar() { tela = Telas.TelaContinuar; }
    public void GoToTelaJogo() { tela = Telas.TelaJogo; TrocarTela(); }
    public void GoToJanelaOpcoes() { tela = Telas.JanelaOpcoes; }
    public void GoToTelaCredito() { tela = Telas.TelaCredito; }
    public void GoToVoltar() {
        if (Jogando) tela = Telas.TelaMenu;
        else tela = Telas.TelaPrincipal;
    }
    #endregion
    #region Janela Lembrnaças
    public void LemnbracaNumeroUM() { nLembr = 1; }
    public void LemnbracaNumeroDOIS() { nLembr = 2; }
    public void LemnbracaNumeroTRES() { nLembr = 3; }
    public void LemnbracaNumeroQUATRO() { nLembr = 4; }
    public void LemnbracaNumeroCINCO() { nLembr = 5; }
    public void LemnbracaNumeroSEIS() { nLembr = 6; }
    #endregion
    #region Janela Opções
    public void OpcoesQUATRO() { nOpcoes = 3; }
    public void OpcoesUM() { nOpcoes = 0; }
    public void OpcoesDOIS() { nOpcoes = 1; }
    public void OpcoesTRES() { nOpcoes = 2; }
    public void IdiomaPortugues() { idoma = Idiomas.Portugues; }
    public void IdiomaIngles() { idoma = Idiomas.English; }
    public  void ControleConjA()
    {
        GameMaster.gm.Player.GetComponent<Controles>().SetConjunto = Controles.GetConjunto.ConjuntoA;
    }
    public void ControleConjB()
    {
        GameMaster.gm.Player.GetComponent<Controles>().SetConjunto = Controles.GetConjunto.ConjuntoB;
    }

    #endregion

    public void Sair()
    {
        Application.Quit();
        Debug.Log("saiu");
    }
}