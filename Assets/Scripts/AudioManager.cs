using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;


}


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        Load();
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveObjects loadedSaveObjects = JsonUtility.FromJson<SaveObjects>(saveString);

            foreach (Sound item in sounds)
            {
                item.source = gameObject.AddComponent<AudioSource>();
                item.source.clip = item.clip;
                item.source.pitch = item.pitch;
                item.source.loop = item.loop;

                if (item.name == "Theme")
                    item.source.volume = loadedSaveObjects.musicVolume;
                else
                    item.source.volume = loadedSaveObjects.sfxVolume;
            }
            //GameObject.FindGameObjectWithTag("SliderMusic").GetComponent<Slider>().value = loadedSaveObjects.musicVolume;
            // GameObject.FindGameObjectWithTag("SliderSFX").GetComponent<Slider>().value = loadedSaveObjects.sfxVolume;
        }
        else            //save dosyasi bulunmadiginda butun degerlerin 1 oldugu bir dosya olusturulur
        {
            foreach (Sound item in sounds)
            {
                item.source = gameObject.AddComponent<AudioSource>();
                item.source.clip = item.clip;
                item.source.volume = item.volume;
                item.source.pitch = item.pitch;
            }
        }
    }

    private AudioSource[] audios;

    public void ValueUpdate()
    {
        audios = gameObject.GetComponents<AudioSource>();

        for (int i = 0; i < audios.Length; i++)
        {
            if (audios[i].clip.name == "MainTheme")
                audios[i].volume = GameObject.FindGameObjectWithTag("SliderMusic").GetComponent<Slider>().value;


            else
                audios[i].volume = GameObject.FindGameObjectWithTag("SliderSFX").GetComponent<Slider>().value;




        }



    }

    private class SaveObjects
    {
        public float musicVolume;
        public float sfxVolume;
        public int currentLevel;
    }
}
