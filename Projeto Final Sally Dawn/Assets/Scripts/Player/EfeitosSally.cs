using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosSally : MonoBehaviour
{
    [System.Serializable]
    public class ParticleEffect
    {
        public GameObject Visual;
        public AudioClip Audio;
        public float TempoVida = 5;
    }
    public ParticleEffect Coletar;

    public void Coletou()
    {
        if (Coletar.Visual != null)
        {
            GameObject ef = Instantiate(Coletar.Visual,transform.position,Quaternion.identity) as GameObject;
            ef.transform.parent = transform;
            Debug.Log("efeitou");
             Destroy(ef, Coletar.TempoVida);
        }
    }
}
