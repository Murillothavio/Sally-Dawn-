using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public enum Telas { TelasInicial, TelaPrincipal, TelaJogar, TelaNovo, TelaContinuar, TelaJogo, TelaMenu, JanelaLembranca, JanelaOpcoes, TelaCredito, TelaSair }
    public Telas tela;
    public int nLembr;
    public GameObject[] DescriLemb = new GameObject[7];
    public bool[] TemLembr = new bool[6];
    public int nOpcoes;
    public GameObject[] JanOpcoes = new GameObject[4];
    public GameObject TelasInicial, TelaPrincipal, TelaJogar, TelaNovo, TelaContinuar, TelaJogo, TelaMenu, JanelaLembranca, JanelaOpcoes, TelaCredito, TelaSair;
    // Start is called before the first frame update
    void Start()
    {
        TrocarTela();
    }

    // Update is called once per frame
    void Update()
    {
        if (tela == Telas.TelasInicial)
            if (Input.anyKeyDown)
                tela = Telas.TelaJogo;///principal

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (tela == Telas.TelaJogo)
                tela = Telas.TelaMenu;
            else if (tela == Telas.TelaMenu)
                tela = Telas.TelaJogo;
            TrocarTela();
        }
        if (tela != Telas.TelaJogo)
            TrocarTela();
    }
    private void TrocarTela()
    {
        if (TelasInicial != null)
            TelasInicial.SetActive(tela == Telas.TelasInicial);
        if (TelaPrincipal != null)
            TelaPrincipal.SetActive(tela == Telas.TelaPrincipal);
        if (TelaJogar != null)
            TelaJogar.SetActive(tela == Telas.TelaJogar);
        if (TelaNovo != null)
            TelaNovo.SetActive(tela == Telas.TelaNovo);
        if (TelaContinuar != null)
            TelaContinuar.SetActive(tela == Telas.TelaContinuar);
        if (TelaJogo != null)
            TelaJogo.SetActive(tela == Telas.TelaJogo);
        if (TelaMenu != null)
            TelaMenu.SetActive(tela == Telas.TelaMenu);
        if (JanelaLembranca != null)
            JanelaLembranca.SetActive(tela == Telas.JanelaLembranca);
        if (JanelaOpcoes != null)
            JanelaOpcoes.SetActive(tela == Telas.JanelaOpcoes);
        if (TelaCredito != null)
            TelaCredito.SetActive(tela == Telas.TelaCredito);
        if (TelaSair != null)
            TelaSair.SetActive(tela == Telas.TelaSair);
    }
    #region GoTo
    public void GoToTelaSair() { tela = Telas.TelaSair; }
    public void GoToTelasInicial() { tela = Telas.TelasInicial; }
    public void GoToTelaPrincipal() { tela = Telas.TelaPrincipal; }
    public void GoToTelaJogar() { tela = Telas.TelaJogar; }
    public void GoToTelaNovo() { tela = Telas.TelaNovo; }
    public void GoToTelaContinuar() { tela = Telas.TelaContinuar; }
    public void GoToTelaJogo() { tela = Telas.TelaJogo; }
    public void GoToTelaMenu() { tela = Telas.TelaMenu; }
    public void GoToJanelaLembranca() { tela = Telas.JanelaLembranca; }
    public void GoToJanelaOpcoes() { tela = Telas.JanelaOpcoes; }
    public void GoToTelaCredito() { tela = Telas.TelaCredito; }
    #endregion
    public void Sair()
    {
        Application.Quit();
        Debug.Log("saiu");
    }


}
