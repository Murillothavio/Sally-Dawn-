using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RaivaPedraCair : MonoBehaviour
{
    public bool Resetar, Cair, PedraNo, MeuCaminho;
    private Rigidbody rb;
    private Vector3 Posi;
    public float Ang = 85, Amor;
    public Vector3 Gambi;
    [SerializeField]
    private Transform[] Filho;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Posi = transform.position;

        Filho = new Transform[transform.childCount];
        for (int i = 0; i < Filho.Length; i++)
            Filho[i] = transform.GetChild(i);

    }

    // Update is called once per frame
    void Update()
    {
        if (Resetar)
        {
            Resetar = false;
            if (Gambi == Vector3.zero)
                Gambi = new Vector3(0, 0, Ang);
            transform.eulerAngles = Gambi;
            transform.position = Posi;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            MeuCaminho = false;
        }

        Cair = false;
        for (int i = 0; i < Filho.Length; i++)
        {
            if (Filho[i].GetComponent<RaivaPedraAfundar>().SimEGambiara)
                Cair = true;
        }
        if (Cair)
            cair();

        if (PedraNo && MeuCaminho)
        {
            Vector3 cu = transform.position;
            cu.y -= Amor;
            transform.position = cu;
        }

    }
    void cair()
    {
        MeuCaminho = true;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cair();
        }
    }
}
