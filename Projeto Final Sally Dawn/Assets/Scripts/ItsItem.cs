using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItsItem : MonoBehaviour
{
    public enum TypeItem { PowerUp, Emotion}
    public TypeItem typeItem;
    public float LifeTime;
    public GameObject TxtTuto;

    private void Start()
    {
        if (TxtTuto != null)
            TxtTuto.GetComponent<Collider>().enabled = false;

    }

    private void OnDestroy()
    {
        if (TxtTuto != null)
            TxtTuto.GetComponent<Collider>().enabled = true;

    }
}
