using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontes : MonoBehaviour
{
    public bool Ativo;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
            anim.SetBool("Abrir", !Ativo);
        else
            Debug.LogError("sem animator da ponte");
    }

    private void OnTriggerEnter(Collider other)
    {
        Ativo = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Ativo = false;
    }
}
