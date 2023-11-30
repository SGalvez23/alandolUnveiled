using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraAudioClips : MonoBehaviour
{
    AudioSource annoraAudio;
    public AudioClip hookShot;
    public AudioClip basicAtk;
    public AudioClip camo;
    public AudioClip frenesi;
    public AudioClip apreton;
    public AudioClip muerteCerte;
    //public AudioClip steps;

    private void Start()
    {
        annoraAudio = GetComponent<AudioSource>();
        annoraAudio.enabled = false;
    }

    /*public void PlayAudio(AudioClip clipToPlay)
    {
        annoraAudio.clip = clipToPlay;
        annoraAudio.enabled = true;
        annoraAudio.Play();
    }*/

    public void PlayHookSound()
    {
        annoraAudio.clip = hookShot;
        annoraAudio.enabled = true;
        annoraAudio.Play();
    }

    public void PlayAtkSound()
    {
        annoraAudio.clip = basicAtk;
        annoraAudio.volume = 0.8f;
        annoraAudio.pitch = 1.4f;
        annoraAudio.enabled = true;
        annoraAudio.Play();
    }

    public void PlayCamoSound()
    {
        annoraAudio.clip = camo;
        annoraAudio.enabled = true;
        annoraAudio.Play();
    }

    public void PlayFrenesiSound()
    {
        annoraAudio.clip = frenesi;
        annoraAudio.enabled = true;
        annoraAudio.Play();
    }

    public void PlayApretonSound()
    {
        annoraAudio.clip = apreton;
        annoraAudio.enabled = true;
        annoraAudio.Play();
    }

    public void PlayMuerteCerteSound()
    {
        annoraAudio.clip = muerteCerte;
        annoraAudio.enabled = true;
        annoraAudio.Play();
    }
}
