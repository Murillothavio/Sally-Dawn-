using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_escada : MonoBehaviour
{
    public Vector3 PontoAtual, PontoRed, PontoBlue;
    // Start is called before the first frame update
    void Start()
    {
        PontoRed.y += transform.position.y;
        PontoBlue.y += transform.position.y;
        transform.position = PontoRed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
