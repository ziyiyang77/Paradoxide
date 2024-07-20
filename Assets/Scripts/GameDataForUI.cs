using UnityEngine;

[System.Serializable]
public class GameDataForUI
{
    public float pollutionPercentage;
    public float bossSatisfaction;
    public int currentMonth;

    public GameDataForUI()
    {
        pollutionPercentage = 5.0f;
        bossSatisfaction = 5.0f;
        currentMonth = 1;
    }

    public void UpdateData(GameData gameData)
    {
        pollutionPercentage = gameData.pollutionPercentage;
        bossSatisfaction = gameData.bossSatisfaction;
        currentMonth = gameData.currentMonth;
    }
}
