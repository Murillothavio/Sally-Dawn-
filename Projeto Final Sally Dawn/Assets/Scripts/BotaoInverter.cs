﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoInverter : MonoBehaviour
{
    public bool DesAtivo;
    private Animator anim;

    public float CDtmp = 1;
    private float currTemp;
    private bool Mudou;
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("aaaaaa botao inverter" + gameObject.name);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
            anim.SetBool("Desativar", DesAtivo);
        if (Mudou)
        {
            currTemp += Time.deltaTime;
            if (currTemp > CDtmp)
            {
                currTemp = 0;
                Mudou = false;
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(other.GetComponent<AndarPlayer>().cntr.ControleAtual.INTERAGIR_BUTTON))
                if (!Mudou)
                {
                    Mudou = true;
                    GameMaster.gm.InverterPlat();
                }
        }
    }
}
