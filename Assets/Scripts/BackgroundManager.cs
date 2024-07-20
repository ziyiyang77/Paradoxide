using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public Sprite backgroundLowPollution;
    public Sprite backgroundMediumPollution;
    public Sprite backgroundHighPollution;

    private SpriteRenderer spriteRenderer;
    private GameDataForUI gameDataForUI;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameDataForUI = GameDataManager.instance.gameDataForUI;
        UpdateBackground();
    }

    public void UpdateBackground()
    {
        float pollution = gameDataForUI.pollutionPercentage;

        if (pollution < 3)
        {
            spriteRenderer.sprite = backgroundHighPollution;
        }
        else if (pollution >= 3 && pollution < 7)
        {
            spriteRenderer.sprite = backgroundMediumPollution;
        }
        else
        {
            spriteRenderer.sprite = backgroundLowPollution;
        }
    }
}
