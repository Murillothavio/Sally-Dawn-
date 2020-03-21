using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dELETHIS : MonoBehaviour
{
    public bool Segura, segurando,s;
    public float Speed = 10;
    public GameObject Centro;
    public float Angulo, Distancia;
    public Vector3 Coeficiente, DeltaPosi;

    // Update is called once per frame
    void Update()
    {


        Angulo = Centro.GetComponent<Balancar>().CurrentAng;
        float Ang = Mathf.Deg2Rad * Angulo;
        Coeficiente = new Vector3(Mathf.Sin(Ang), -1* Mathf.Cos(Ang), 0);
        DeltaPosi = Coeficiente * Distancia;

        Segura = (Input.GetKey(KeyCode.Space));
        segurando = Segura;
        transform.position += Vector3.up * Input.GetAxis("VerticalA");
        if (!segurando)
            transform.position += Vector3.right * Input.GetAxis("HorizontalA");

        if ((segurando && DeltaPosi == Vector3.zero) || Input.GetAxis("VerticalA") != 0)
            CalcDistancia();

        if (segurando)
        {
            transform.eulerAngles = new Vector3(0, 0, Angulo);
            if (s)
                transform.position = DeltaPosi + Centro.transform.position;
        }
    }

    void CalcDistancia()
    {
        Vector3 Delta = transform.position - Centro.transform.position;

        Distancia = Mathf.Sqrt((Delta.x * Delta.x) + (Delta.y * Delta.y));
    }
}
