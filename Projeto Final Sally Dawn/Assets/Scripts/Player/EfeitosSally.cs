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

    public ParticleEffect ColetarMemoria, ColetarPowerUp, Pular, Jato, Andar, Escalar, Morte, Renascer;
    
    private void Update()
    {
        Andar.Ativo = (GetComponent<AndarPlayer>().isGrounded && !GetComponent<AndarPlayer>().isClimb);
        if (Andar.Visual != null)
            Andar.Visual.SetActive(Andar.Ativo);

        Escalar.Ativo = (GetComponent<AndarPlayer>().isGrounded && GetComponent<AndarPlayer>().isClimb);
        if (Escalar.Visual != null)
            Escalar.Visual.SetActive(Escalar.Ativo);
    }

    public void Coletou(bool memo)
    {
        if (memo)
            TempEffect(ColetarMemoria);
        else
            TempEffect(ColetarPowerUp);
    }

    private void TempEffect(ParticleEffect pef)
    {
        if (pef.Visual != null)
        {
            GameObject ef = Instantiate(pef.Visual, transform.position, Quaternion.identity) as GameObject;
            ef.transform.parent = transform;
            Destroy(ef, pef.TempoVida);
        }
        else
            Debug.LogError("Sem Efeito");
    }
    public void EfeitoPular() {
        if (Pular.Visual != null)
        {
            GameObject ef = Instantiate(Pular.Visual, transform.position, Quaternion.identity) as GameObject;
            Destroy(ef, Pular.TempoVida);

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
    public void EfeitoMorrer()
    {
        if (Morte.Visual != null)
        {
            GameObject ef = Instantiate(Morte.Visual, transform.position, Quaternion.identity) as GameObject;
            Destroy(ef, Morte.TempoVida);
        }
        Debug.Log("To Do Morrer");
    }
    public void EfeitoRenascer()
    {
        if (Renascer.Visual != null)
        {
            GameObject ef = Instantiate(Renascer.Visual, transform.position, Quaternion.identity) as GameObject;
            ef.transform.parent = transform;
            Destroy(ef, Renascer.TempoVida);
        }
        Debug.Log("To Do Renascer");
    }
}
