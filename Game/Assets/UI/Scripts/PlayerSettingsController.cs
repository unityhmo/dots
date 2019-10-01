using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsController : MonoBehaviour {
    public enum Setting {
        Music,
        Sfx
    }

    public static readonly Dictionary<Setting, float> defaultValues = new Dictionary<Setting, float>() {
        {Setting.Music, 1f},
        {Setting.Sfx, 1f},
    };

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private float musicLevel;
    private float sfxLevel;
    private bool isDirty;

    private readonly Action<string, float> updateSetting = (key, value) => {
        PlayerPrefs.SetFloat(key, value);
        // h4ck5
        AudioManagerController.Instance.LoadAudioLevelFromProps();
    };

    void Start() {
        musicLevel = GetFloat(Setting.Music, defaultValues[Setting.Music]);
        sfxLevel = GetFloat(Setting.Sfx, defaultValues[Setting.Sfx]);
        musicSlider.value = musicLevel;
        sfxSlider.value = sfxLevel;

        musicSlider.onValueChanged.AddListener(value => updateSetting(Setting.Music.ToString(), value));
        sfxSlider.onValueChanged.AddListener(value => updateSetting(Setting.Sfx.ToString(), value));
    }

    public void Reset() {
        musicLevel = defaultValues[Setting.Music];
        sfxLevel = defaultValues[Setting.Sfx];

        SetFloat(Setting.Music, musicLevel);
        SetFloat(Setting.Sfx, sfxLevel);

        musicSlider.value = musicLevel;
        sfxSlider.value = sfxLevel;
    }

    private void SetFloat(Setting setting, float value) {
        PlayerPrefs.SetFloat(setting.ToString(), value);
    }

    public static float GetFloat(Setting setting, float value) {
        return PlayerPrefs.GetFloat(setting.ToString(), value);
    }
}