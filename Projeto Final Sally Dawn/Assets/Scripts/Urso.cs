using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urso : MonoBehaviour
{
    private Transform Player;
    [Range(0,100)]
    public float Distancia, Longe, Perto;
    public bool EstaLonge, EstaPerto, Andar;

    private Rigidbody rb;
    private Animator anim;
    [Range(10, 50)]
    public float Speed;
    public float DistLife;
    public float temp;
    public bool Tchau;
    public float AnimaTchau, AnimaVira;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        CalcularDist();
        EstaLonge = (Distancia > Longe);
        EstaPerto = (Distancia < Perto);

        if (EstaPerto)
            Andar = true;
        if (EstaLonge)
            Andar = false;

        if (Andar)
        {
            temp += Time.deltaTime;
            if (temp > AnimaTchau + AnimaVira)
            {
                Tchau = true;
                transform.position += Vector3.right * Speed * Time.deltaTime;
                DistLife -= Time.deltaTime;
            }
        }
        else if (Tchau)
            temp = AnimaTchau;

        if (DistLife < 0)
        {
            anim.SetTrigger("Sumir");
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
