using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource hit;


    public AudioClip hitClip;
    void Start()
    {
        hit = gameObject.AddComponent<AudioSource>();
        hit.clip = hitClip;
    }


    void Update()
    {
        
    }

    public void PlayHitAudio()
    {
        hit.Play();
    }
}
