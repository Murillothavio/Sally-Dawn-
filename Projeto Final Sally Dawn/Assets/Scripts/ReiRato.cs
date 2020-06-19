using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiRato : MonoBehaviour
{
    public bool Corra, Reset, SetT, Gambiarra;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (SetT)
        {
            SetT = false;
            if (Corra)
            {
                anim.SetTrigger("Reset");
                Corra = false;
                Gambiarra = false;
            }
            else
            {
                Corra = true;
                anim.SetTrigger("Corra");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SetT = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Gambiarra)
                SetT = true;
            else
                Gambiarra = true;
        }
    }
}
