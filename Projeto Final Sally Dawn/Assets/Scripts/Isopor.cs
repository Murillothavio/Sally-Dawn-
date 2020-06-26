using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Isopor : MonoBehaviour
{
    public bool Reset,Perto;
    private Animator animi;
    // Start is called before the first frame update
    void Start()
    {
        animi = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Reset)
        {
            animi.SetTrigger("Reset");
            Reset = false;
        }
        if (Perto)
        {
            Perto = false;
            animi.SetTrigger("Perto");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            animi.SetTrigger("Perto");
        }
    }
}


