using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBomb : MonoBehaviour
{
    public GameObject Bomba;
    public bool Drop;
    [Range(0, 10)] public float CoolDown = 10, currTime = -10;
    public float jhh;

    private void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Drop = GetComponentInParent<AndarPlayer>().Kbomba;

        jhh = GetComponentInParent<AndarPlayer>().horizontal;

        if (Drop&&currTime<0)
        {
            GameObject b = Instantiate(Bomba, transform.position, Quaternion.identity) as GameObject;
            if (b.GetComponent<Bomb>() == null)
                Debug.LogError("Not get bomb");
            else
            {
                b.GetComponent<Bomb>().h = GetComponentInParent<AndarPlayer>().horizontal;
                b.GetComponent<Bomb>().speed += GetComponentInParent<AndarPlayer>().moveSpeed;
            }
            currTime = CoolDown;
        }
        if (currTime > 0)
            currTime -= Time.deltaTime;
    }
}
