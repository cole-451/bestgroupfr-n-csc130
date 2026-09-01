using System;
using UnityEngine;

/// <summary>
/// Fires OnTick once per sixteenth note, driven off an AudioSource's playback time
/// rather than accumulated Time.deltaTime. This keeps note timing locked to the actual
/// song audio instead of slowly drifting out of sync over the course of a track.
/// Requires an AudioSource assigned in the inspector (add one to this GameObject).
/// </summary>
public class BPMTick : Singleton<BPMTick>
{
    public static event Action OnTick;

    [SerializeField] private AudioSource conductorSource;
    [SerializeField] private float bpm = 120f;

    private float secPerTick; // duration of one sixteenth note, in seconds
    private int currentTick;

    public float TickInterval => secPerTick;

    protected override void Awake()
    {
        base.Awake();
        RecalculateTickInterval();
    }

    private void Update()
    {
        if (conductorSource == null || !conductorSource.isPlaying) return;

        int tickNow = Mathf.FloorToInt(conductorSource.time / secPerTick);

        // while loop (not if) so a single dropped/slow frame can't cause a missed note spawn
        while (currentTick < tickNow)
        {
            currentTick++;
            OnTick?.Invoke();
        }
    }

    /// <summary>
    /// Call before Play() to set the tempo ticks should be generated at.
    /// </summary>
    public void SetBPM(float newBpm)
    {
        bpm = newBpm;
        RecalculateTickInterval();
    }

    /// <summary>
    /// Starts the song and resets tick tracking. Call SetBPM() first.
    /// </summary>
    public void Play(AudioClip clip)
    {
        conductorSource.clip = clip;
        conductorSource.Play();
        currentTick = 0;
    }

    private void RecalculateTickInterval()
    {
        secPerTick = (60f / bpm) / 4f; // quarter note duration / 4 = sixteenth note duration
    }

    public int GetCurrentTick() => currentTick;
}