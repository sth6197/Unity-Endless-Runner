using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource effectAudioSource;
    [SerializeField] AudioSource sceneryAudioSource;

    [SerializeField] AudioClip titleClip;
    [SerializeField] AudioClip maingameClip;

    [SerializeField] private Toggle bgmToggle;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private float bgmVolume = 1.0f;
    private float sfxVolume = 1.0f;

    private void Start()
    {
        bgmToggle.onValueChanged.AddListener(OnBGMToggleChanged);
        sfxToggle.onValueChanged.AddListener(OnSFXToggleChanged);

        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title")
        {
            PlayBackgroundMusic(titleClip);
        }
        else if(scene.name == "Game")
        {
            PlayBackgroundMusic(maingameClip);
        }
    }

    public void PlayBackgroundMusic(AudioClip audioclip)
    {
        if(sceneryAudioSource.clip == audioclip && sceneryAudioSource.isPlaying)
        {
            return;
        }

        sceneryAudioSource.clip = audioclip;
        sceneryAudioSource.loop = true;
        sceneryAudioSource.volume = bgmVolume;
        sceneryAudioSource.Play();
    }

    public void Listen(AudioClip audioClip)
    {
        effectAudioSource.PlayOneShot(audioClip, sfxVolume);
    }

    private void OnBGMToggleChanged(bool isOn)
    {
        bgmVolume = isOn ? bgmSlider.value : 0f;
        sceneryAudioSource.mute = !isOn;
        sceneryAudioSource.volume = bgmVolume;
    }

    private void OnSFXToggleChanged(bool isOn)
    {
        sfxVolume = isOn ? sfxSlider.value : 0f;
        effectAudioSource.mute = !isOn;
        effectAudioSource.volume = sfxVolume;
    }

    private void OnBGMVolumeChanged(float value)
    {
        bgmVolume = value;

        if(bgmToggle.isOn)
        {
            sceneryAudioSource.volume = bgmVolume;
        }
    }

    private void OnSFXVolumeChanged(float value)
    {
        sfxVolume = value;

        if(sfxToggle.isOn)
        {
            effectAudioSource.volume = sfxVolume;
        }
    }
}