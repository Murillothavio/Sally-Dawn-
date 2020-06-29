using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaivaPedraVoltar : MonoBehaviour
{
    public RaivaPedraCair[] Pass;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("algo");
            for (int i = 0; i < Pass.Length; i++)
            {
                Pass[i].Afudar = false;
            }
        }
    }
}
