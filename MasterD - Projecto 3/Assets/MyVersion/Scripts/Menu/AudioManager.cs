using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Data Repository Reference
    private DataRepository repository;

    //Music Audio Source Reference
    private AudioSource musicAudioSource;

    // Start is called before the first frame update
    void Awake()
    {
        //Get References
        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();

        musicAudioSource = GetComponent<AudioSource>();

        //Set Volume
        musicAudioSource.volume = repository.GetMusicVolume();
    }

    public void SetMusicVolume(float musicVolume)
    {
        musicAudioSource.volume = musicVolume;

        repository.SetMusicVolume(musicVolume);
    }

    public void SetSFXVolume(float sfxVolume)
    {
        repository.SetSFXVolume(sfxVolume);
    }
}
