using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarFases : MonoBehaviour
{
    public bool inAtivo,Criar;
   // [HideInInspector]
    public GameObject[] Vfase, Vtrans;
    [Range(0, 6)]
    public float faseIndex;
    private int IndexFase;
    //public KeyCode kc;
    void Start()
    {
        Vtrans = Resources.LoadAll<GameObject>("Pref_Trans");//Busca os prefabs
        Vfase = Resources.LoadAll<GameObject>("Pref_Fase");  //
        if (!inAtivo)
        {
            ApagarBack();
            GetComponent<Ambiente>().TrocaConfig();
            Invoke("ApagarBack", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        IndexFase = (int)faseIndex; ;
        faseIndex = IndexFase;
        if (!inAtivo )
        {
            if (Criar)
            {
                GetComponent<Ambiente>().TrocaConfig();
                ApagarBack();
            }
            Criar = false;
        }

    }
    void ApagarBack()
    {

        if (IndexFase == 0)
        {
            for (int i = 1; i < Vfase.Length; i++)
                Destroy(GameObject.Find(Vfase[i].name));
            for (int i = 1; i < Vtrans.Length; i++)
                Destroy(GameObject.Find(Vtrans[i].name));
        }
        else
        {
            Destroy(GameObject.Find(Vfase[Mathf.Clamp(IndexFase - 2, 0, Vfase.Length)].name + "(Clone)"));
            if (IndexFase > 1)
            {
                Destroy(GameObject.Find(Vtrans[Mathf.Clamp(IndexFase - 3, 0, Vtrans.Length)].name + "(Clone)"));
                Destroy(GameObject.Find(Vtrans[Mathf.Clamp(IndexFase - 3, 0, Vtrans.Length)].name));
            }
            CriarNext();
        }
    }
    void CriarNext()
    {
        Criar = false;
        Destroy(GameObject.Find(Vfase[IndexFase].name + "(Clone)"));
        if ((IndexFase<6))
        Destroy(GameObject.Find(Vtrans[IndexFase].name + "(Clone)"));

        Instantiate(Vfase[IndexFase]);
        if ((IndexFase<6))
        Instantiate(Vtrans[IndexFase]);
    }
}

