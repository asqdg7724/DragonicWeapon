using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_List : MonoBehaviour
{
    public static BGM_List BGM;

    AudioSource myAudio;

    public AudioClip[] Stagesounds;
    public AudioClip[] UIsounds;


    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    public void PartSoundPlay(int SoundNumber)
    {
        myAudio.clip = Stagesounds[SoundNumber];

        myAudio.Play();
    }

    public void UISoundPlay(int SoundNumber)
    {
        myAudio.loop = false;

        myAudio.clip = UIsounds[SoundNumber];

        myAudio.Play();

    }
}
