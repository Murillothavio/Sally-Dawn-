using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urso : MonoBehaviour
{
    private Transform Player;

    [Range(0,100)]
    public float Distancia, Longe, Perto;

    [HideInInspector]
    public bool EstaLonge, EstaPerto, Andar, LookLeft, Virando;
    [HideInInspector] public float TVirando, IdleV;

    private Rigidbody rb;
    private Animator animPai, animFilho;
    private Transform Filho;
    private Vector3 Look = new Vector3();
    [Range(10, 50)]
    public float Speed;
    public float DistLife;

    [HideInInspector]
    public float temp, soma;

    [HideInInspector]
    public bool Tchau;

    [HideInInspector]
    public float AnimaTchau, AnimaVira;

    [Range(0,3)]
    public float AnimaSpeed = 1;

    [HideInInspector]
    public AnimationClip ClipTchau, ClipVira;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        animPai = GetComponent<Animator>();
        Filho = transform.GetChild(0);
        animFilho = Filho.GetComponent<Animator>();
        AnimaTchau = ClipTchau.length / AnimaSpeed;
        AnimaVira = ClipVira.length / AnimaSpeed;
        
        Look = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        AnimaTchau = ClipTchau.length / AnimaSpeed;
        AnimaVira = ClipVira.length / AnimaSpeed;
        animFilho.speed = AnimaSpeed;
        soma = AnimaTchau + AnimaVira;

        CalcularDist();
        EstaLonge = (Distancia > Longe);
        EstaPerto = (Distancia < Perto);

        if (EstaPerto)
            Andar = true;
        if (EstaLonge)
            Andar = false;

        //   Andar = LookLeft;
        LookLeft = Andar;
        animPai.SetBool("LookLeft", LookLeft);
        animFilho.SetBool("LookLeft", LookLeft);

        if (Andar != Virando)
        {
            TVirando += Time.deltaTime;
            if (TVirando < AnimaTchau + (AnimaVira / 2))
                IdleV += Time.deltaTime * 2;
            if (TVirando > AnimaTchau + AnimaVira )
            {
                if (Tchau)
                    TVirando = AnimaTchau;
                else
                    TVirando = 0;
                Virando = Andar;
            }
        }
        else
        {
            Look = transform.position;
            Look.x += LookLeft ? 10 : -10;
            Look.z -= 1;
            transform.LookAt(Look);
            IdleV -= Time.deltaTime * 2;
        }
        IdleV = Mathf.Clamp(IdleV, 0, 1);
        animFilho.SetFloat("IdleTurn", IdleV);

        if (Andar)
        {
            temp += Time.deltaTime;
            if (temp > AnimaTchau + AnimaVira)
            {
                animFilho.speed = 1;
                animFilho.SetBool("First", false);
                Tchau = true;
                transform.position += Vector3.right * Speed * Time.deltaTime;
                DistLife -= Time.deltaTime;
            }
        }
        else if (Tchau)
            temp = AnimaTchau;

        if (DistLife < 0)
        {
            animPai.SetTrigger("Sumir");
            Destroy(gameObject, 1.5f);
        }
    }

    void CalcularDist()
    {
        float CatetoX = transform.position.x - Player.position.x;
        float CatetoY = transform.position.y - Player.position.y;

        Distancia = Mathf.Sqrt(CatetoX * CatetoX + CatetoY * CatetoY);
    }
}
