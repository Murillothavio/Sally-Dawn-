using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_base_escada : MonoBehaviour
{
    public Vector3 Altura, Distancia;
    public GameObject[] Colunas = new GameObject[5];

    // Start is called before the first frame update
    void Awake()
    {
        float Ha, Hb, Da, Db;
        Ha = transform.GetChild(0).gameObject.transform.position.y;
        Hb = transform.GetChild(1).gameObject.transform.position.y;

        if (Ha > Hb)
            Altura = new Vector3(Hb, Ha, 0);
        else
            Altura = new Vector3(Ha, Hb, 0);
        Altura.z = Mathf.Abs((Ha - Hb) / 4);

        for (int i = 0; i < Colunas.Length; i++)
            Colunas[i] = transform.GetChild(2).GetChild(i).gameObject;

        Da = Colunas[0].transform.position.x;
        Db = Colunas[4].transform.position.x;
        Distancia = new Vector3(Da, Db, Mathf.Abs((Db - Da) / 4));

        for (int i = 0; i < Colunas.Length; i++)
        {
            Colunas[i].GetComponent<Plataforma_escada>().PontoRed = new Vector3((Distancia.z * i) + Distancia.x, Altura.x + (Altura.z * i), 0);
            Colunas[i].GetComponent<Plataforma_escada>().PontoBlue = new Vector3((Distancia.z * i) + Distancia.x, Altura.y - (Altura.z * i), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
