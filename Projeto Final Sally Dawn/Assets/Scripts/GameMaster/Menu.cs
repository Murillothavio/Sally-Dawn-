using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [System.Serializable]
    public class MenuLembrancas
    {
        [HideInInspector]
        public string name;
        public bool SeTem;
        public GameObject Descric, Raiz;
    }
    [System.Serializable]
    public class ConfigData {
        public int IdiomaData;
        public float MusicVolumeData, SfxVolumeData;
    }

    #region Variavl
    public AudioMixer aMixerEffect;
    public AudioClip acMenu, acCredito;
    public enum Telas { TelasInicial, TelaPrincipal, TelaJogar, TelaNovo, TelaContinuar, TelaJogo, TelaMenu, JanelaLembranca, JanelaOpcoes, TelaCredito, TelaSair, TelaSobre }
    public Telas tela;
    public enum Idiomas { Portugues, English }
    public Idiomas idioma;
    [SerializeField]
    private GameObject[] TxtIdioma;
    public bool load = true, Jogando, IsMenu, VaiResetar, ChegouMenu, EsperaCarregou, RecarregarScene;
    #endregion
    public float TempEsperando;
    public ConfigData Zero, Atual;
    #region Objetos
    [HideInInspector]
    public int nLembr;
    public Transform Raizes;
    //  [SerializeField]
    private Animator AnimiRaizes;
    private Emocoes lmb;
    public MenuLembrancas[] Lembr = new MenuLembrancas[7];

  //  [HideInInspector]
    public int nOpcoes;
    public GameObject[] JanOpcoes = new GameObject[4];

  //  [HideInInspector]
    public int nObjetivos;
    public GameObject[] ListaObjetivos;

    [HideInInspector]
    public GameObject TelasInicial, TelaPrincipal, TelaJogar, TelaNovo, TelaContinuar, TelaJogo, TelaMenu, JanelaLembranca;
    [HideInInspector]
    public GameObject JanelaOpcoes, TelaCredito, TelaSair, TelaSobre, TxtLoadInicial;
    #endregion
    Resolution[] resolutions;
    public Vector2Int[] resolucoes;
    public Dropdown resolutionDropdown;

    private void Start()
    {
        #region resolucoes
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> Options = new List<string>();
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
        resolutionDropdown.RefreshShownValue();
        #endregion

        TrocarTela();
        if (GameMaster.gm.Player != null)
            lmb = GameMaster.gm.Player.GetComponent<Eventos>().Memorias;
        if (Raizes == null)
            Debug.LogError("Sem raizes transform menu");
        else
            AnimiRaizes = Raizes.GetChild(0).GetComponent<Animator>();

        if (acMenu != null)
        {
            GameMaster.gm.Player.GetComponent<AudioChange>().acEvento = acMenu;
            GameMaster.gm.Player.GetComponent<AudioChange>().Trocando = true;
        }
        CarregarConfig();
        Configar(Atual);
    }

    // Update is called once per frame
    void Update()
    {
        if (tela == Telas.TelasInicial)
        {
            if (!EsperaCarregou)
            {
                TempEsperando += Time.deltaTime;
                if (TempEsperando > 3)
                {
                    EsperaCarregou = true;
                    TempEsperando = 0;
                }
            }
            else
            {
                if (Input.anyKeyDown)
                {
                    EsperaCarregou = false;
                        tela = Telas.TelaPrincipal;
                        TrocarTela();
                }
            }
            TxtLoadInicial.SetActive(EsperaCarregou);
           // Debug.Log(SceneManager.sceneLoaded)
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
                if (RecarregarScene)
                {
                    RecarregarScene = false;
                    Debug.Log("recarregou");
                    SceneManager.LoadScene("Game");
                }
                RecarregarScene = true;

                if (acMenu != null)
                {
                    GameMaster.gm.Player.GetComponent<AudioChange>().acEvento = acMenu;
                    GameMaster.gm.Player.GetComponent<AudioChange>().Trocando = true;
                }
                GameMaster.gm.FadeIN(2,1);
                Invoke("GoLoadar", 1);
             
            }
        }
        else if (tela == Telas.TelaJogo)
        {
            VaiResetar = false;
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
    private void GoLoadar()
    {
        GameMaster.gm.testloadar = true;
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
        if (TelaSobre != null)
            TelaSobre.SetActive(tela == Telas.TelaSobre);
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
        Atual.IdiomaData = (idioma == Idiomas.Portugues) ? 0 : 1;
          
    }
    #region GoTo
    public void GoToTelaSair() { tela = Telas.TelaSair; }
    public void GoToTelaMenu() { tela = Telas.TelaMenu; }
    public void GoToJanelaLembranca() { tela = Telas.JanelaLembranca; }
    public void GoToTelasInicial() { tela = Telas.TelasInicial; }

    public void GoToTelaPrincipal() { tela = Telas.TelaPrincipal;  }
    public void GoToTelaJogar() { tela = Telas.TelaJogar; }

    public void GoToTelaNovo() { tela = Telas.TelaNovo; VaiResetar = true; }
    public void GoToTelaContinuar() { tela = Telas.TelaContinuar; }

    public void GoToTelaJogo() { tela = Telas.TelaJogo; if (VaiResetar) GameMaster.gm.Resetar = true; TrocarTela(); }
    public void GoToJanelaOpcoes() { tela = Telas.JanelaOpcoes; }
    public void GoToSobre() { tela = Telas.TelaSobre; }
    public void GoToTelaCredito()
    {
        tela = Telas.TelaCredito;
        if (acCredito != null)
        {
            GameMaster.gm.Player.GetComponent<AudioChange>().acEvento = acCredito;
            GameMaster.gm.Player.GetComponent<AudioChange>().Trocando = true;
            ChegouMenu = false;
        }
    }
    public void GoToVoltar()
    {
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
    public void ControleConjA()
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
        Atual.MusicVolumeData = volume;
    }
    public void SetVolumeEfeito(float volume)
    {
        aMixerEffect.SetFloat("SFXVolume", volume);
        Atual.SfxVolumeData = volume;
    }
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        //  Vector2Int r = resolucoes[index];
        // if (r.x != 0 && r.y != 0)
        //   Screen.SetResolution(r.x, r.y, Screen.fullScreen);
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("Cubemap");
    }

    #endregion

    public void SalvarConfig()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Config.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, Atual);
        stream.Close();
    }
    void CarregarConfig()
    {
        string path = Application.persistentDataPath + "/Config.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formattter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ConfigData data = formattter.Deserialize(stream) as ConfigData;
            stream.Close();
            Atual = data;
        }
        else
            Debug.LogError("Save file not found in " + path);
    }

    public void ResetarConfig()
    {
        Configar(Zero);
    }
    void  Configar(ConfigData data)
    {
        if (data.IdiomaData == 0)
            idioma = Idiomas.Portugues;
        if (data.IdiomaData == 1)
            idioma = Idiomas.English;

        SetVolumeEfeito(data.SfxVolumeData);
        SetVolumeMusica(data.MusicVolumeData);
    }

    public void Sair()
    {
        Application.Quit();
        Debug.Log("saiu");
    }
}