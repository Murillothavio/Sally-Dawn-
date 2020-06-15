using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pontes : MonoBehaviour
{
    public bool Ativo;
    public Animator anim;
    [SerializeField]
    private Collider Pressao;

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
        Pressao = other;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == Pressao)
            Ativo = false;
    }
}
