using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    //Data Repository Reference
    private DataRepository repository;

    //Music Audio Source Reference
    private AudioSource musicAudioSource;

    //SFX Audio Source Reference
    private AudioSource sfxAudioSource;

    //Game Song Reference
    private AudioClip gameSong;

    //Boss Song Reference
    private AudioClip bossSong;

    //Endgame Song Reference
    private AudioClip endgameSong;

    //Shooting SFXClip Reference
    private AudioClip fireSFX;

    //Warning SFXClip Reference
    private AudioClip warningSFX;

    //Clear SFXClip Reference
    private AudioClip clearSFX;

    // Start is called before the first frame update
    void Awake()
    {
        //Get References
        repository = GameObject.Find("DataRepository").GetComponent<DataRepository>();

        musicAudioSource = GetComponents<AudioSource>()[0];
        sfxAudioSource = GetComponents<AudioSource>()[1];

        gameSong = Resources.Load<AudioClip>("Music/GameSong");
        bossSong = Resources.Load<AudioClip>("Music/BossSong");
        endgameSong = Resources.Load<AudioClip>("Music/EndGameSong");

        fireSFX = Resources.Load<AudioClip>("Music/FireShot");
        warningSFX = Resources.Load<AudioClip>("Music/WarningSFX");
        clearSFX = Resources.Load<AudioClip>("Music/Clear");

        //Set Volumes
        musicAudioSource.volume = repository.GetMusicVolume();
        sfxAudioSource.volume = repository.GetSFXVolume();
    }

    public void PlayGameSong()
    {
        musicAudioSource.clip = gameSong;
        musicAudioSource.Play();
    }

    public void PlayBossSong()
    {
        musicAudioSource.clip = bossSong;
        musicAudioSource.Play();
    }

    public void PlayEndGameSong()
    {
        musicAudioSource.clip = endgameSong;
        musicAudioSource.Play();
    }

    public void PlayFireSFX()
    {
        sfxAudioSource.pitch = Random.Range(1.6f, 2.4f);

        sfxAudioSource.PlayOneShot(fireSFX);
    }

    public void PlayWarningSFX()
    {
        sfxAudioSource.pitch = 1f;

        sfxAudioSource.PlayOneShot(warningSFX);
    }

    public void PlayClearSFX()
    {
        sfxAudioSource.pitch = 1f;

        sfxAudioSource.PlayOneShot(clearSFX);
    }

    public void MusicPause()
    {
        musicAudioSource.Pause();
    }

    #region Set Volumes
    public void SetMusicVolume(float musicVolume)
    {
        musicAudioSource.volume = musicVolume;

        repository.SetMusicVolume(musicVolume);
    }

    public void SetSFXVolume(float sfxVolume) 
    {
        sfxAudioSource.volume = sfxVolume;

        repository.SetSFXVolume(sfxVolume);
    }
    #endregion
}
