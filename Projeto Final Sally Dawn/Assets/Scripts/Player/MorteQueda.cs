using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteQueda : MonoBehaviour
{
    public Transform Cam;
    public float Distancia;
    [Range(1,100)]
    public float Max;
    [SerializeField]
    private float tdelay = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Cam == null)
            Debug.LogError("No cam in Morte queda");
    }

    // Update is called once per frame
    void Update()
    {
        if (tdelay < 2)
            tdelay += Time.deltaTime;
        else if ((Cam != null) && (GetComponent<AndarPlayer>().CanWalk))
            Distancia = GameMaster.CalcularDist(transform, Cam);

        if (GetComponent<AndarPlayer>().CanWalk)
            if (Distancia > Max)
            {
                GetComponent<AndarPlayer>().Morrer();
                Debug.Log("morrer");
            }
    }
}
