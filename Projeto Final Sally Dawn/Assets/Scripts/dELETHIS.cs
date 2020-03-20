using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dELETHIS : MonoBehaviour
{
    public bool Segura;
    public float Speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Input.GetAxis("HorizontalA");
        transform.position += Vector3.up * Input.GetAxis("VerticalA");

        Segura = (Input.GetKey(KeyCode.Space));
        
    }
}
