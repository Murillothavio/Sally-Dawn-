using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
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

    public AudioMixer aMixerEffect;
    public AudioClip acMenu, acCredito;
    public enum Telas { TelasInicial, TelaPrincipal, TelaJogar, TelaNovo, TelaContinuar, TelaJogo, TelaMenu, JanelaLembranca, JanelaOpcoes, TelaCredito, TelaSair }
    public Telas tela;
    public enum Idiomas { Portugues, English}
    public Idiomas idioma;
    [SerializeField]
    private GameObject[] TxtIdioma;
    public bool load=true, Jogando,IsMenu, VaiResetar, ChegouMenu;

    [HideInInspector]
    public int nLembr;
    public Transform Raizes;
  //  [SerializeField]
    private Animator AnimiRaizes;
    private Emocoes lmb;
    public MenuLembrancas[] Lembr= new MenuLembrancas[7];

    [HideInInspector]
    public int nOpcoes;
    public GameObject[] JanOpcoes = new GameObject[4];

    [HideInInspector]
    public int nObjetivos;
    public GameObject[] ListaObjetivos;

    [HideInInspector]
    public GameObject TelasInicial, TelaPrincipal, TelaJogar, TelaNovo, TelaContinuar, TelaJogo, TelaMenu, JanelaLembranca, JanelaOpcoes, TelaCredito, TelaSair;

    Resolution[] resolutions;
    public Vector2Int[] resolucoes;
    public Dropdown resolutionDropdown;

    private void Awake()
    {
        resolutions = Screen.resolutions;
        /* resolutionDropdown.ClearOptions();

         List<string> Options=new List<string>();
         int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            
                Options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
         resolutionDropdown.AddOptions(Options);
         resolutionDropdown.value = currentResolutionIndex;
         resolutionDropdown.RefreshShownValue();*/

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

        if (tela == Telas.JanelaOpcoes)
            for (int i = 0; i < JanOpcoes.Length; i++)
            {
                JanOpcoes[i].SetActive(i == nOpcoes);
            }

        if (tela == Telas.TelaMenu)
        {
            nObjetivos = (int)GameMaster.gm.Player.GetComponent<Ambiente>().NumFases;
            for (int i = 0; i < ListaObjetivos.Length; i++)
                ListaObjetivos[i].SetActive(nObjetivos == i);
        }

        if (tela == Telas.TelaPrincipal)
        {
            Jogando = false;
            VaiResetar = false;
            if (!ChegouMenu)
            {
                ChegouMenu = true;
                if (acMenu != null)
                {
                    GameMaster.gm.Player.GetComponent<AudioChange>().acEvento = acMenu;
                    GameMaster.gm.Player.GetComponent<AudioChange>().Trocando = true;
                }
                Debug.Log("carregar");
            }
        }
        else if (tela == Telas.TelaJogo)
        {
            Jogando = true;
            if (ChegouMenu)
            {
                ChegouMenu = false;
                if (GameMaster.gm.Player.GetComponent<AudioChange>().acEvento == acMenu)
                {
                    GameMaster.gm.Player.GetComponent<AudioChange>().acEvento = null;
                    GameMaster.gm.Player.GetComponent<AudioChange>().Trocando = true;
                }
            }
        }

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
        {
            if (item.GetComponent<TextMeshProUGUI>() != null)
                item.GetComponent<TextMeshProUGUI>().enabled = (idioma == Idiomas.Portugues);
            if (item.GetComponent<Text>() != null)
                item.GetComponent<Text>().enabled = (idioma == Idiomas.Portugues);
        }

        TxtIdioma = GameObject.FindGameObjectsWithTag("IdiomaENG");
        foreach (var item in TxtIdioma)
        {
            if (item.GetComponent<TextMeshProUGUI>() != null)
                item.GetComponent<TextMeshProUGUI>().enabled = (idioma == Idiomas.English);
            if (item.GetComponent<Text>() != null)
                item.GetComponent<Text>().enabled = (idioma == Idiomas.English);
        }
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
    public void GoToTelaCredito() { tela = Telas.TelaCredito;
        if (acCredito != null)
        {
            GameMaster.gm.Player.GetComponent<AudioChange>().acEvento = acCredito;
            GameMaster.gm.Player.GetComponent<AudioChange>().Trocando = true;
            ChegouMenu = false;
        }
    }
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
    public void IdiomaPortugues() { idioma = Idiomas.Portugues; }
    public void IdiomaIngles() { idioma = Idiomas.English; }
    public  void ControleConjA()
    {
        GameMaster.gm.Player.GetComponent<Controles>().SetConjunto = Controles.GetConjunto.ConjuntoA;
    }
    public void ControleConjB()
    {
        GameMaster.gm.Player.GetComponent<Controles>().SetConjunto = Controles.GetConjunto.ConjuntoB;
    }

    #endregion
    #region Config 
    public void SetVolumeMusica(float volume)
    {
        GameMaster.gm.Player.GetComponent<AudioChange>().AudioVolume = volume;
    }
    public void SetVolumeEfeito(float volume)
    {
        aMixerEffect.SetFloat("SFXVolume", volume);
        Debug.Log(volume);

    }
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Vector2Int r = resolucoes[index];
        if (r.x != 0 && r.y != 0)
            Screen.SetResolution(r.x, r.y, Screen.fullScreen);
    }

    #endregion
    public void Sair()
    {
        Application.Quit();
        Debug.Log("saiu");
    }
}