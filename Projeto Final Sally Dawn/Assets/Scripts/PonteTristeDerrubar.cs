using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PonteTristeDerrubar : MonoBehaviour
{
    public bool Stop = true;
    public PonteTriste[] Tabuas;
    public GameObject PrefabTabua;
    public Transform[] Pontos = new Transform[2];
    public int numTabua;
    public float DeltaX, DeltaY, distancia;

    // Start is called before the first frame update
    void Start()
    {
        if (Pontos[0] != null && Pontos[1] != null)
            distancia = GameMaster.CalcularDist(Pontos[0], Pontos[1]);
        else Debug.LogError("Sem pontos de distancia");
        numTabua = (int)(distancia / DeltaX) + 1;


        Tabuas = new PonteTriste[numTabua];
        for (int i = 0; i < numTabua; i++)
        {
            Vector3 pos = Pontos[0].position;
            pos.x += i * DeltaX;
            GameObject t = Instantiate(PrefabTabua, pos, Quaternion.identity) as GameObject;
            t.transform.parent = transform;
            Tabuas[i] = t.GetComponent<PonteTriste>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Stop = false;
            for (int i = 0; i < Tabuas.Length; i++)
            {
                Tabuas[i].Stop = Stop;
            }
        }
    }
}
