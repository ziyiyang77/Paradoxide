using UnityEngine;

[System.Serializable]
public class GameData
{
    public float playerIncome;
    public float pollutionPercentage;
    public float bossSatisfaction;
    public int currentMonth;

    public GameData()
    {
        playerIncome = 1000.0f;
        pollutionPercentage = 50.0f;
        bossSatisfaction = 30.0f;
        currentMonth = 1;
    }

    // Methods to update income and pollution
    public void IncreaseIncome(float amount)
    {
        playerIncome += amount;
    }

    public void DecreaseIncome(float amount)
    {
        playerIncome -= amount;
    }

    public void IncreasePollution(float amount)
    {
        pollutionPercentage += amount;
    }

    public void DecreasePollution(float amount)
    {
        pollutionPercentage -= amount;
    }

    public void IncreaseBossSatisfaction(float amount)
    {
        bossSatisfaction += amount;
    }

    public void DecreaseBossSatisfaction(float amount)
    {
        bossSatisfaction -= amount;
    }

    public void AdvanceMonth()
    {
        currentMonth++;
    }

    public bool IsGameOver()
    {
        return currentMonth > 12; // Game lasts for 12 months
    }
}
