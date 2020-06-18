using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleTransporte : MonoBehaviour
{
    public Transform OutraParte;
    [SerializeField]
    private Transform Saida;
    private float Zposi;

    // Start is called before the first frame update
    void Start()
    {
        // OutraParte = transform;
        Montar();
    }

    void Montar()
    {
        if (OutraParte != null)
        {
            if (OutraParte.name == "Entrada")
                Saida = OutraParte.GetChild(0);
            else
            {
                for (int i = 0; i < OutraParte.childCount; i++)
                    if (OutraParte.GetChild(i).name == "Entrada")
                    {
                        Saida = OutraParte.GetChild(i).GetChild(0);
                        OutraParte.GetChild(i).GetComponent<TeleTransporte>().OutroLado(transform);
                    }
            }
        }
    }

    public void OutroLado(Transform Ligacao)
    {
        if (OutraParte == null)
            OutraParte = Ligacao;
        if (OutraParte != null)
            Montar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (OutraParte == null)
                Debug.LogError("Sem portal");
            if (Saida == null)
                Debug.Log("Sem saida do portal");

            Zposi = other.transform.position.z;
            Debug.Log(" cant walk");

            Vector3 NovPosi = Saida.position;
            NovPosi.z = Zposi;

            other.transform.position = NovPosi;

        }
    }
}
