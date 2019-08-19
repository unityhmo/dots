using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsController : MonoBehaviour {
    private enum Setting {
        Music,
        Sfx
    }

    private Dictionary<Setting, int> defaultValues = new Dictionary<Setting, int>() {
        {Setting.Music, 10},
        {Setting.Sfx, 10},
    };

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private int musicLevel;
    private int sfxLevel;
    private bool isDirty;

    private readonly Action<string, float> updateSetting = (key, value) => PlayerPrefs.SetInt(key, (int) value);

    void Start() {
        musicLevel = GetInt(Setting.Music, 10);
        sfxLevel = GetInt(Setting.Sfx, 10);
        musicSlider.value = musicLevel;
        sfxSlider.value = sfxLevel;

        musicSlider.onValueChanged.AddListener(value => updateSetting(Setting.Music.ToString(), value));
        sfxSlider.onValueChanged.AddListener(value => updateSetting(Setting.Sfx.ToString(), value));
    }

    public void Reset() {
        musicLevel = defaultValues[Setting.Music];
        sfxLevel = defaultValues[Setting.Sfx];

        SetInt(Setting.Music, musicLevel);
        SetInt(Setting.Sfx, sfxLevel);

        musicSlider.value = musicLevel;
        sfxSlider.value = sfxLevel;
    }

    private int GetInt(Setting setting, int value) {
        return PlayerPrefs.GetInt(setting.ToString(), value);
    }

    private void SetInt(Setting setting, int value) {
        PlayerPrefs.SetInt(setting.ToString(), value);
    }
}