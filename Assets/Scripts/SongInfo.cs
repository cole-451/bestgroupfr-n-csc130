using System;
using UnityEngine;

/// <summary>
/// SongInfo class. Holds the following:
/// Name of song.
/// Artist name.
/// Genre.
/// Audio clips for lead, bass, vocals, and drums.
/// </summary>
[Serializable]
public struct SongInfo
{
    public readonly string songName;
    public readonly string artistName;
    public readonly string genre;
    public readonly AudioClip leadAudioClip;
    public readonly AudioClip bassAudioClip;
    public readonly AudioClip vocalsAudioClip;
    public readonly AudioClip drumsAudioClip;

    public SongInfo(string songName, string artistName, string genre, AudioClip leadAudioClip, AudioClip bassAudioClip, AudioClip vocalsAudioClip, AudioClip drumsAudioClip)
    {
        this.songName = songName;
        this.artistName = artistName;
        this.genre = genre;
        this.leadAudioClip = leadAudioClip;
        this.bassAudioClip = bassAudioClip;
        this.vocalsAudioClip = vocalsAudioClip;
        this.drumsAudioClip = drumsAudioClip;
    }
}