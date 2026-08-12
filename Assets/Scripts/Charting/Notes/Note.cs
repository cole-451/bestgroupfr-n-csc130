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
/// Represents a note in the game. Contains color, type, and beat(0-15).
/// </summary>
public class Note : MonoBehaviour
{
    [SerializeField] public NoteColor color;
    [SerializeField] public NoteType type;
    [SerializeField, Range(0, 15)] public int beat;
}
