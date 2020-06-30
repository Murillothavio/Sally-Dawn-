using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutomov : MonoBehaviour
{
    public Collider c;
    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        c.enabled = !GameMaster.gm.PAUSADO;
    }
}
