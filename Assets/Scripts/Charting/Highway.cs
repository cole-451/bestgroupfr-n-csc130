using System.Collections.Generic;
using UnityEngine;

public class Highway : Singleton<Highway>
{
    [SerializeField] private Chart chart;
    [SerializeField] private SongInfo songInfo;
    [SerializeField] private GameObject notePrefab; // must have a NoteController component
    [SerializeField] private Transform greenSpawn;
    [SerializeField] private Transform redSpawn;
    [SerializeField] private Transform yellowSpawn;
    [SerializeField] private Transform blueSpawn;
    [SerializeField] private Transform orangeSpawn;
    [SerializeField] private Transform generalSpawn; // used only for measuring fall distance
    [SerializeField] private Transform strikeLine;
    [SerializeField] private int noteBufferCount = 8; // number of 16th notes early a note spawns

    private List<(int absoluteTick, Note note)> timeline = new();
    private Dictionary<NoteColor, Transform> spawnPoints;
    private int nextIndex = 0;
    private float fallSpeed;

    protected override void Awake()
    {
        base.Awake();

        spawnPoints = new Dictionary<NoteColor, Transform>
        {
            { NoteColor.Green, greenSpawn },
            { NoteColor.Red, redSpawn },
            { NoteColor.Yellow, yellowSpawn },
            { NoteColor.Blue, blueSpawn },
            { NoteColor.Orange, orangeSpawn },
        };
    }

    void Start()
    {
        BuildTimeline();

        BPMTick.Instance.SetBPM(songInfo.bpm);

        float distance = Mathf.Abs(generalSpawn.position.y - strikeLine.position.y);
        fallSpeed = distance / (noteBufferCount * BPMTick.Instance.TickInterval);

        BPMTick.Instance.Play(songInfo.leadAudioClip);
    }

    void OnEnable() => BPMTick.OnTick += HandleTick;
    void OnDisable() => BPMTick.OnTick -= HandleTick;

    void BuildTimeline()
    {
        int cumulativeTicks = 0;

        foreach (Section section in chart.sections)
        {
            foreach (Measure measure in section.measures)
            {
                // ticks per measure (depends on its own time signature)
                int ticksInMeasure = measure.timeSignatureNumerator * 4;

                foreach (Note note in measure.notes)
                {
                    int absoluteTick = cumulativeTicks + note.beat;
                    timeline.Add((absoluteTick, note));
                }

                cumulativeTicks += ticksInMeasure;
            }
        }

        timeline.Sort((a, b) => a.absoluteTick.CompareTo(b.absoluteTick));
    }

    void HandleTick()
    {
        int currentTick = BPMTick.Instance.GetCurrentTick();
        int targetTick = currentTick + noteBufferCount;

        while (nextIndex < timeline.Count && timeline[nextIndex].absoluteTick == targetTick)
        {
            SpawnNote(timeline[nextIndex].note);
            nextIndex++;
        }
    }

    void SpawnNote(Note note)
    {
        if (!spawnPoints.TryGetValue(note.color, out Transform spawn) || spawn == null)
        {
            Debug.LogWarning($"Highway: no spawn point assigned for note color {note.color}");
            return;
        }

        GameObject noteObj = Instantiate(notePrefab, spawn.position, Quaternion.identity);
        NoteController controller = noteObj.GetComponent<NoteController>();

        if (controller == null)
        {
            Debug.LogError("Highway: notePrefab is missing a NoteController component.");
            return;
        }

        controller.Initialize(note, fallSpeed, strikeLine.position.y);
    }
}