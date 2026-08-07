using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A section in the song. Contains a list of measures.
/// The name of the section must be unique.
/// </summary>
[CreateAssetMenu(fileName = "NewSection", menuName = "BW/SectionDefinition")]
public class Section : ScriptableObject
{
    public string sectionName;
    public List<Measure> measures;
}