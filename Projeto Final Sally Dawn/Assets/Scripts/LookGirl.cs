using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookGirl : MonoBehaviour
{
    private Transform Girl;
    private Vector3 Posi;
    public float h = 3.5f;


    // Start is called before the first frame update
    void Start()
    {
        Girl = GameMaster.gm.Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Posi = Girl.position;
        Posi.y += h;
        transform.LookAt(Posi);
    }
}
