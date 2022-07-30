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
        Load();
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
                item.source.volume = loadedSaveObjects.sfxVolume;
                item.source.pitch = item.pitch;
            }
            //GameObject.FindGameObjectWithTag("SliderMusic").GetComponent<Slider>().value = loadedSaveObjects.musicVolume;
            // GameObject.FindGameObjectWithTag("SliderSFX").GetComponent<Slider>().value = loadedSaveObjects.sfxVolume;
        }
        else            //save dosyasi bulunmadiginda butun degerlerin 1 oldugu bir dosya olusturulur
        {
            SaveObjects saveObjects = new SaveObjects
            {
                musicVolume = 1,
                sfxVolume = 1,
                currentLevel = 1
            };
            string json = JsonUtility.ToJson(saveObjects);

            File.WriteAllText(Application.dataPath + "/save.txt", json);

            foreach (Sound item in sounds)
            {
                item.source = gameObject.AddComponent<AudioSource>();
                item.source.clip = item.clip;
                item.source.volume = item.volume;
                item.source.pitch = item.pitch;
            }
        }
    }

    private class SaveObjects
    {
        public float musicVolume;
        public float sfxVolume;
        public int currentLevel;
    }
}
