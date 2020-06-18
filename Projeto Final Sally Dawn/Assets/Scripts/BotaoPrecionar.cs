using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoPrecionar : MonoBehaviour
{
    public bool CicloB, Ocupado, Mudar, QueroCaixa;
    public float CDTemp=2, passtemp;
    [SerializeField]
    private Collider Pressao;


    void Update()
    {
        if (!QueroCaixa)
        if (CicloB != Mudar)
        {
            GameMaster.gm.InverterPlat();
            Mudar = CicloB;
        }

        if (passtemp >= 0)
            passtemp -= Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (passtemp < 0)
            {
                if (CicloB) CicloB = false;
                else CicloB = true;
                passtemp = CDTemp;
            }
        }

        Ocupado = true;
        Pressao = other;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == Pressao)
            Ocupado = false;
    }
}
