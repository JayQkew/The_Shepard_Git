using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    public AudioSource musicSource;

    [Tooltip("volume = alpha")] public Gradient musicFadeIn;
    [Tooltip("volume = alpha")] public Gradient musicFadeOut;

    public float fadeInTime;
    public float fadeOutTime;

    private void Awake()
    {
        Instance = this;
    }

    public void FadeMusicIn() => StartCoroutine(FadeMusic(musicFadeIn, fadeInTime));

    public void FadeMusicOut() => StartCoroutine(FadeMusic(musicFadeOut, fadeOutTime));

    public IEnumerator FadeMusic(Gradient fadeDir, float fadeTime)
    {
        float timeElapsed = 0;
        if (fadeDir == musicFadeIn)
        {
            musicSource.time = 0;
            musicSource.Play();
        }

        while (timeElapsed < fadeTime)
        {
            musicSource.volume = fadeDir.Evaluate(timeElapsed/fadeTime).a;
            Debug.Log(fadeDir.Evaluate(timeElapsed / fadeTime).a);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        musicSource.volume = fadeDir.Evaluate(1).a;

        if (fadeDir == musicFadeOut) musicSource.Stop();
    }
}
