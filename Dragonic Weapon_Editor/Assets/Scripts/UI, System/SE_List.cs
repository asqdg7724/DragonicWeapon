using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_List : MonoBehaviour
{
    public static SE_List SE;

    AudioSource myAudio;

    public AudioClip[] sounds;


    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void SoundPlay(int SoundNumber)
    {
        myAudio.clip = sounds[SoundNumber];

        myAudio.Play();

    }
}
