using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NojoFumaca : MonoBehaviour
{
    public GameObject Maquina;
    public Material sh;
    public bool Ativo;
    [SerializeField]
    private float dissolve;
    private GameObject Filho;

    // Start is called before the first frame update
    void Start()
    {
        if (Maquina == null)
            Debug.LogError("Sem Maquina de Nojo");

        sh = GetComponent<MeshRenderer>().material;
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

        Filho.SetActive(Ativo);
        sh.enableInstancing = Ativo;
    }
}
