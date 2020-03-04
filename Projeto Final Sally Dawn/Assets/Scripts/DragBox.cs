using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBox : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject play;
    public bool seg;
    [SerializeField]
    private Vector3 DeltaPosi;
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
        if (!seg)
        {
            DeltaPosi = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

            if (DeltaPosi == Vector3.zero)
            {
                DeltaPosi = play.transform.position - transform.position;
            }
            //rb.velocity = play.GetComponent<Rigidbody>().velocity;
            Vector3 tp = play.transform.position - DeltaPosi;
            transform.position = tp;
            Debug.Log( tp + ".");
        }
    }
}
