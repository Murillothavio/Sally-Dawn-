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
    [System.Serializable]
    public class ObjetoEfeito
    {
        public GameObject Visual;
        public bool Ativo;
    }
    public ParticleEffect Coletar;

    public ObjetoEfeito Andar, Pular, Jato;

    private void Start()
    {
    }

    private void Update()
    {
        Andar.Ativo = GetComponent<AndarPlayer>().isGrounded;
        if (Andar.Visual != null)
            Andar.Visual.SetActive(Andar.Ativo);
    }

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
