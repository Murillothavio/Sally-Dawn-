using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_quadupla : MonoBehaviour
{
   
    public float Mxtime = 1;
    private float ptime;
    public GameObject[] QuatroPontos = new GameObject[4];
     public bool Alinhado = false;
    public bool[] Igual = new bool[4];
    
    
    
    private void Awake()
    {

        for (int i = 0; i < QuatroPontos.Length; i++)
        {
            QuatroPontos[i] = transform.GetChild(i).gameObject;
            QuatroPontos[i].GetComponent<Plataforma_movimento>().MeuPai = gameObject;
            
        }
    }
    

    public void AtualizaBool(GameObject gb)
    {
        //for (int i = 0; i < QuatroPontos.Length; i++)
        //    if (gb == QuatroPontos[i]) { 
        //        Igual[i] = true;
        //    QuatroPontos[i].GetComponent<Plataforma_movimento>().Junto = false;
        //}


    }
    private void Update()
    {/*
        if (Igual == True) 
            Igual = False;

        for (int i = 0; i < QuatroPontos.Length; i++)
            Debug.Log(Igual[i]);
        Debug.Log("........");
        for (int i = 0; i < QuatroPontos.Length; i++)
        {
            QuatroPontos[i].GetComponent<Plataforma_movimento>().Movendo = !Igual[i];
        }*/
        for (int i = 0; i < Igual.Length; i++)
        {
            Igual[i] = QuatroPontos[i].GetComponent<Plataforma_movimento>().TaNoPonto;

        }
        if (!Alinhado)
            for (int i = 0; i < QuatroPontos.Length; i++)
            {
                QuatroPontos[i].GetComponent<Plataforma_movimento>().Movendo = (!Igual[i]);
            }
        else
            ptime += Time.deltaTime;
        if (ptime>Mxtime)
        {
            ptime = 0;
            Alinhado = false;
        }

        if (Igual[0] == true && Igual[1] == true && Igual[2] == true && Igual[3] == true)
        {
            Debug.Log("isso funciona");
            if (!Alinhado)
            {
                Alinhado = true;
                for (int i = 0; i < QuatroPontos.Length; i++)
                {
                    QuatroPontos[i].GetComponent<Plataforma_movimento>().Movendo = true;
                }
            }
        }

    }
}
