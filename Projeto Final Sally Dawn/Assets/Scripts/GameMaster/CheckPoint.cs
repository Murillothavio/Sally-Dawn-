﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public bool Salvo;
    public float Cor;
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
        Cor = Mathf.MoveTowards(Cor, (Salvo ? 1 : 0), 0.5f);
        // for (int i = 0; i < material.Length; i++)
        //{
        material[0].SetFloat("_TransicaoGirassol", Cor);
       // }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("salvaar");
            GameMaster.gm._savepointsmenu = transform;
            GameMaster.gm.SavePlayer();
        }
    }
}
