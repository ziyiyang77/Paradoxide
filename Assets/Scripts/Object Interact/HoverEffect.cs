using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HoverEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isHovering = false;
    private float fadeSpeed = 5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color; // Start with alpha 0
        }
        else
        {
            Debug.LogError("SpriteRenderer component is missing.");
        }
    }

    private void Update()
    {
        if (spriteRenderer != null)
        {
            if (isHovering)
            {
                if (spriteRenderer.color.a < 1)
                {
                    Color color = spriteRenderer.color;
                    color.a = Mathf.Min(spriteRenderer.color.a + fadeSpeed * Time.deltaTime, 1);
                    spriteRenderer.color = color;
                }
            }
            else
            {
                if (spriteRenderer.color.a > 0)
                {
                    Color color = spriteRenderer.color;
                    color.a = Mathf.Max(spriteRenderer.color.a - fadeSpeed * Time.deltaTime, 0);
                    spriteRenderer.color = color;
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        isHovering = true;
    }

    private void OnMouseExit()
    {
        isHovering = false;
    }
}
