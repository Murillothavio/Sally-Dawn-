using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugagem : MonoBehaviour
{
    private GameObject[] PlataformasDuplas;
    private GameObject[] PlataformasInvi;
    public bool Desativar, Deletar;
    // Start is called before the first frame update
    void Start()
    {
            PlataformasInvi = GameObject.FindGameObjectsWithTag("PlataformaInvisivel");
            PlataformasDuplas = GameObject.FindGameObjectsWithTag("PlataformasMovel");
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

            /*foreach (var item in PlataformasInvi)
                if (item.GetComponent<Plataforma_desAtivar>() != null)
                    if (item.GetComponent<Plataforma_desAtivar>().Visivel)
                        item.GetComponent<Plataforma_desAtivar>().Visivel = false;
                    else
                        item.GetComponent<Plataforma_desAtivar>().Visivel = true;*/
            foreach (var item in PlataformasInvi)
            {
                bool Visivel = item.activeInHierarchy;
                //if (item.GetComponent<Plataforma_desAtivar>() != null)
                    if (Visivel)
                        Visivel = false;
                    else
                        Visivel = true;
                item.SetActive(Visivel);
            }
        }
    }
}
