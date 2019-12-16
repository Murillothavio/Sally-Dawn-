using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_escada : MonoBehaviour
{
    public CameraShake cmShake;
    public float Shake;
    private GameObject gbFilho;
    public bool Ativo, IsBlue, Ativando = true;
    [HideInInspector]
    public Vector3 PontoAtual, PontoRed, PontoBlue, Ponto0;
    [HideInInspector]
    public float AnguloAtual, AnguloRed, AnguloBlue;
    [HideInInspector]
    public float VeloLinear, VeloAngular, DelaTime, GoToH, GoToA;

    void Start()
    {
        gbFilho = transform.GetChild(0).gameObject;
        PontoRed.y += transform.position.y;
        PontoBlue.y += transform.position.y;
        Ponto0 = transform.position;
        PontoAtual = Ponto0;
        PontoAtual.x = PontoRed.x;
    }

    void Update()
    {
        if (Ativo)
        {
            if (IsBlue)
            {
                GoToH = PontoBlue.y;
                GoToA = AnguloBlue;
            }
            else
            {
                GoToH = PontoRed.y;
                GoToA = AnguloRed;
            }
        }
        else
        {
            GoToH = Ponto0.y;
            GoToA = 0;
        }

        if (Ativando)
        {
            VeloLinear = Mathf.Abs((Ponto0.y - PontoRed.y) / DelaTime);
            VeloAngular = Mathf.Abs((0 - AnguloRed) / DelaTime);
        }
        else
        {
            VeloLinear = Mathf.Abs((PontoBlue.y - PontoRed.y) / DelaTime);
            VeloAngular = Mathf.Abs((AnguloBlue - AnguloRed) / DelaTime);
        }

        if (GoToH == Ponto0.y)
            Ativo = true;
        else if ((PontoAtual == PontoBlue || PontoAtual == PontoRed) && (GoToH != Ponto0.y))
            Ativando = false;

        PontoAtual.y = Mathf.MoveTowards(PontoAtual.y, GoToH, VeloLinear * Time.deltaTime);
        AnguloAtual = Mathf.MoveTowards(AnguloAtual, GoToA, VeloAngular * Time.deltaTime);

        if (PontoAtual.y != GoToH)
            cmShake.shakeAmount = Shake / 100;

        transform.position = PontoAtual;
        gbFilho.transform.rotation = Quaternion.Euler(Vector3.forward * AnguloAtual);
    }
}
