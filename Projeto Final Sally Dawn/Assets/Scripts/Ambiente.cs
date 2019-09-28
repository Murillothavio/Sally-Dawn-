using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambiente : MonoBehaviour
{
    public enum Fase { Neutro, Alegre, Triste, Raiva, Nojo, Medo, Etereo}
    public Fase ambiente;
    public AudioClip[] som= new AudioClip[7];
    private AudioSource AudSrc;

    #region config
    public float TempoOcioso;
    public float MaxOcioso = 10;
    [Range(0, 6)]
    public float QlAnimaOcioso;
    public float AnimaOcioso = 5;
    public bool Segurando, Escalando, Caindo;//*
    public float moveSpeed = 10;
    [Range(5, 15)]
    public float jumpforce = 10;
    [Range(5, 15)]
    public float walkSpeed = 10;
    [Range(5, 10)]
    public float crawlSpeed = 7;
    [Range(5, 10)]
    public float climbSpeed = 8;
    [Range(10, 30)]
    public float runSpeed;
    [Range(5, 10)]
    public float PullshSpeed;
    [Range(0, 2)]
    public float currentSpeed = .5f;
    [Range(1, 7)]
    public float fallMultiplier = 2.5f;
    [Range(1, 7)]
    public float lowJumpMultiplier = 2f;
    #endregion
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
