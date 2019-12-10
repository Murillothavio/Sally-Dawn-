using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_quadupla : MonoBehaviour
{
    public GameObject[] QuatroPontos = new GameObject[4];
    [HideInInspector]
    public bool Alinhado, tst;
    public bool[] Igual = new bool[4];
    
    private void Awake()
    {
        for (int i = 0; i < QuatroPontos.Length; i++)
        {
            QuatroPontos[i] = transform.GetChild(i).gameObject;
            QuatroPontos[i].GetComponent<Plataforma_movimento>().MeuPai = gameObject;
        }
    }
    
    private void Update()
    {
        for (int i = 0; i < Igual.Length; i++)
            Igual[i] = QuatroPontos[i].GetComponent<Plataforma_movimento>().TaNoPonto;

        tst = (Igual[0] == Igual[1] && Igual[1] == Igual[2] && Igual[2] == Igual[3]);
        if (tst)
            Alinhado = (Igual[3] == true);

        for (int i = 0; i < QuatroPontos.Length; i++)
            QuatroPontos[i].GetComponent<Plataforma_movimento>().Movendo = (!Igual[i] || Alinhado);
    }
}
