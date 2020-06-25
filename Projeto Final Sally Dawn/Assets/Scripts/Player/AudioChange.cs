using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioChange : MonoBehaviour
{
    public AudioClip acFase, acEvento, acAtual;
    public bool Trocando;
    private bool bplay;
    [Range(0, 1)] public float AudioVolume = 1;
    [Range(0, 1)] public float currVolume = 1;

    public AudioMixer mixer;
    private AudioSource AudSrc;

    public float Tmaximo, tcorrido;

    // Start is called before the first frame update
    private void Start()
    {
        AudSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (acEvento == null)
            acAtual = acFase;
        else acAtual = acEvento;

        if (Trocando)
        {
            tcorrido += Time.deltaTime;
            float sng = (tcorrido > Tmaximo / 2) ? 1 : -1;
            if ((tcorrido > Tmaximo / 2) && !bplay)
            {
                bplay = true;
                AudSrc.clip = acAtual;
                AudSrc.Play();
            }
            currVolume += Time.deltaTime * (AudioVolume / (Tmaximo / 2)) * sng;

            if (tcorrido > Tmaximo)
                Trocando = false;
        }
        else
        {
            tcorrido = 0;
            currVolume = AudioVolume;
            bplay = false;
        }
        currVolume = Mathf.Clamp(currVolume, 0.001f, AudioVolume);
        mixer.SetFloat("AudioVolume", Mathf.Log10(currVolume) * 20);
    }
}
