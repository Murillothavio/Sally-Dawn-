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
    [HideInInspector]
    public Color[] Locais;
    [SerializeField]
    private string CodigoTxt;
    private float NextCod = 0;
    [HideInInspector]
    public Transform[] PontosAmbiente = new Transform[7];
    private Vector3[] PontoscCord = new Vector3[7];

    public KeyCode TrocarAmbiente, TrocarSkin, InstaciarFases;
    public KeyCode[] PecasKey = new KeyCode[7];

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

        if (Input.GetKeyDown(TrocarAmbiente))
            GetComponent<Ambiente>().TrocaAmbiente();
        
        if (Input.GetKeyDown(TrocarSkin))
            GetComponent<Ambiente>().TrocaConfig();

        if (Input.GetKeyDown(InstaciarFases))
            GetComponent<CriarFases>().Criar = true;

        #region ColetaPcas
        for (int i = 0; i < PecasKey.Length; i++)
            if (Input.GetKeyDown(PecasKey[i]))
                switch (i)
                {
                    case 0:
                        if (GetComponent<Eventos>().PwrUp.Neutro)
                            GetComponent<Eventos>().PwrUp.Neutro = false;
                        else
                            GetComponent<Eventos>().PwrUp.Neutro = true;
                        break;
                    case 1:
                        if (GetComponent<Eventos>().PwrUp.Alegre)
                            GetComponent<Eventos>().PwrUp.Alegre = false;
                        else
                            GetComponent<Eventos>().PwrUp.Alegre = true;
                        break;
                    case 2:
                        if (GetComponent<Eventos>().PwrUp.Triste)
                            GetComponent<Eventos>().PwrUp.Triste = false;
                        else
                            GetComponent<Eventos>().PwrUp.Triste = true;
                        break;
                    case 3:
                        if (GetComponent<Eventos>().PwrUp.Raiva)
                            GetComponent<Eventos>().PwrUp.Raiva = false;
                        else
                            GetComponent<Eventos>().PwrUp.Raiva = true;
                        break;
                    case 4:
                        if (GetComponent<Eventos>().PwrUp.Nojo)
                            GetComponent<Eventos>().PwrUp.Nojo = false;
                        else
                            GetComponent<Eventos>().PwrUp.Nojo = true;
                        break;
                    case 5:
                        if (GetComponent<Eventos>().PwrUp.Medo)
                            GetComponent<Eventos>().PwrUp.Medo = false;
                        else
                            GetComponent<Eventos>().PwrUp.Medo = true;
                        break;
                    case 6:
                        if (GetComponent<Eventos>().PwrUp.Etereo)
                            GetComponent<Eventos>().PwrUp.Etereo = false;
                        else
                            GetComponent<Eventos>().PwrUp.Etereo = true;
                        break;
                }
        #endregion
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
