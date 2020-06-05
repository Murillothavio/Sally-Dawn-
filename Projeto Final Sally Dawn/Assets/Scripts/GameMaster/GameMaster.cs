using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public GameObject Player;
    public string NameFileS = "saves", TypeSave = "txt", cu;
   public  string filesave;

    public bool PAUSADO;
    
    [SerializeField]
    private static GameObject[] PlataformasDuplas;
    [SerializeField]
    private static GameObject[] PlataformasInvi;
    [SerializeField]
    private static GameObject[] PlataformaEscada;
    [SerializeField]
    private static GameObject[] BotoesInverter;



    public bool testsalvar, testloadar;
    [Header("save")]
    public float DataNumeroFase;
    public Emocoes DataPowerUps, DataMemorias;

    public Vector3 _savepointsmenu;
    public Vector3[] SavePointsMenu = new Vector3[7];

    public Color FaseCor;
    //[HideInInspector]
    public Color[] FasesEmCores = new Color[7];

    [System.Serializable]
    public class SaveData
    {
        public int NumeroDaFase;
        public int[] NumeroMemorias = new int[7], NumeroPowerUp = new int[7];
    }

    public SaveData Zero, Atual;
    void Awake()
    {
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        if (Player == null)
            Debug.LogError("no player GM");
    }
    void Start()
    {
        InverterPlat();

        DataMemorias = GameMaster.gm.Player.GetComponent<Eventos>().Memorias;
        DataPowerUps = GameMaster.gm.Player.GetComponent<Eventos>().PwrUp;
    }

    // Update is called once per frame
    void Update()
    {
        PAUSADO = GetComponent<Menu>().IsMenu;
        filesave = "/" + NameFileS + "." + TypeSave;

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


        _savepointsmenu = SavePointsMenu[(int)DataNumeroFase];
        FaseCor = FasesEmCores[(int)DataNumeroFase];
    }

    public void GetData()
    {
        DataNumeroFase = GameMaster.gm.Player.GetComponent<Ambiente>().NumFases;
        Atual.NumeroDaFase = (int)DataNumeroFase;

        Atual.NumeroMemorias[0] = (DataMemorias.Neutro) ? 1 : 0;
        Atual.NumeroMemorias[1] = (DataMemorias.Alegre) ? 1 : 0;
        Atual.NumeroMemorias[2] = (DataMemorias.Triste) ? 1 : 0;
        Atual.NumeroMemorias[3] = (DataMemorias.Raiva) ? 1 : 0;
        Atual.NumeroMemorias[4] = (DataMemorias.Nojo) ? 1 : 0;
        Atual.NumeroMemorias[5] = (DataMemorias.Medo) ? 1 : 0;
        Atual.NumeroMemorias[6] = (DataMemorias.Etereo) ? 1 : 0;

        Atual.NumeroPowerUp[0] = (DataMemorias.Neutro) ? 1 : 0;
        Atual.NumeroPowerUp[1] = (DataMemorias.Alegre) ? 1 : 0;
        Atual.NumeroPowerUp[2] = (DataMemorias.Triste) ? 1 : 0;
        Atual.NumeroPowerUp[3] = (DataMemorias.Raiva) ? 1 : 0;
        Atual.NumeroPowerUp[4] = (DataMemorias.Nojo) ? 1 : 0;
        Atual.NumeroPowerUp[5] = (DataMemorias.Medo) ? 1 : 0;
        Atual.NumeroPowerUp[6] = (DataMemorias.Etereo) ? 1 : 0;
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

    }
    
    public static void InverterPlat()
    {
        Debug.Log("wert");
        PlataformasDuplas = GameObject.FindGameObjectsWithTag("PlataformasMovel");
        foreach (var item in PlataformasDuplas)
            if (item.GetComponent<Plataforma_movimento>() != null)
                if (item.GetComponent<Plataforma_movimento>().Alternado)
                    item.GetComponent<Plataforma_movimento>().Alternado = false;
                else
                    item.GetComponent<Plataforma_movimento>().Alternado = true;

        PlataformasInvi = GameObject.FindGameObjectsWithTag("PlataformaInvisivel");
        foreach (var item in PlataformasInvi)
        {
            bool Visivel = item.activeInHierarchy;
            if (Visivel)
                Visivel = false;
            else
                Visivel = true;
            item.SetActive(Visivel);
        }

        PlataformaEscada = GameObject.FindGameObjectsWithTag("PlataformaEscada");
        foreach (var item in PlataformaEscada)
            if (item.GetComponent<Plataforma_base_escada>() != null)
                if (item.GetComponent<Plataforma_base_escada>().IsBlue)
                    item.GetComponent<Plataforma_base_escada>().IsBlue = false;
                else item.GetComponent<Plataforma_base_escada>().IsBlue = true;

        BotoesInverter = GameObject.FindGameObjectsWithTag("BotaoInverter");
        foreach (var item in BotoesInverter)
        {
            if (item.GetComponent<BotaoInverter>() != null)
                if (item.GetComponent<BotaoInverter>().DesAtivo)
                    item.GetComponent<BotaoInverter>().DesAtivo = false;
                else item.GetComponent<BotaoInverter>().DesAtivo = true;
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
            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

}
