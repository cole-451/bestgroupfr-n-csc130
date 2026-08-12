using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A song chart. Contains a list of sections.
/// </summary>
[CreateAssetMenu(fileName = "NewChart", menuName = "BW/ChartDefinition")]
public class Chart : ScriptableObject
{
    public List<Section> sections;
}