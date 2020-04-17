using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Detector : MonoBehaviour
{
    public GameObject[] Targets;
    [Range(51, 200)] public float rDisMaior = 100;
    [Range(1, 50)] public float rDisMenor = 20;
    [Range(0, 200)] public float rDistancia;

    public bool Proximando;
    [Header("por decimo de segundo")]
    [Range(0, 100)] public float FreqnBeep = 10;
    [Range(21, 200)] public float FreqnMax = 100;
    [Range(1, 20)] public float FreqnMin = 10;


    [SerializeField]
    private float currTm = 09999;

    private AudioSource adSrc;

    // Start is called before the first frame update
    void Start()
    {
        adSrc = GetComponent<AudioSource>();
        Varedura();
    }

    // Update is called once per frame
    void Update()
    {
        Medir();


        FreqnBeep = FreqnMin + ((FreqnMax - FreqnMin) / (rDisMaior - rDisMenor) * (rDistancia-rDisMenor));

        if (rDistancia < rDisMenor)
            FreqnBeep = FreqnMin;

        Proximando = (rDistancia < rDisMaior);
        if (Proximando)
        {
            currTm += Time.deltaTime;
            if (currTm > (FreqnBeep / 10))
            {
                currTm = 0;
                adSrc.Play();
            }
        }
        else currTm = 99999;
    }

    void Varedura()
    {
        Targets = GameObject.FindGameObjectsWithTag("Emocao");
    }
    void Medir()
    {
        float Menor=999;
        float CatetoX, CatetoY, Hipot;

        bool notfound = false;

        for (int i = 0; i < Targets.Length; i++)
        {
            notfound = (Targets[i] == null);
            if (!notfound)
            {
                CatetoX = Targets[i].transform.position.x - transform.position.x;
                CatetoY = Targets[i].transform.position.y - transform.position.y;

                Hipot = Mathf.Sqrt(CatetoX * CatetoX + CatetoY * CatetoY);

                if (Hipot < Menor)
                    Menor = Hipot;
            }
        }
        rDistancia = Menor;

        if (notfound)
            Varedura();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .5f);
        Gizmos.DrawSphere(transform.position, rDisMaior);

        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawSphere(transform.position, rDisMenor);


    }
}
