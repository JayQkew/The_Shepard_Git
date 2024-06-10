using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioLogic : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;

    public void Play() => source.Play();

    public void RandomPlay()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Play();
    }
}
