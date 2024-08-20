using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice : MonoBehaviour
{
    public AudioClip Audio;
    AudioSource AS;
    void Start()
    {
        AS = GetComponent<AudioSource>();
    }
    void Update()
    {
        
    }
    public void AudioPlay()
    {
        AS.Play();
    }
    public void AudioPause()
    {
        AS.Pause();
    }
    public void AudioStop()
    {
        AS.Stop();
    }

}
