using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaivaNojo : MonoBehaviour
{
    public NojoFumaca fecha;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            fecha.Maquina = gameObject;
        }
    }
}
