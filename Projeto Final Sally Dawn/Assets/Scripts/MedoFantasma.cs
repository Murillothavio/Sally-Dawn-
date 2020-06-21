using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedoFantasma : MonoBehaviour
{
    public float Speed=10;
    public bool IrB, IndoB;
    [Header("---Draw----")]
    public bool Draw;
    private Vector3 PInicial, PAtual;
    [Range(1,75)]
    public float IrFrente, IrtTras;
    private float Ir, DistanciaMenor, DistanciaMaior, DistanciaAtual;
   public float Ang=90;

// Start is called before the first frame update
void Start()
    {
        PInicial = transform.position;
        DistanciaAtual = PInicial.x;
        PAtual = PInicial;
    }

    // Update is called once per frame
    void Update()
    {
        DistanciaMenor = PInicial.x - IrtTras;
        DistanciaMaior = PInicial.x + IrFrente;

        if (DistanciaAtual >= DistanciaMaior)
            IrB = true;
        if (DistanciaAtual <= DistanciaMenor)
            IrB = false;

        if (IndoB!=IrB)
        {
            IndoB = IrB;
            Ang *= -1;
            transform.eulerAngles = new Vector3(0, Ang, 0);
        }

        if (IrB)
            Ir = DistanciaMenor;
        else Ir = DistanciaMaior;

        DistanciaAtual = Mathf.MoveTowards(DistanciaAtual, Ir, Speed);
        PAtual.x = DistanciaAtual;
        transform.position = PAtual;
    }

    private void OnDrawGizmos()
    {

        if (Draw)
        {
            Vector3 p = PInicial;
            if (PInicial == Vector3.zero)
                p = transform.position;

            Vector3 Cum, Cdois;
            Cum = p;
            Cum.x -= (IrtTras / 2);

            Gizmos.color = new Color(1, 0.5f, 0, 0.75f);
            Gizmos.DrawCube(Cum, new Vector3(IrtTras, 1, 1));

            Cdois = p;
            Cdois.x += IrFrente / 2;

            Gizmos.color = new Color(1, 0, 0.5f, 0.75f);
            Gizmos.DrawCube(Cdois, new Vector3(IrFrente, 1, 1));

        }
    }
}
