using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_Attach : MonoBehaviour
{
    public GameObject Player, Caixote;

    private void Start()
    {
      //  Debug.Log(gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player = collision.gameObject;
            if (collision.gameObject == Player)
            {
                if (Player.GetComponent<AndarPlayer>().isGrounded)
                    Player.transform.parent = transform;
                else Player.transform.parent = null;
            }
        }
        if (collision.gameObject.tag == "Zona_empurrar")
        {
            Caixote = collision.gameObject;
            if (collision.gameObject == Caixote)
            {
                Caixote.transform.parent = transform;
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
        if (collision.gameObject.tag == "Zona_empurrar")
        {
            Caixote.transform.parent = null;
            Caixote = null;
        }

    }
}