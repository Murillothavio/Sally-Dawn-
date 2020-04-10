using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_Attach : MonoBehaviour
{
    public GameObject Player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player = collision.gameObject;
            if (collision.gameObject == Player)
            {
                Player.transform.parent = transform;
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == Player)
        {
            Player.transform.parent = null;
            Player = null;
        }

    }
}