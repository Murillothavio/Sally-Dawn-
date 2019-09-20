using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambiente : MonoBehaviour
{
    public enum Fase { Neutro, Alegre, Triste, Raiva, Nojo, Medo, Etereo}
    public Fase ambiente;
    public AudioClip[] som= new AudioClip[7];
    private AudioSource AudSrc;
    // Start is called before the first frame update
    void Start()
    {
        AudSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        TrocaAmbiente();
        TrocaSom(0);
        AudSrc.enabled = true;

    }
    void TrocaAmbiente()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ambiente == Fase.Neutro)
                ambiente = Fase.Alegre;
            else if (ambiente == Fase.Alegre)
                ambiente = Fase.Triste;
            else if (ambiente == Fase.Triste)
                ambiente = Fase.Raiva;
            else if (ambiente == Fase.Raiva)
                ambiente = Fase.Nojo;
            else if (ambiente == Fase.Nojo)
                ambiente = Fase.Medo;
            else if (ambiente == Fase.Medo)
                ambiente = Fase.Etereo;
            else if (ambiente == Fase.Etereo)
                ambiente = Fase.Neutro;
            AudSrc.enabled = false;
            Debug.Log("oi");
        }

    }
void TrocaSom(int IdSom)
    {
        
        if (ambiente == Fase.Neutro)
            IdSom = 0;
        else if (ambiente == Fase.Alegre)
            IdSom = 1;
        else if (ambiente == Fase.Triste)
            IdSom = 2;
        else if (ambiente == Fase.Raiva)
            IdSom = 3;
        else if (ambiente == Fase.Nojo)
            IdSom = 4;
        else if (ambiente == Fase.Medo)
            IdSom = 5;
        else if (ambiente == Fase.Etereo)
            IdSom = 6;

        AudSrc.clip = som[IdSom];
    }
}
