using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] AudioClip musicClip;
    [SerializeField] float musicVolume;

    void Start()
    {
        var audioPlayer = FindObjectOfType<AudioPlayer>();
        if (audioPlayer != null)
        {
            audioPlayer.PlayAudioLoop(musicClip, musicVolume, StaticReferences.MusicChannel);
        }
    }
}
