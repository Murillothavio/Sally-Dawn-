using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugagem : MonoBehaviour
{
    private GameObject[] PlataformasDuplas;
    private GameObject[] PlataformasInvi;
    private GameObject[] PlataformaEscada;
    [Header("SetActive ponto Red e Blue")]
    public bool Desativar;
    [Header("Destroy ponto Red e Blue")]
    public bool  Deletar;

    void Start()
    {
            PlataformasInvi = GameObject.FindGameObjectsWithTag("PlataformaInvisivel");
            PlataformasDuplas = GameObject.FindGameObjectsWithTag("PlataformasMovel");
            PlataformaEscada = GameObject.FindGameObjectsWithTag("PlataformaEscada");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var item in PlataformasDuplas)
                if (item.GetComponent<Plataforma_movimento>() != null)
                    if (item.GetComponent<Plataforma_movimento>().Alternado)
                        item.GetComponent<Plataforma_movimento>().Alternado = false;
                    else
                        item.GetComponent<Plataforma_movimento>().Alternado = true;
            
            foreach (var item in PlataformasInvi)
            {
                bool Visivel = item.activeInHierarchy;
                    if (Visivel)
                        Visivel = false;
                    else
                        Visivel = true;
                item.SetActive(Visivel);
            }

            foreach (var item in PlataformaEscada)
                if (item.GetComponent<Plataforma_base_escada>() != null)
                    if (item.GetComponent<Plataforma_base_escada>().IsBlue)
                        item.GetComponent<Plataforma_base_escada>().IsBlue = false;
                    else
                        item.GetComponent<Plataforma_base_escada>().IsBlue = true;

        }
    }
}
