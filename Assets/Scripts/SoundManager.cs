﻿using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;



    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        if (!s.source.isPlaying)
            s.source.Play();
    }

    public void Pause (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Pause();
    }
}