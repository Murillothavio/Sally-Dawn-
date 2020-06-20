using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiRatoAni : MonoBehaviour
{
    public bool Corra, Reset, Ataque;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Corra", Corra);

        anim.SetBool("Reset", Reset);

        anim.SetBool("Ataque", Ataque);
    }
}
