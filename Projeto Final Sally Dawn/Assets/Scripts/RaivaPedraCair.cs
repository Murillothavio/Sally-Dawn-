using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaivaPedraCair : MonoBehaviour
{
    public bool Afudar;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("morte", Afudar);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Afudar = true;
        }
    }
}
