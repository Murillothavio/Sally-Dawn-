using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_escada : MonoBehaviour
{
    public CameraShake cmShake;
    public float Shake;
    private Transform gbFilho;
    public bool Ativo, IsBlue, Ativando = true;
    [HideInInspector]
    public Vector3 PontoAtual, PontoRed, PontoBlue, Ponto0;
    [HideInInspector]
    public float AnguloAtual, AnguloRed, AnguloBlue;
    [HideInInspector]
    public float VeloLinear, VeloAngular, DelaTime, GoToH, GoToA;

    void Start()
    {
        gbFilho = transform.GetChild(0).gameObject.transform;
        Transform gbColuna = transform.GetChild(1).gameObject.transform;
        float DeltaColuna = gbFilho.position.y - gbColuna.position.y;
        PontoRed.y += transform.position.y + DeltaColuna;
        PontoBlue.y += transform.position.y + DeltaColuna;
        Ponto0 = transform.position;
        PontoAtual = Ponto0;
        PontoAtual.x = PontoRed.x;
        if (cmShake == null)
            Debug.LogError("No Cam Shake");
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
            VeloLinear = Mathf.Abs((Ponto0.y - PontoRed.y) / DelaTime);//TODO modificar
            VeloAngular = Mathf.Abs((0 - AnguloRed) / DelaTime);
        }
        else
        {
            VeloLinear = Mathf.Abs((PontoBlue.y - PontoRed.y) / DelaTime);
            VeloAngular = Mathf.Abs((AnguloBlue - AnguloRed) / DelaTime);
        }

        if (GoToH == Ponto0.y)
            Ativando = true;
        else if ((PontoAtual.y == PontoBlue.y || PontoAtual.y == PontoRed.y) && (GoToH != Ponto0.y))
            Ativando = false;

        PontoAtual.y = Mathf.MoveTowards(PontoAtual.y, GoToH, VeloLinear * Time.deltaTime);
        AnguloAtual = Mathf.MoveTowards(AnguloAtual, GoToA, VeloAngular * Time.deltaTime);

        if (PontoAtual.y != GoToH)
            cmShake.shakeAmount = Shake / 100;

        transform.position = PontoAtual;
        gbFilho.rotation = Quaternion.Euler(Vector3.forward * AnguloAtual);
    }
}
