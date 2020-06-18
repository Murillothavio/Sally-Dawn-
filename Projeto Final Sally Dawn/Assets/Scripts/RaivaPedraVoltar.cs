using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaivaPedraVoltar : MonoBehaviour
{
    public Transform[] Pedras;
    // Start is called before the first frame update
    void Start()
    {
        Pedras = new Transform[transform.childCount];

        for (int i = 0; i < Pedras.Length; i++)
        {
            Pedras[i] = transform.GetChild(i);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < Pedras.Length; i++)
                Pedras[i].GetComponent<RaivaPedraCair>().Resetar = true;
        }
    }
}
