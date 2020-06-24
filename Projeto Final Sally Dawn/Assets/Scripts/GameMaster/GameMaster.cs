using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public GameObject Player, Cam;
    public string NameFileS = "saves", TypeSave = "txt";
    [HideInInspector]
    public  string filesave;

    public bool PAUSADO;
    
    [SerializeField]
    private  GameObject[] PlataformasDuplas;
    [SerializeField]
    private  GameObject[] PlataformasInvi;
    [SerializeField]
    private  GameObject[] PlataformaEscada;
    [SerializeField]
    private  GameObject[] BotoesInverter;
    [SerializeField]
    private GameObject[] PortasMedo;

    private Animator anim, CamFade;
    private float SpeedFade = 1;
    
    public bool testsalvar, testloadar, Resetar;
    [Header("save")]
    public float DataNumeroFase;
    public Emocoes DataPowerUps, DataMemorias;

    public Transform _savepointsmenu;

    public Color FaseCor;
    [HideInInspector]
    public Color[] FasesEmCores = new Color[7];

    [System.Serializable]
    public class SaveData
    {
        public int NumeroDaFase;
        public float[] SavePointMenuCoord = new float[3];
        public int[] NumeroMemorias = new int[7], NumeroPowerUp = new int[7];
    }

    public SaveData Zero, Atual;
    void Awake()
    {
        filesave = "/" + NameFileS + "." + TypeSave;
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        if (Player == null)
            Debug.LogError("no player GM");
        if (Cam == null)
            Debug.LogError("No com GM");
        else CamFade = Cam.GetComponent<Animator>();
        anim = GetComponent<Animator>();
       
    }
    void Start()
    {
     //   InverterPlat();

        DataMemorias = GameMaster.gm.Player.GetComponent<Eventos>().Memorias;
        DataPowerUps = GameMaster.gm.Player.GetComponent<Eventos>().PwrUp;
    }

    // Update is called once per frame
    void Update()
    {
        PAUSADO = GetComponent<Menu>().IsMenu;

        if (testloadar)
        {
            testloadar = false;
            SetData(LoadPlayer());
        }
        if (testsalvar)
        {
            testsalvar = false;
            SavePlayer();
        }
        if (Resetar)
        {
            Resetar = false;
            SetData(Zero);
        }


      //  _savepointsmenu = SavePointsMenu[(int)DataNumeroFase];
        FaseCor = FasesEmCores[(int)DataNumeroFase];
    }

    public void GetData()
    {
        DataNumeroFase = GameMaster.gm.Player.GetComponent<Ambiente>().NumFases;
        Atual.NumeroDaFase = (int)DataNumeroFase;

        Atual.SavePointMenuCoord[0] = _savepointsmenu.position.x;
        Atual.SavePointMenuCoord[1] = _savepointsmenu.position.y;
        Atual.SavePointMenuCoord[2] = _savepointsmenu.position.z;

        Atual.NumeroMemorias[0] = (DataMemorias.Neutro) ? 1 : 0;
        Atual.NumeroMemorias[1] = (DataMemorias.Alegre) ? 1 : 0;
        Atual.NumeroMemorias[2] = (DataMemorias.Triste) ? 1 : 0;
        Atual.NumeroMemorias[3] = (DataMemorias.Raiva) ? 1 : 0;
        Atual.NumeroMemorias[4] = (DataMemorias.Nojo) ? 1 : 0;
        Atual.NumeroMemorias[5] = (DataMemorias.Medo) ? 1 : 0;
        Atual.NumeroMemorias[6] = (DataMemorias.Etereo) ? 1 : 0;

        Atual.NumeroPowerUp[0] = (DataPowerUps.Neutro) ? 1 : 0;
        Atual.NumeroPowerUp[1] = (DataPowerUps.Alegre) ? 1 : 0;
        Atual.NumeroPowerUp[2] = (DataPowerUps.Triste) ? 1 : 0;
        Atual.NumeroPowerUp[3] = (DataPowerUps.Raiva) ? 1 : 0;
        Atual.NumeroPowerUp[4] = (DataPowerUps.Nojo) ? 1 : 0;
        Atual.NumeroPowerUp[5] = (DataPowerUps.Medo) ? 1 : 0;
        Atual.NumeroPowerUp[6] = (DataPowerUps.Etereo) ? 1 : 0;
    }

    public void SetData(SaveData ds)
    {

        GameMaster.gm.Player.GetComponent<Ambiente>().SetAmbiente(ds.NumeroDaFase);

        GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Neutro = (ds.NumeroMemorias[0] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Alegre = (ds.NumeroMemorias[1] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Triste = (ds.NumeroMemorias[2] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Raiva = (ds.NumeroMemorias[3] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Nojo = (ds.NumeroMemorias[4] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Medo = (ds.NumeroMemorias[5] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().Memorias.Etereo = (ds.NumeroMemorias[6] == 1);

        GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Neutro = (ds.NumeroPowerUp[0] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Alegre = (ds.NumeroPowerUp[1] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Triste = (ds.NumeroPowerUp[2] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Raiva = (ds.NumeroPowerUp[3] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Nojo = (ds.NumeroPowerUp[4] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Medo = (ds.NumeroPowerUp[5] == 1);
        GameMaster.gm.Player.GetComponent<Eventos>().PwrUp.Etereo = (ds.NumeroPowerUp[6] == 1);

        Vector3 posicao = Vector3.zero;
        posicao.x = ds.SavePointMenuCoord[0];
        posicao.y = ds.SavePointMenuCoord[1];
        posicao.z = ds.SavePointMenuCoord[2];
        GameMaster.gm.Player.transform.position = posicao;
        
    }
    
    public  void InverterPlat()
    {
        Debug.Log("inverteu");
        PlataformasDuplas = GameObject.FindGameObjectsWithTag("PlataformasMovel");
        foreach (var item in PlataformasDuplas)
        {
            if (item.GetComponent<Plataforma_movimento>() != null)
                if (item.GetComponent<Plataforma_movimento>().Alternado)
                    item.GetComponent<Plataforma_movimento>().Alternado = false;
                else
                    item.GetComponent<Plataforma_movimento>().Alternado = true;
        }

        PlataformasInvi = GameObject.FindGameObjectsWithTag("PlataformaInvisivel");
        foreach (var item in PlataformasInvi)
        {
            if (item.GetComponent<Plataforma_desAtivar>() != null)
                if (item.GetComponent<Plataforma_desAtivar>().Visivel)
                    item.GetComponent<Plataforma_desAtivar>().Visivel = false;
                else
                    item.GetComponent<Plataforma_desAtivar>().Visivel = true;
        }

        PlataformaEscada = GameObject.FindGameObjectsWithTag("PlataformaEscada");
        foreach (var item in PlataformaEscada)
        {
            if (item.GetComponent<Plataforma_base_escada>() != null)
                if (item.GetComponent<Plataforma_base_escada>().IsBlue)
                    item.GetComponent<Plataforma_base_escada>().IsBlue = false;
                else item.GetComponent<Plataforma_base_escada>().IsBlue = true;
        }

        BotoesInverter = GameObject.FindGameObjectsWithTag("BotaoInverter");
        foreach (var item in BotoesInverter)
        {
            if (item.GetComponent<Plataforma_Botao>() != null)
                item.GetComponent<Plataforma_Botao>().Ativo =
                   !item.GetComponent<Plataforma_Botao>().Ativo;
        }

        PortasMedo= GameObject.FindGameObjectsWithTag("PortaMedo");
        foreach (var item in PortasMedo)
        {
            if (item.GetComponent<Porta_Medo>() != null)
                item.GetComponent<Porta_Medo>().Ativo = !item.GetComponent<Porta_Medo>().Ativo;
        }
    }

    public static float  CalcularDist(Transform a, Transform b)
    {
        float CatetoX = a.position.x - b.position.x;
        float CatetoY = a.position.y - b.position.y;

        return Mathf.Sqrt(CatetoX * CatetoX + CatetoY * CatetoY);
    }

    public  void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + filesave;
        FileStream stream = new FileStream(path, FileMode.Create);

        GetData();

        formatter.Serialize(stream, Atual);
        stream.Close();

    }
    
    public  SaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + filesave;
        if (File.Exists(path))
        {
            BinaryFormatter formattter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formattter.Deserialize(stream) as SaveData;
            stream.Close();
            Atual = data;
            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public void FadeIN(float Timer = 3, float Wait = 0)
    {
        if (Timer == 0)
            SpeedFade = 1;
        else        SpeedFade = 3 / Timer;


        CamFade.speed = SpeedFade;
        CamFade.SetTrigger("Fade");
        Invoke("FadeOUT", (Timer / 2) + Wait);

    }
    public void FadeOUT()
    {
        CamFade.speed = SpeedFade;
        CamFade.SetTrigger("Despause");
    }
}
