using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Plataforma_desAtivar : MonoBehaviour
{
    public bool Visivel;
    private bool Mudar;
    public Collider cl;
    [SerializeField]
    private float TargetDisolve, ShDisolve;
    public Material[] Shaders = new Material[2];
    public Material sh;
    [SerializeField]
    private MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        //  gameObject.SetActive(Visivel);
        cl = GetComponent<Collider>();
        if (cl == null)
            Debug.LogError("Sem collider invi");

        mr = GetComponent<MeshRenderer>();
        sh = Shaders[(Visivel) ? 1 : 0];
        if (sh == null)
            Debug.LogError("Sem shader invi");
        else 
        {
            mr.material = sh;
        }

        Mudar = Visivel;
    }

    // Update is called once per frame
    void Update()
    {
        TargetDisolve = (Visivel) ? 1 : -1;
        ShDisolve = Mathf.MoveTowards(ShDisolve, TargetDisolve, Time.deltaTime);

        sh.SetFloat("_Disolve", ShDisolve);

        if (Mudar!=Visivel)
        {
            Mudar = Visivel;
            Invoke("MudarCollider", 1);
        }
    }

    void MudarCollider()
    {
        cl.enabled = Visivel;
    }
}
