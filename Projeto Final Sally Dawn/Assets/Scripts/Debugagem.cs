using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugagem : MonoBehaviour
{
    private GameObject[] PlataformasDuplas;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlataformasDuplas = GameObject.FindGameObjectsWithTag("PlataformasMovel");

            foreach (var item in PlataformasDuplas)
            {
                if (item.GetComponent<Plataforma_movimento>() != null)
                    if (item.GetComponent<Plataforma_movimento>().Alternado)
                        item.GetComponent<Plataforma_movimento>().Alternado = false;
                    else
                        item.GetComponent<Plataforma_movimento>().Alternado = true;
            }
        }
    }
}
