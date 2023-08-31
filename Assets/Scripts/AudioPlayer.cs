using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource[] audioSources;
    AudioSource currentSource;

    void Awake()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        currentSource = audioSources[0];
    }

    public bool IsCurrentlyPlaying(string channel)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.name == channel)
            {
                currentSource = source;
            }
        }

        return currentSource.isPlaying;
    }

    public void PlayClipOnce(AudioClip clip, float volume, string channel)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.name == channel)
            {
                currentSource = source;
            }
        }
        currentSource.loop = false;
        currentSource.volume = volume;
        currentSource.PlayOneShot(clip);
    }

    public void PlayAudioLoop(AudioClip clip, float volume, string channel)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.name == channel)
            {
                currentSource = source;
            }
        }
        currentSource.volume = volume;
        currentSource.clip = clip;
        currentSource.loop = true;
        currentSource.PlayDelayed(1f);
    }

    public void StopAudio(string channel)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.name == channel)
            {
                currentSource = source;
            }
        }
        currentSource.loop = false;
        currentSource.Stop();
    }
}
