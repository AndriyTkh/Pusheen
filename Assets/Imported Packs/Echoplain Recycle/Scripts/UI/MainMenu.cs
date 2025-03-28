using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string menuMusic;
    [Space (20f)]

    public Slider musicSlider;
    public Slider sfxSlider;
    [Space(20f)]

    public int _level;
    public string _transitionName;

    private void Start()
    {
        LoadVolume();
        LoadLevel();
        MusicManager.Instance.PlayMusic(menuMusic);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P) && Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.Instance.LoadSceneName("Instant", "SampleScene");
        }
    }


    private void LoadLevel()
    {
        _level = PlayerPrefs.GetInt("Level");
        if (_level == 0)
        {
            _level = 2;
            PlayerPrefs.SetInt("Level", 2);
        }
    }

    public void Quit()
    {
        SaveVolume();
        Application.Quit();
    }

    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SaveVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    public void Transition()
    {
        SaveVolume();
        MusicManager.Instance.musicSource.Stop();
        LevelManager.Instance.LoadSceneLevel(_transitionName, _level);
    }
}