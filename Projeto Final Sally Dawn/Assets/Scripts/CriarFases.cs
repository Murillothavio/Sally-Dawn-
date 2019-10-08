using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarFases : MonoBehaviour
{
    public bool inAtivo;
    public GameObject[] Vfase, Vtrans;
    [Range(0, 6)]
    public float Fase;
    private int fase;

    // Start is called before the first frame update
    void Start()
    {
        Vtrans = Resources.LoadAll<GameObject>("Pref_Trans");//Busca os prefabs
        Vfase = Resources.LoadAll<GameObject>("Pref_Fase");  //
    }

    // Update is called once per frame
    void Update()
    {
        Fase = (int)Fase;

        if (!inAtivo)
        {
            Debug.Log("work");
        }

    }
}
