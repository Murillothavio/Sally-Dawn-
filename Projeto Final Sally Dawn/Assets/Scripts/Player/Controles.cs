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
    public enum Acoes { PULAR_BUTTON, INTERAGIR_BUTTON, AGACHAR_BUTTON, SEGURAR_BUTTON, AGARRAR_BUTTON, BOMBA_BUTTON }
   // [HideInInspector]
    public Conjutos[] ControleOpcoes;
    public enum GetConjunto { ConjuntoA,ConjuntoB,ConjuntoCustom}
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
        switch (SetConjunto) {
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
    public bool GetPressButton(Acoes b) {
        KeyCode keypress = KeyCode.None;
        switch (b)
        {
            case Acoes.AGACHAR_BUTTON:
                keypress = ControleAtual.AGACHAR_BUTTON;
                break;
            case Acoes.AGARRAR_BUTTON:
                keypress = ControleAtual.AGARRAR_BUTTON;
                break;
            case Acoes.BOMBA_BUTTON:
                keypress = ControleAtual.BOMBA_BUTTON;
                break;
            case Acoes.INTERAGIR_BUTTON:
                keypress = ControleAtual.INTERAGIR_BUTTON;
                break;
            case Acoes.PULAR_BUTTON:
                keypress = ControleAtual.PULAR_BUTTON;
                break;
            case Acoes.SEGURAR_BUTTON:
                keypress = ControleAtual.SEGURAR_BUTTON;
                break;
        }
        if (keypress == KeyCode.None)
        {
            Debug.LogError("Key Null");
            return false;
        }
        else
            return (Input.GetKeyDown(keypress));
        
    }

}
