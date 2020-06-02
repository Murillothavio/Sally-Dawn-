using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBox : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject play;
    public bool seg, encostado;
    [SerializeField]
    private Vector3 DeltaPosi;
    [Header("_____________")]
    public bool Draw;
    [Range(1, 100)]
    public float MaxRange = 30;
    private float MinRange = 5;
    public Vector3 Inicial;
    public float DistanciaAtual, DistanP;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        play = GameObject.Find("Player");
        seg = play.GetComponent<AndarPlayer>().Segurando;
        Inicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DistanciaAtual = CalcularDistBox(gameObject);
        if (DistanciaAtual > MaxRange)
        {
            DistanP = CalcularDistBox(play);
            if (!(DistanP < MinRange))
                transform.position = Inicial;
        }

        seg = play.GetComponent<AndarPlayer>().Segurando;
        if (seg && encostado)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            if (DeltaPosi == Vector3.zero)
            {
                DeltaPosi = play.transform.position - transform.position;
            }
            //rb.velocity = play.GetComponent<Rigidbody>().velocity;
            Vector3 tp = play.transform.position - DeltaPosi;
            transform.position = tp;
        }
        else if (encostado)
        {
            DeltaPosi = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            DeltaPosi = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == play)
            encostado = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == play)
            encostado = false;
    }


    public float CalcularDistBox(GameObject a)
    {
        float CatetoX = a.transform.position.x - Inicial.x;
        float CatetoY = a.transform.position.y - Inicial.y;

        return Mathf.Sqrt(CatetoX * CatetoX + CatetoY * CatetoY);
    }
    private void OnDrawGizmos()
    {
        if (Draw)
        {
            Vector3 ini;
            if (Inicial == Vector3.zero)
                ini = transform.position;
            else
                ini = Inicial;
            Gizmos.color = new Color(0, 1, 0, .5f);
            Gizmos.DrawSphere(ini, MaxRange);

            Gizmos.color = new Color(0, 0, 1, .9f);
            Gizmos.DrawSphere(ini, MinRange);
        }
    }
}
