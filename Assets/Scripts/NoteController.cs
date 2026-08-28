using UnityEngine;

/// <summary>
/// Attached to the note prefab. Moves the note toward the strike line at a constant
/// </summary>
public class NoteController : MonoBehaviour
{
    [SerializeField] private float despawnBuffer = 2f;

    public Note NoteData { get; private set; }

    private float fallSpeed;
    private float strikeLineY;

    public void Initialize(Note note, float speed, float strikeY)
    {
        NoteData = note;
        fallSpeed = speed;
        strikeLineY = strikeY;
    }

    private void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < strikeLineY - despawnBuffer)
        {
            Destroy(gameObject);
        }
    }
}