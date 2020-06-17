using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private GameObject cam;
    [Range(20, 50)]
    public float NovaDistancia = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (cam == null)
                cam = GameMaster.gm.Cam;
            if (cam != null)
            {
                cam.GetComponent<CameraFollow>().NovoAfastamento = NovaDistancia;
            }
            else
            {
                Debug.LogError("No cam zoom");
            }

        }
    }
}
