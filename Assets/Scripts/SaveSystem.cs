using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    private int currentLevelData;
    private string json;

    private class SaveObjects
    {
        public float musicVolume;
        public float sfxVolume;
        public int currentLevel;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    //musicVolume = GameObject.FindGameObjectWithTag("SliderMusic").GetComponent<Slider>().value,
    //sfxVolume = GameObject.FindGameObjectWithTag("SliderMusic").GetComponent<Slider>().value,
    private void Awake()
    {

    }

    public void SaveMusicAndSFX()
    {
        SaveObjects saveObjects = new SaveObjects
        {
            musicVolume = GameObject.FindGameObjectWithTag("SliderMusic").GetComponent<Slider>().value,
            sfxVolume = GameObject.FindGameObjectWithTag("SliderSFX").GetComponent<Slider>().value,
            currentLevel = currentLevelData
        };
        json = JsonUtility.ToJson(saveObjects);

        File.WriteAllText(Application.dataPath + "/save.txt", json);


        SaveObjects loadedSaveObjects = JsonUtility.FromJson<SaveObjects>(json);

    }

    
    public void Load()
    {
        if(File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveObjects loadedSaveObjects = JsonUtility.FromJson<SaveObjects>(saveString);

            GameObject.FindGameObjectWithTag("SliderMusic").GetComponent<Slider>().value = loadedSaveObjects.musicVolume;
            GameObject.FindGameObjectWithTag("SliderSFX").GetComponent<Slider>().value= loadedSaveObjects.sfxVolume;
            currentLevelData = loadedSaveObjects.currentLevel;
        }
        else            //save dosyasi bulunmadiginda butun degerlerin 1 oldugu bir dosya olusturulur
        {
            SaveObjects saveObjects = new SaveObjects
            {
                musicVolume = 1,
                sfxVolume = 1,
                currentLevel = 1
            };
            json = JsonUtility.ToJson(saveObjects);

            File.WriteAllText(Application.dataPath + "/save.txt", json);
            currentLevelData = 1;
        }
    }

    public void CheckCurrentLevel(int level)
    {
        string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
        SaveObjects loadedSaveObjects = JsonUtility.FromJson<SaveObjects>(saveString);

        currentLevelData = loadedSaveObjects.currentLevel;

        if(level > currentLevelData)
        {
            SaveObjects saveObjects = new SaveObjects
            {
                musicVolume = loadedSaveObjects.musicVolume,
                sfxVolume = loadedSaveObjects.sfxVolume,
                currentLevel = level
            };
            json = JsonUtility.ToJson(saveObjects);

            File.WriteAllText(Application.dataPath + "/save.txt", json);
        }


    }

    public void CreateNewGame()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
        SaveObjects loadedSaveObjects = JsonUtility.FromJson<SaveObjects>(saveString);

        SaveObjects saveObjects = new SaveObjects
        {
            musicVolume = loadedSaveObjects.musicVolume,
            sfxVolume = loadedSaveObjects.sfxVolume,
            currentLevel = 1
        };
        json = JsonUtility.ToJson(saveObjects);

        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public int FindCurrentLevel()
    {
        string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
        SaveObjects loadedSaveObjects = JsonUtility.FromJson<SaveObjects>(saveString);

        return loadedSaveObjects.currentLevel;
    }


}
