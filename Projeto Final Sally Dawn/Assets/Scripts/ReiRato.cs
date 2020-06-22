using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReiRato : MonoBehaviour
{
    public bool Corra, Reset, SetT, Gambiarra;
    private Animator anim;
    public GameObject ObjAttack, ObjIdle;
    public float TimeAttack;
    public bool Attacking;

    public Vector3 Posi;
    public float Correcao;

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
                anim.SetTrigger("Ataque");
                Corra = false;
                Gambiarra = false;
                Attacking = true;
                Invoke("Stop", TimeAttack);
            }
            else
            {
                Corra = true;
                anim.SetTrigger("Corra");
            }
        }

        if (!Attacking)
        {
            Posi = ObjIdle.transform.position;
            Posi.y += Correcao;
            ObjAttack.transform.position = Posi;
        }
        ObjAttack.SetActive(Attacking);
        ObjIdle.SetActive(!Attacking);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SetT = true;
        }
    }
    void Stop()
    {
        Attacking = false;
        Debug.Log("To Do Kill");
    }
   /* private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Gambiarra)
                SetT = true;
            else
                Gambiarra = true;
        }
    }*/
}
