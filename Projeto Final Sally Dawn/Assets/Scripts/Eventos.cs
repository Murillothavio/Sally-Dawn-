using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eventos : MonoBehaviour
{
    public Emocoes Peças = new Emocoes();
    public Emocoes PwrUp = new Emocoes();
    public enum Fase { Neutro, Alegre, Triste, Raiva, Nojo, Medo, Etereo }
    public Fase ambiente;

    public Text txt;
    private string stg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stg = "E:";
        stg += Peças.Neutro ? 1 : 0;
        stg += Peças.Alegre ? 1 : 0;
        stg += Peças.Triste ? 1 : 0;
        stg += Peças.Raiva ? 1 : 0;
        stg += Peças.Nojo ? 1 : 0;
        stg += Peças.Medo ? 1 : 0;
        stg += Peças.Etereo ? 1 : 0;
        stg += "// P: ";
        stg += PwrUp.Neutro ? 1 : 0;
        stg += PwrUp.Alegre ? 1 : 0;
        stg += PwrUp.Triste ? 1 : 0;
        stg += PwrUp.Raiva ? 1 : 0;
        stg += PwrUp.Nojo ? 1 : 0;
        stg += PwrUp.Medo ? 1 : 0;
        stg += PwrUp.Etereo ? 1 : 0;

        if (txt != null)
            txt.text = stg;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ItsItem>() != null)
        {
            ambiente = (Fase)GetComponent<Ambiente>().ambiente;
            if (other.GetComponent<ItsItem>().typeItem == ItsItem.TypeItem.Emotion)
                Coletar(Peças);
            else
            if (other.GetComponent<ItsItem>().typeItem == ItsItem.TypeItem.PowerUp)
                Coletar(PwrUp);
        }
    }

    private void Coletar(Emocoes eme)
    {

        if (ambiente == Fase.Neutro)
            eme.Neutro = true;
        else if (ambiente == Fase.Alegre)
            eme.Alegre = true;
        else if (ambiente == Fase.Triste)
            eme.Triste = true;
        else if (ambiente == Fase.Raiva)
            eme.Raiva = true;
        else if (ambiente == Fase.Nojo)
            eme.Nojo = true;
        else if (ambiente == Fase.Medo)
            eme.Medo = true;
        else if (ambiente == Fase.Etereo)
            eme.Etereo = true;
    }
}
