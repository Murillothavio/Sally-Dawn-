using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ambiente))]
public class Codigos : MonoBehaviour
{
    [Header("Levar ate ambiente")]
    public bool Pull;
    [SerializeField]
    private Color Local;
    public Color[] Locais;
    [SerializeField]
    private string CodigoTxt;
    private float NextCod = 0;
    [HideInInspector]
    public Transform[] PontosAmbiente = new Transform[7];
    private Vector3[] PontoscCord = new Vector3[7];

    private void Awake()
    {
        for (int i = 0; i < PontosAmbiente.Length; i++)
        {
            PontoscCord[i] = PontosAmbiente[i].position;
        }
        LevarPonto();
    }

    // Update is called once per frame
    void Update()
    {
        Local = GetComponent<Ambiente>().CorAmbiente;
        #region Chamar NEXT
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.N))
                NextCod = 1;
            if (Input.GetKeyDown(KeyCode.E))
                if (NextCod == 1)
                    NextCod++;
                else
                    NextCod = 0;
            if (Input.GetKeyDown(KeyCode.X))
                if (NextCod == 2)
                    NextCod++;
                else
                    NextCod = 0;
            if (Input.GetKeyDown(KeyCode.T))
                if (NextCod == 3)
                    NextCod++;
                else
                    NextCod = 0;
        }
        switch (NextCod)
        {
            case 0:
                CodigoTxt = "";
                break;
            case 1:
                CodigoTxt = "" + NextCod + "N";
                break;
            case 2:
                CodigoTxt = "" + NextCod + "NE";
                break;
            case 3:
                CodigoTxt = "" + NextCod + "NEX";
                break;
            case 4:
                CodigoTxt = "" + NextCod + "NEXT";
                GetComponent<Ambiente>().TrocaAmbiente();
                Invoke("LevarPonto", 1f);
                NextCod = 0;
                break;
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.O))
            GetComponent<Ambiente>().TrocaAmbiente();
        if (Input.GetKeyDown(KeyCode.L))
            GetComponent<CriarFases>().Criar = true;
    }

    void LevarPonto()
    {
        if (Pull)
        {
            for (int i = 0; i < Locais.Length; i++)
            {
                if (Locais[i] == Local)
                    transform.position = PontoscCord[i];
            }
        }
        else Debug.Log("nada");
    }
}
