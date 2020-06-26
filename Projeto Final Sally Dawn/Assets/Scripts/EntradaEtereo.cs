using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntradaEtereo : MonoBehaviour
{
    public Emocoes Vazio;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Eventos>().PwrUp = Vazio;
        }
    }
}
