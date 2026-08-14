using UnityEngine;

public class Highway : Singleton<Highway>
{
    [SerializeField] private Chart chart;

    // Some sort of chart building function breaking down the chart into segments and creating the highway
}