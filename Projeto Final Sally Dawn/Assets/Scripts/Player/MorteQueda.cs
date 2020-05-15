using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteQueda : MonoBehaviour
{
    public Transform Cam;
    public float Distancia;
    [Range(1,100)]
    public float Max;
    // Start is called before the first frame update
    void Start()
    {
        if (Cam == null)
            Debug.LogError("No cam in Morte queda");
    }

    // Update is called once per frame
    void Update()
    {
        if (Cam != null)
            Distancia = GameMaster.CalcularDist(transform, Cam);

        if (Distancia > Max)
        {
            GetComponent<AndarPlayer>().Morrer();
            Debug.Log("morrer");
        }
    }
}
