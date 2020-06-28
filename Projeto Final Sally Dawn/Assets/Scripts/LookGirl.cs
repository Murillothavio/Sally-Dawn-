using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookGirl : MonoBehaviour
{
    private Transform Girl;
    public Vector3 Posi;
    public float h = 0.5f, max = 2, min = -2;


    // Start is called before the first frame update
    void Start()
    {
        Girl = GameMaster.gm.Player.transform;

        max += transform.position.y;
        min += transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Posi = Girl.position;
        Posi.y += h;
        Posi.y = Mathf.Clamp(Posi.y, min, max);
        transform.LookAt(Posi);
    }
}
