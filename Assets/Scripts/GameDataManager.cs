using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    public GameData gameData;
    public GameDataForUI gameDataForUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameData = new GameData();
            gameDataForUI = new GameDataForUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateUIData()
    {
        gameDataForUI.UpdateData(gameData);
    }
}
