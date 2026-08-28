using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Fret : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color color;
    private bool isHeld = false;

    private float OnAlpha = 1f;
    private float OffAlpha = 0.4f;

    [SerializeField] private NoteColor fretColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        color = spriteRenderer.color;

        SetAlpha(OffAlpha);

    }

    public void SetHeld(bool held)
    {
        isHeld = held;
        if (held) SetAlpha(OnAlpha);
        else SetAlpha(OffAlpha);
    }

    private void SetAlpha(float alphaValue)
    {
        color.a = alphaValue;
        spriteRenderer.color = color;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        TryHit(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryHit(other);
    }

    private void TryHit(Collider2D other)
    {
        Debug.Log($"Trigger hit: {other.name}, isHeld={isHeld}, tag={other.tag}");
        if (!isHeld) return;
        if (!other.CompareTag("Note")) return;

        var note = other.GetComponent<NoteColorTag>();
        if (note == null) return;

        Debug.Log($"Note color: {note.Color}, Fret color: {fretColor}, Match: {note.Color == fretColor}");

        if (note.Color == fretColor)
        {
            Destroy(other.gameObject);
            // TODO: hit VFX, score++, combo, etc.
        }

    }
}