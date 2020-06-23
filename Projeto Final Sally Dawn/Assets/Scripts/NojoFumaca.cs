using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class NojoFumaca : MonoBehaviour
{
    public GameObject Maquina;
    public Material sh;
    public MeshRenderer mr;
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
    }

    // Update is called once per frame
    void Update()
    {
        Ativo = (Maquina != null);

        dissolve = Ativo ? 1 : 0;
        dissolve = Mathf.MoveTowards(dissolve, (Ativo ? 1 : 0), Time.deltaTime);

        Filho.SetActive(Ativo);
        sh.SetFloat("_Disolve", dissolve);
        mr.enabled = (dissolve != 0);
    }
}
