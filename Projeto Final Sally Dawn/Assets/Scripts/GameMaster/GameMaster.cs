using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    public bool PAUSADO;
    [SerializeField]
    private static GameObject[] PlataformasDuplas;
    [SerializeField]
    private static GameObject[] PlataformasInvi;
    [SerializeField]
    private static GameObject[] PlataformaEscada;
    [SerializeField]
    private static GameObject[] BotoesInverter;
    void Awake()
    {
        if (gm == null)
            gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InverterPlat();

    }

    private void Update()
    {
        PAUSADO = GetComponent<Menu>().IsMenu;
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
}
