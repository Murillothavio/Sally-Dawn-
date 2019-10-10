using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriarFases : MonoBehaviour
{
    public bool inAtivo,Criar;
    public GameObject[] Vfase, Vtrans;
    [Range(0, 6)]
    public float faseIndex;
    private int IndexFase;


    // Start is called before the first frame update
    void Start()
    {
        Vtrans = Resources.LoadAll<GameObject>("Pref_Trans");//Busca os prefabs
        Vfase = Resources.LoadAll<GameObject>("Pref_Fase");  //
        if (!inAtivo)
            ApagarBack();
    }

    // Update is called once per frame
    void Update()
    {
        IndexFase = (int)faseIndex; ;
        faseIndex = IndexFase;
        if (!inAtivo )
        {
            if (Input.GetKeyDown(KeyCode.L)|| Criar)
                ApagarBack();
            
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
            Destroy(GameObject.Find(Vfase[IndexFase - 1].name + "(Clone)"));
            if (IndexFase > 1)
            {
                Destroy(GameObject.Find(Vtrans[IndexFase - 2].name + "(Clone)"));
                Destroy(GameObject.Find(Vtrans[IndexFase - 2].name));
            }
            CriarNext();
        }
    }
    void CriarNext()
    {
        Criar = false;
        Destroy(GameObject.Find(Vfase[IndexFase /*+ 1*/].name + "(Clone)"));
        if ((IndexFase<6))
        Destroy(GameObject.Find(Vtrans[IndexFase/* + 1*/].name + "(Clone)"));

        Instantiate(Vfase[IndexFase /*+ 1*/]);
        if ((IndexFase<6))
        Instantiate(Vtrans[IndexFase/* + 1*/]);
    }
}

