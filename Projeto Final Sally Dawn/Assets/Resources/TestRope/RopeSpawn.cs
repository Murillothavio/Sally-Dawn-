using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject ParenteObject, PartPrefab;

    [SerializeField]
    [Range(1, 100)]
    int Length = 1;

    [SerializeField]
    float partDistance = 0.21f;

    [SerializeField]
    bool reset, spawn, snapFirst, snapLast;

    [SerializeField]
    Transform TheFirst, TheLast;

    // Update is called once per frame
    void Update()
    {
        if (!reset || spawn)
        {
            for (int i = 0; i < ParenteObject.transform.childCount; i++)
            {
                Destroy(ParenteObject.transform.GetChild(i).gameObject);
            }
            reset = true;
        }
        if (spawn)
        {
            Spawnar();
            spawn = false;
        }
    }

    public void Spawnar()
    {
        int count = (int)(Length / partDistance);

        for (int i = 0; i < count; i++)
        {
            GameObject tmp;
            Vector3 posi = transform.position;
            posi.y -= partDistance * (i + 1);

            tmp = Instantiate(PartPrefab, posi, Quaternion.identity, ParenteObject.transform);
            tmp.name = ParenteObject.transform.childCount.ToString();

            if (i == 0)
            {
                Destroy(tmp.GetComponent<CharacterJoint>());
                TheFirst = tmp.transform;
                posi.y += partDistance * 0.07f;
                TheFirst.position = posi;
                if (snapFirst)
                    tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                tmp.GetComponent<CharacterJoint>().connectedBody = ParenteObject.transform.Find((ParenteObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
        TheLast = ParenteObject.transform.Find((ParenteObject.transform.childCount).ToString());
        if (snapLast)
            TheLast.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
