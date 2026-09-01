using System;
using UnityEngine;

/// <summary>
/// Represents the color of a note.
/// </summary>
public enum NoteColor
{
    Green,
    Red,
    Yellow,
    Blue,
    Orange
}

/// <summary>
/// Represents the input type of a note.
/// </summary>
public enum NoteType
{
    Tap,
    Strum,
    Sustain
}

/// <summary>
/// Represents a note in the game. Contains color, type, and beat (0-15, in sixteenth-note
/// slots within its measure). Sustain notes additionally use sustainLength to describe how
/// many sixteenth-note ticks the hold lasts.
/// NOTE: if a measure's timeSignatureNumerator gives it fewer than 16 slots (e.g. 3/4 = 12),
/// make sure beat values for notes in that measure stay within range, or they'll bleed into
/// the timing of the next measure.
/// </summary>
[Serializable]
public class Note
{
    [SerializeField] public NoteColor color;
    [SerializeField] public NoteType type;
    [SerializeField, Range(0, 15)] public int beat;

    [SerializeField, Tooltip("Only use if sustain note. Length of the hold, in sixteenth-note ticks.")]
    public int sustainLength;
}