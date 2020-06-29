using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool Salvo;
    [Range(0, 1)] public float Cor;
    public MeshRenderer[] mr;
    public Material[] material;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            mr[i] = transform.GetChild(0).GetChild(i).GetComponent<MeshRenderer>();
            material[i] = mr[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Salvo = (GameMaster.gm._savepointsmenu == transform);

        Cor = Mathf.MoveTowards(Cor, (Salvo ? 1 : 0),Time.deltaTime);
        for (int i = 0; i < material.Length; i++)
        {
            material[i].SetFloat("_TransicaoGirassol", Cor);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!Salvo)
            {
                Debug.Log("salvaar");
                GameMaster.gm._savepointsmenu = transform;
                GameMaster.gm.SavePlayer();
            }
        }
    }
}
