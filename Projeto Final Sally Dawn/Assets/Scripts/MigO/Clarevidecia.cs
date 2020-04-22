using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clarevidecia : MonoBehaviour
{
    private Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== "SystemEsconder")
            Anim.SetBool("Perto", true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SystemEsconder")
            Anim.SetBool("Perto", false);
    }
}
