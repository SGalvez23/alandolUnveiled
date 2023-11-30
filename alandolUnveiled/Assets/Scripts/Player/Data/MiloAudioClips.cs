using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiloAudioClips : MonoBehaviour
{
    AudioSource miloAudio;
    public AudioClip basicAtk;
    public AudioClip viejon;
    public AudioClip rojoVivo;
    public AudioClip cheve;
    public AudioClip carnita;

    private void Start()
    {
        miloAudio = GetComponent<AudioSource>();
        miloAudio.enabled = false;
    }

    public void PlayBasicAtkSound()
    {
        miloAudio.clip = basicAtk;
        miloAudio.enabled = true;
        miloAudio.Play();
    }

    public void PlayViejonSound()
    {
        miloAudio.clip = viejon;
        miloAudio.enabled = true;
        miloAudio.Play();
    }

    public void PlayrRojoVivoSound()
    {
        miloAudio.clip = rojoVivo;
        miloAudio.enabled = true;
        miloAudio.Play();
    }

    public void PlayCheveSound()
    {
        miloAudio.clip = cheve;
        miloAudio.enabled = true;
        miloAudio.Play();
    }

    public void PlayCarnitaSound()
    {
        miloAudio.clip = carnita;
        miloAudio.enabled = true;
        miloAudio.Play();
    }
}
