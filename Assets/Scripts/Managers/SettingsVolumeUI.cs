using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach to the OptionsPanel GameObject. Syncs slider handle positions
/// to the saved volume prefs every time the panel is opened, without
/// re-triggering AudioManager's SetXVolume methods.
/// </summary>
public class SettingsVolumeUI : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void OnEnable()
    {
        masterSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("MasterVolume", 1f));
        musicSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("MusicVolume", 0.8f));
        sfxSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFXVolume", 1f));
    }
}