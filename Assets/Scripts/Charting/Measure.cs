using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A measure in the song. Contains a list of notes and the time signature numerator.
/// The list of notes must not exceed the amount of sixteenth notes that would fit in the time signature.
/// (ex. 4/4 time signature would allow for 16 sixteenth notes, so the list of notes must not exceed 16 elements).
/// </summary>
[CreateAssetMenu(fileName = "NewMeasure", menuName = "BW/MeasureDefinition")]
public class Measure : ScriptableObject
{
    public List<Note> notes;
    public int timeSignatureNumerator;
}