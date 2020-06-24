using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioFaseCiclo : MonoBehaviour
{
    public bool Ativo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Ativo)
                GameMaster.gm.InverterPlat();
        }
    }
}
