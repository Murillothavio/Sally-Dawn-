using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevador : MonoBehaviour
{
    public bool Ativo;
    public float Tempo = 10;
    [SerializeField]
    private float DpT, ttt, Speed, Distancia, Ini;
    [SerializeField]
    private Vector3 Posi;
    [SerializeField]
    private GameObject Fim, Cant, Pp;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            if (go.name == "Fim")
                Fim = go;
            else
                Cant = go;
        }

        Posi = transform.position;
        Ini = Posi.y;

        Distancia = Fim.transform.position.y;

        DpT = (Distancia - Ini) / Tempo;
        Speed = (DpT * Time.deltaTime);
        Speed = Mathf.Abs(Speed);

        ttt = Tempo;
    }

    // Update is called once per frame
    void Update()
    {
        //start
        //

        if (Ativo)
        {
            Posi.y = Mathf.MoveTowards(Posi.y, Distancia, Speed);
            ttt -= Time.deltaTime;
        }

        if (Mathf.Abs(Posi.y) >= Mathf.Abs(Distancia))
            Ativo = false;
        transform.position = Posi;
        Cant.SetActive(Ativo);

        if (Pp != null)
            Pp.GetComponent<AndarPlayer>().CanWalk = !Ativo;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Ativo = true;
            Pp = collision.gameObject;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Pp = null;
        GameMaster.gm.LiberaPlayer();
    }
}
