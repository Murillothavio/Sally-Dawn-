using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dELETHIS : MonoBehaviour
{
    public GameObject addAttach;
    public bool Segura, segurando, s;
    public float Speed = 10;
    public GameObject Centro;
    public float Angulo, Distancia;
    public Vector3 Coeficiente, DeltaPosi;
    private Rigidbody rg;
    public LayerMask ChaoMove;
    public Transform Plat;
    public Vector3 DeltaPlata;
    private void Start()
    {
        rg = GetComponent<Rigidbody>();
        Plataforma_Attach pa = addAttach.gameObject.AddComponent<Plataforma_Attach>() as Plataforma_Attach;
    }

    // Update is called once per frame
    void Update()
    {
        Angulo = Centro.GetComponent<Balancar>().CurrentAng;
        float Ang = Mathf.Deg2Rad * Angulo;
        Coeficiente = new Vector3(Mathf.Sin(Ang), -1 * Mathf.Cos(Ang), 0);
        #region Movimento
        Segura = (Input.GetKey(KeyCode.Space));
        if (!segurando)
        {
            transform.position += Vector3.up * Input.GetAxis("VerticalA");
            Vector3 v =/*
            transform.position +=*/ Vector3.right * Input.GetAxis("HorizontalA") * Speed * Time.deltaTime;
            rg.velocity = v;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (segurando)
        {
            Vector3 k = Coeficiente;
            k.y *= -1;
            transform.position += Input.GetAxis("VerticalA") * k;
            transform.eulerAngles = new Vector3(0, 0, Angulo);
        }
        #endregion
        #region Calculos

        if ((segurando && Distancia == 0) || Input.GetAxis("VerticalA") != 0)
            CalcDistancia();
        if (!segurando && s)
            if (Plat != null)
                if (Input.GetAxis("VerticalA") + Input.GetAxis("HorizontalA") != 0)
                    DeltaPlata = transform.position - Plat.position;
        #endregion
        #region Atualizacao

        if (segurando)
        {
            DeltaPosi = Coeficiente * Distancia;
            //   if (s)
            transform.position = DeltaPosi + Centro.transform.position;
        }
        if (s)
            transform.position = DeltaPlata + Plat.position;
        #endregion
    }

    void CalcDistancia()
    {
        Vector3 Delta = transform.position - Centro.transform.position;

        Distancia = Mathf.Sqrt((Delta.x * Delta.x) + (Delta.y * Delta.y));
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Zona_agarrar")
        {
            segurando = Segura;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlataformasMovel")
        {
            s = true;
            Plat = other.gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlataformasMovel")
        {
            s = false;
            Plat = null;
        }
    }
}
