using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegiaoMusica : MonoBehaviour
{
    public AudioClip Musica;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<AudioChange>().acEvento = Musica;
            other.GetComponent<AudioChange>().Trocando = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<AudioChange>().acEvento = null;
            other.GetComponent<AudioChange>().Trocando = true;
        }
    }
}
