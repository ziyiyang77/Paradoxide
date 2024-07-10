using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

    // Method to complete a round and advance the month
    public void CompleteRound()
    {
        gameData.AdvanceMonth();

        if (gameData.IsGameOver())
        {
            EndGame();
        }
        else
        {
            // Code to handle the start of a new month
        }
    }

    private void EndGame()
    {
        // Code to handle end of game, show results, etc.
        Debug.Log("Game Over");
    }

    // Add methods to increase/decrease income and pollution
    public void IncreaseIncome(float amount)
    {
        gameData.IncreaseIncome(amount);
    }

    public void DecreaseIncome(float amount)
    {
        gameData.DecreaseIncome(amount);
    }

    public void IncreasePollution(float amount)
    {
        gameData.IncreasePollution(amount);
    }

    public void DecreasePollution(float amount)
    {
        gameData.DecreasePollution(amount);
    }
}
