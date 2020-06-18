using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaivaPedraAfundar : MonoBehaviour
{
    public bool SimEGambiara;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SimEGambiara = true;
        }
    }
}
