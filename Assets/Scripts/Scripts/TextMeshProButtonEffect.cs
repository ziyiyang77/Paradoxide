using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class TextMeshProButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private TextMeshProUGUI textMeshPro;
    private Color originalColor;
    private Shadow textShadow;
    private RectTransform rectTransform;
    private AudioSource audioSource;
    private Vector2 OriginalPosition;

    public float animationDuration = 0.5f;
    public Color hoverColor = Color.white;
    public Color clickColor = Color.clear;
    public Vector2 hoverShadowOffset = new Vector2(2f, -2f);
    public Color hoverShadowColor = Color.black;
    public Vector2 clickJumpOffset = new Vector2(0f, 20f);
    public AudioClip clickSound;

    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
            return;
        }
        originalColor = textMeshPro.color;
        
        textShadow = textMeshPro.GetComponent<Shadow>();
        if (textShadow == null)
        {
            textShadow = textMeshPro.gameObject.AddComponent<Shadow>();
        }

        rectTransform = textMeshPro.GetComponent<RectTransform>();
        OriginalPosition = rectTransform.anchoredPosition;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateTextEffect(hoverColor, hoverShadowColor, hoverShadowOffset));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateTextEffect(originalColor, Color.clear, Vector2.zero));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateClickEffect());
    }

    private IEnumerator AnimateTextEffect(Color targetColor, Color targetShadowColor, Vector2 targetShadowOffset)
    {
        float elapsedTime = 0f;
        Color startColor = textMeshPro.color;
        Color startShadowColor = textShadow.effectColor;
        Vector2 startShadowOffset = textShadow.effectDistance;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);

            textMeshPro.color = Color.Lerp(startColor, targetColor, t);
            textShadow.effectColor = Color.Lerp(startShadowColor, targetShadowColor, t);
            textShadow.effectDistance = Vector2.Lerp(startShadowOffset, targetShadowOffset, t);

            yield return null;
        }

        textMeshPro.color = targetColor;
        textShadow.effectColor = targetShadowColor;
        textShadow.effectDistance = targetShadowOffset;
    }

    private IEnumerator AnimateClickEffect()
    {
        float elapsedTime = 0f;
        Vector2 startPos = rectTransform.anchoredPosition;
        Vector2 targetPos = startPos + clickJumpOffset;

        audioSource.PlayOneShot(clickSound);

        while (elapsedTime < animationDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / (animationDuration / 2));

            rectTransform.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < animationDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / (animationDuration / 2));

            rectTransform.anchoredPosition = Vector2.Lerp(targetPos, startPos, t);

            yield return null;
        }

        // 重置为初始位置
        rectTransform.anchoredPosition = OriginalPosition;
        textMeshPro.color = originalColor;
    }

}
