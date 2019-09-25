using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simplejump : MonoBehaviour
{
    public bool IsJump;
    public float Ground;
    private Rigidbody rb;
    private Animator anima;
    private bool IsGround;
    public float PousoTime;
    public bool DelayGround;
    [Range(0, 5)]
    public float PousoDelay = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        anima = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
            IsJump = true;
        if (IsJump)
        {
            IsJump = false;
            var vel = rb.velocity;
            vel.y = 10;
            rb.velocity = vel;
            anima.SetTrigger("j");

        }

    }
    private void FixedUpdate()
    {
        IsGround = (rb.transform.position.y <= 0);
    //    if (IsGround)
    //        PousoTime += Time.deltaTime;
    ////    else if(rb.velocity.y>0)
    //  //      PousoTime = 0;
    //    if (PousoTime > PousoDelay)
    //        DelayGround = true;
    //    else
    //        DelayGround = false;


        float vymin = -1, vymax = 1, vy = rb.velocity.y;
        if (vy != 0)
        {
            PousoTime = 0;

            if (vymin/10 > vy)
                vy = vymin;
            if (vy > vymax/10)
                vy = vymax;
        }
        Ground = DelayGround ? 1 : 0;
        anima.SetFloat("Blend", Ground);
        anima.SetFloat("vy", vy);
        anima.SetBool("J", IsGround);
    }
}
