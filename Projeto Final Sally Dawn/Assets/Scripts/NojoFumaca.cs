using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class NojoFumaca : MonoBehaviour
{
    public GameObject Maquina;
    public Material sh;
    public MeshRenderer mr;
    public Collider cr;
    public bool Ativo;
    [SerializeField]
    private float dissolve;
    private GameObject Filho;

    // Start is called before the first frame update
    void Start()
    {
        if (Maquina == null)
            Debug.LogError("Sem Maquina de Nojo");

        mr = GetComponent<MeshRenderer>();
        sh =mr.material;
        if (sh == null)
            Debug.LogError("SemShader Nojo");

        Filho = transform.GetChild(0).gameObject;
        if (Filho == null)
            Debug.LogError("Sem Morte Fumaca");

        cr = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Ativo = (Maquina != null);

        dissolve = Ativo ? 1 : 0;
        dissolve = Mathf.MoveTowards(dissolve, (Ativo ? 1 : 0), Time.deltaTime);

        if (Filho != null)
        {
            Filho.SetActive(Ativo);
            sh.SetFloat("_Disolve", dissolve);
        }
        if (mr != null)
            mr.enabled = (dissolve != 0);
        if (cr != null)
            cr.enabled = (dissolve != 0);
    }
}
