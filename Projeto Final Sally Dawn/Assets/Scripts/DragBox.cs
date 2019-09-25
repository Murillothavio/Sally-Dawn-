using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBox : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject play;
    public bool seg;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        play = GameObject.Find("Player");
        seg = play.GetComponent<AndarPlayer>().Segurando;
    }

    // Update is called once per frame
    void Update()
    {
        seg = play.GetComponent<AndarPlayer>().Segurando;
        if(!seg)
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        else
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
}
