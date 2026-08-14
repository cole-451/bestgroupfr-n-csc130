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

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        color = spriteRenderer.color;

        SetAlpha(OffAlpha);

    }

    public void SetHeld(bool held)
    {
        if (held) SetAlpha(OnAlpha);
        else SetAlpha(OffAlpha);
    }

    private void SetAlpha(float alphaValue)
    {
        color.a = alphaValue;
        spriteRenderer.color = color;
    }
}