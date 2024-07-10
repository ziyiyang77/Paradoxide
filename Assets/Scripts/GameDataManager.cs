using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    public GameData gameData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameData = new GameData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
