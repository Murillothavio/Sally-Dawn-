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
        public bool Ativo;
    }

    public ParticleEffect Coletar, Pular, Jato, Andar;

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
            GameObject ef = Instantiate(Coletar.Visual, transform.position, Quaternion.identity) as GameObject;
            ef.transform.parent = transform;
            Destroy(ef, Coletar.TempoVida);
        }
        else
            Debug.LogError("Sem Efeito Coletar");
    }
    public void EfeitoPular() {
        if (Pular.Visual != null)
        {
            GameObject ef = Instantiate(Pular.Visual, transform.position, Quaternion.identity) as GameObject;
            Destroy(ef, Coletar.TempoVida);

        }
        else Debug.LogError("Sem Efeito Pular");
    }
    public void EfeitoJato()
    {
        Jato.Ativo = true;
        if (Jato.Visual != null)
            Jato.Visual.SetActive(Jato.Ativo);
        Invoke("DesligaJato", Jato.TempoVida);
    }
    void DesligaJato()
    {
        Jato.Ativo = false;
        if (Jato.Visual != null)
            Jato.Visual.SetActive(Jato.Ativo);
    }
}
