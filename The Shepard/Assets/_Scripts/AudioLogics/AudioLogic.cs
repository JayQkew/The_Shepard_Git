using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioLogic : MonoBehaviour
{
    public AudioSource source;
    public UnityEvent sound;

    private void Start()
    {
        sound.AddListener(Play);
    }

    public void Play() => source.Play();
}
