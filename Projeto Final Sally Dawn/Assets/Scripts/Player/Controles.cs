using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controles : MonoBehaviour
{
    [System.Serializable]
    public class Conjutos
    {
        [HideInInspector]
        public string name;
        public string VERTICAL_AXIS, HORIZONTAL_AXIS;
        public KeyCode PULAR_BUTTON, INTERAGIR_BUTTON, AGACHAR_BUTTON, SEGURAR_BUTTON, AGARRAR_BUTTON, BOMBA_BUTTON;
    }
    public string[] Acoes = new string[]  
    { "PULAR_BUTTON", "INTERAGIR_BUTTON", "AGACHAR_BUTTON", "SEGURAR_BUTTON", "AGARRAR_BUTTON", "BOMBA_BUTTON" };
    // [HideInInspector]
    public Conjutos[] ControleOpcoes;
    public enum GetConjunto { ConjuntoA, ConjuntoB, ConjuntoCustom }
    public GetConjunto SetConjunto;

    public Conjutos ControleAtual;

    public float Vertical, Horizontal;
    // Start is called before the first frame update
    void Start()
    {
        ControleOpcoes[2] = ControleOpcoes[0];
        TrocarConjuto();
    }
    void TrocarConjuto()
    {
        int indexConjuto = 0;
        switch (SetConjunto)
        {
            case GetConjunto.ConjuntoA:
                indexConjuto = 0;
                break;
            case GetConjunto.ConjuntoB:
                indexConjuto = 1;
                break;
            case GetConjunto.ConjuntoCustom:
                indexConjuto = 2;
                break;
        }
        ControleAtual = ControleOpcoes[indexConjuto];
    }
    // Update is called once per frame
    void Update()
    {
        TrocarConjuto();
        Horizontal = Input.GetAxis(ControleAtual.HORIZONTAL_AXIS);
        Vertical = Input.GetAxis(ControleAtual.VERTICAL_AXIS);
    }
    public bool GetPressButton(string b)
    {
        KeyCode keypress = KeyCode.None;
        switch (b)
        {
            case "AGACHAR_BUTTON":
                keypress = ControleAtual.AGACHAR_BUTTON;
                break;
            case "AGARRAR_BUTTON":
                keypress = ControleAtual.AGARRAR_BUTTON;
                break;
            case "BOMBA_BUTTON":
                keypress = ControleAtual.BOMBA_BUTTON;
                break;
            case "INTERAGIR_BUTTON":
                keypress = ControleAtual.INTERAGIR_BUTTON;
                break;
            case "PULAR_BUTTON":
                keypress = ControleAtual.PULAR_BUTTON;
                break;
            case "SEGURAR_BUTTON":
                keypress = ControleAtual.SEGURAR_BUTTON;
                break;
        }
        if (keypress == KeyCode.None)
        {
            Debug.LogError("Key Null");
            return false;
        }
        else
        {
            if (keypress == ControleAtual.PULAR_BUTTON)
                return (Input.GetKeyDown(keypress));
            else
                return (Input.GetKey(keypress));
        }

    }
    public float GetPressAxis(string axis)
    {
        if (axis == ControleAtual.HORIZONTAL_AXIS)
            return Horizontal;
        else if (axis == ControleAtual.VERTICAL_AXIS)
            return Vertical;
        else return 0;
    }
}
