using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MainPageUIManager : MonoBehaviour
{
    [SerializeField] private Slider pollutionSlider;
    [SerializeField] private Slider satisfactionSlider;
    [SerializeField] private Text monthText;
    [SerializeField] private Button nextDayButton;

    public static float playerIncome;
    public static float pollutionPercentage;
    public static float bossSatisfaction;
    public static int currentMonth = 1;

    private static int monthCount = 0;

    private void Start()
    {
        if (monthCount != currentMonth)
        {
            UpdateData();
            monthCount++;
        }

        UpdateUI();

        nextDayButton.interactable = false;

        // Check GameStateManager flag and enable button if submission has been made
        if (GameStateManager.instance.hasSubmitted)
        {
            EnableNextDayButton();
        }
    }

    private void UpdateUI()
    {
        pollutionSlider.value = pollutionPercentage;
        satisfactionSlider.value = bossSatisfaction;
        monthText.text = currentMonth.ToString();
    }

    private void UpdateData()
    {
        GameData data = GameDataManager.instance.gameData;

        pollutionPercentage = data.pollutionPercentage / 10f;
        bossSatisfaction = data.bossSatisfaction / 10f;
        currentMonth = data.currentMonth;
    }

    public void EnableNextDayButton()
    {
        nextDayButton.interactable = true;
    }

    public void OnNextDayButtonClicked()
    {
        // Set firstTimeClick in MessageManager to false
        MessageManager.firstTimeClick = true;

        NextDayRoutine();
    }

    private void NextDayRoutine()
    {
        GameData data = GameDataManager.instance.gameData;

        if (data.bossSatisfaction < 2)
        {
            SceneTransitionManager.instance.FadeAndLoadScene("BE1");
        }
        else if (data.pollutionPercentage < 2)
        {
            SceneTransitionManager.instance.FadeAndLoadScene("BE2");
        }

        // Fade to black
        SceneTransitionManager.instance.FadeAndLoadScene("Main");

        // Update game data
        data.AdvanceMonth();

        if (data.IsGameOver())
        {
            SceneTransitionManager.instance.FadeAndLoadScene("End");
        }
        // Simulate some delay
        //yield return new WaitForSeconds(1.0f);

        // Update the UI after returning from the computer scene
        UpdateUI();
        UpdateData();

        // Optionally disable the button again
        nextDayButton.interactable = false;

        // Reset GameStateManager flag
        GameStateManager.instance.hasSubmitted = false;
        Debug.Log("submit false");

        
    }
}
