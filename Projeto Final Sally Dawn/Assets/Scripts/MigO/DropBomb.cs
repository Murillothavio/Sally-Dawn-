using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public GameObject Bomba;
    public bool Drop;
    [Range(0, 10)] public float CoolDown = 10, currTime = -10;

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Drop = GetComponentInParent<AndarPlayer>().Kbomba;

        if (Drop&&currTime<0)
        {
            Instantiate(Bomba, transform.position, Quaternion.identity);
            currTime = CoolDown;
        }
        if (currTime > 0)
            currTime -= Time.deltaTime;
    }
}
