using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private const string MasterKey = "MasterVolume";
    private const string SfxKey = "SfxVolume";
    private const string MusicKey = "MusicVolume";

    void Start()
    {

        float master = PlayerPrefs.GetFloat(MasterKey, 0.75f);
        float sfx = PlayerPrefs.GetFloat(SfxKey, 0.75f);
        float music = PlayerPrefs.GetFloat(MusicKey, 0.75f);

        masterSlider.SetValueWithoutNotify(master);
        sfxSlider.SetValueWithoutNotify(sfx);
        musicSlider.SetValueWithoutNotify(music);

       
        SetMasterVolume(master);
        SetSoundFXVolume(sfx);
        SetMusicVolume(music);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSoundFXVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(MasterKey, level);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(SfxKey, level);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(MusicKey, level);
    }
}
