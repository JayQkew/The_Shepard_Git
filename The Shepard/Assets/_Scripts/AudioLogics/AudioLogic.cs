using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioLogic : MonoBehaviour
{
    public AudioSource source;

    public void Play() => source.Play();
}
