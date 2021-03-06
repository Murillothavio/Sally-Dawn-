﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [Range(0, 10)] public float CoolDown = 5, currTime = -1;
    public float speed = 10, atrito = 1, h;
    private bool dest = false;
    private Rigidbody rb;
    public GameObject Boom;
    // Start is called before the first frame update
    void Start()
    {
        currTime = CoolDown;
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed * h);
        speed = Mathf.MoveTowards(speed, 0, atrito);

        if (currTime<0)
        {
            dest = true;
          //  Debug.Log("BUUU");
        }
        if (currTime < -0.5f)
        {
            Destroy(gameObject);
        }

        else currTime -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
       // Debug.Log(other.tag);
        if (dest)
            if (other.gameObject.tag == "Destruir")
            {
         //       Debug.Log(other.gameObject.name);
                Destroy(other.gameObject);
            }
    }
    private void OnDestroy()
    {
        if (Boom != null)
        {
            GameObject bm = Instantiate(Boom, transform.position, Quaternion.identity) as GameObject;
            Destroy(bm, 2);
        }

    }
}
