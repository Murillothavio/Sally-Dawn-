using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaBoom : MonoBehaviour
{
    public GameObject Boom;

    private void OnDestroy()
    {
        GameObject b = Instantiate(Boom, transform.position, Quaternion.identity)as GameObject;
        Destroy(b, 2);
    }

}
