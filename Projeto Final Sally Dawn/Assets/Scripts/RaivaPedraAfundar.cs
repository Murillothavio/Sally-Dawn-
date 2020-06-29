using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaivaPedraAfundar : MonoBehaviour
{
    public RaivaPedraCair[] Pass;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            for (int i = 0; i < Pass.Length; i++)
            {
                Pass[i].Afudar = true;
            }
        }
    }
}
