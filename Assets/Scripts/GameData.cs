using UnityEngine;

[System.Serializable]
public class GameData
{
    public float pollutionPercentage;
    public float bossSatisfaction;
    public int currentMonth;

    public GameData()
    {
        pollutionPercentage = 5.0f;
        bossSatisfaction = 5.0f;
        currentMonth = 1;
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
