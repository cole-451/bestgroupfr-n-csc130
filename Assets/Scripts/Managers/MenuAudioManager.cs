using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;



/// <summary>
/// Central audio hub: pooled SFX playback, crossfading music, and mixer volume control persisted via PlayerPrefs.
/// Attach to an "MenuAudioManager" GameObject. 
/// Assign the Mixer and two empty child AudioSources (for music A/B) in the Inspector.
/// Mixer must have exposed float params: MasterVolume, MusicVolume, SFXVolume.
/// </summary>
public class MenuAudioManager : MonoBehaviour
{
    public static MenuAudioManager Instance { get; private set; }


    [Header("Mixer")]
    public AudioMixer mixer;
    public AudioMixerGroup sfxGroup;

    [Header("Music (assign two empty AudioSource children)")]
    public AudioSource musicSourceA;
    public AudioSource musicSourceB;
    private AudioSource activeMusicSource;
    private AudioSource inactiveMusicSource;

    [Header("SFX Pool")]
    public int sfxPoolSize = 6;
    private List<AudioSource> sfxPool = new List<AudioSource>();
    private int sfxPoolIndex = 0;

    private const string MasterParam = "MasterVolume";
    private const string MusicParam = "MusicVolume";
    private const string SFXParam = "SFXVolume";




    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        activeMusicSource = musicSourceA;
        inactiveMusicSource = musicSourceB;

        for (int i = 0; i < sfxPoolSize; i++)
        {
            GameObject go = new GameObject("SFX_Voice_" + i);
            go.transform.SetParent(transform);
            AudioSource src = go.AddComponent<AudioSource>();
            src.outputAudioMixerGroup = sfxGroup;
            sfxPool.Add(src);
        }

        LoadVolumePrefs();
    }


    // ---- SFX ----

    // Call this instead of AudioSource.PlayOneShot directly so rapid
    // hover/click sounds don't stomp on each other.
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        AudioSource src = sfxPool[sfxPoolIndex];
        sfxPoolIndex = (sfxPoolIndex + 1) % sfxPool.Count;
        src.PlayOneShot(clip, volume);
    }





    // ---- Music ----

    // Crossfades from whatever is currently playing into newClip.
    public void PlayMusic(AudioClip clip, float fadeDuration = 1f, bool loop = true)
    {
        StartCoroutine(CrossfadeMusic(clip, fadeDuration, loop));
    }

    private IEnumerator CrossfadeMusic(AudioClip newClip, float duration, bool loop)
    {
        inactiveMusicSource.clip = newClip;
        inactiveMusicSource.loop = loop;
        inactiveMusicSource.volume = 0f;
        inactiveMusicSource.Play();

        float t = 0f;
        float startVolActive = activeMusicSource.volume;

        while (t < duration)
        {
            t += Time.deltaTime;
            float lerp = t / duration;
            activeMusicSource.volume = Mathf.Lerp(startVolActive, 0f, lerp);
            inactiveMusicSource.volume = Mathf.Lerp(0f, 1f, lerp);
            yield return null;
        }

        activeMusicSource.Stop();
        activeMusicSource.volume = 0f;
        inactiveMusicSource.volume = 1f;

        // swap roles for next crossfade
        var temp = activeMusicSource;
        activeMusicSource = inactiveMusicSource;
        inactiveMusicSource = temp;
    }

    // ---- Volume control (wire directly to Slider.OnValueChanged, 0-1 range) ----

    public void SetMasterVolume(float linear01)
    {
        SetMixerVolume(MasterParam, linear01);
        PlayerPrefs.SetFloat(MasterParam, linear01);
    }

    public void SetMusicVolume(float linear01)
    {
        SetMixerVolume(MusicParam, linear01);
        PlayerPrefs.SetFloat(MusicParam, linear01);
    }

    public void SetSFXVolume(float linear01)
    {
        SetMixerVolume(SFXParam, linear01);
        PlayerPrefs.SetFloat(SFXParam, linear01);
    }

    private void SetMixerVolume(string param, float linear01)
    {
        // Sliders are linear 0-1, mixer wants decibels (-80 to 0).
        float dB = linear01 > 0.0001f ? Mathf.Log10(linear01) * 20f : -80f;
        mixer.SetFloat(param, dB);
    }

    private void LoadVolumePrefs()
    {
        SetMasterVolume(PlayerPrefs.GetFloat(MasterParam, 1f));
        SetMusicVolume(PlayerPrefs.GetFloat(MusicParam, 0.8f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFXParam, 1f));
    }


}
