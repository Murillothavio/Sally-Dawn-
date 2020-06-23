using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_Botao : MonoBehaviour
{
    public bool Ativo;
    public MeshRenderer mr;
    public Material mat;
    public float Inter;
    private string Slide = "_Interpolation";

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
    }

    // Update is called once per frame
    void Update()
    {
        Inter = Mathf.MoveTowards(Inter, ((Ativo) ? 0 : 1), Time.deltaTime);
        mat.SetFloat(Slide, Inter);
        
    }
}
