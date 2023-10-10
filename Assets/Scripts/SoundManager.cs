using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource sceneAudio;
    public float volume = 1f;
    void Awake()
    {
        sceneAudio = GetComponent<AudioSource>();
        sceneAudio.volume = volume;
    }




}
