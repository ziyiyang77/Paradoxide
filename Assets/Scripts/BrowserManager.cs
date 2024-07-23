using UnityEngine;

public class BrowserManager : MonoBehaviour
{
    public Sprite news1;
    public Sprite news2;
    public Sprite news3;
    public Sprite news4;
    public Sprite news5;

    private SpriteRenderer spriteRenderer;
    private GameDataForUI gameDataForUI;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameDataForUI = GameDataManager.instance.gameDataForUI;
        UpdateBrowserSprite();
    }

    public void UpdateBrowserSprite()
    {
        int currentMonth = gameDataForUI.currentMonth;

        Sprite newSprite = null;

        switch (currentMonth)
        {
            case 1:
            case 2:
            case 3:
                newSprite = news1;
                break;
            case 4:
            case 5:
            case 6:
                newSprite = news2;
                break;
            case 7:
            case 8:
            case 9:
                newSprite = news3;
                break;
            case 10:
            case 11:
            case 12:
                newSprite = news4;
                break;
            default:
                // Optional: handle unexpected months if necessary
                break;
        }


        if (newSprite != null && spriteRenderer.sprite != newSprite)
        {
            spriteRenderer.sprite = newSprite;
        }
    }
}
