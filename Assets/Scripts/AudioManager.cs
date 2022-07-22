using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;

    [Range(.1f,3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    void Awake()
    {
        foreach (Sound item in sounds)
        {
            item.source = gameObject.AddComponent<AudioSource>();
            item.source.clip = item.clip;
            item.source.volume = item.volume;
            item.source.pitch = item.pitch;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
