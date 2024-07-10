using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MainPageUIManager : MonoBehaviour
{
    [SerializeField] private Slider pollutionSlider;
    [SerializeField] private Slider satisfactionSlider;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI monthText;
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

        // Check PlayerPrefs flag and enable button if submission has been made
        if (PlayerPrefs.GetInt("HasSubmitted", 0) == 1)
        {
            EnableNextDayButton();
        }
    }

    private void UpdateUI()
    {
        pollutionSlider.value = pollutionPercentage;
        satisfactionSlider.value = bossSatisfaction;
        incomeText.text = $"Income: ${playerIncome}";
        monthText.text = $"Month: {currentMonth}";
    }

    private void UpdateData()
    {
        GameData data = GameDataManager.instance.gameData;

        pollutionPercentage = data.pollutionPercentage / 100f;
        bossSatisfaction = data.bossSatisfaction / 100f;
        playerIncome = data.playerIncome;
        currentMonth = data.currentMonth;
    }

    public void EnableNextDayButton()
    {
        nextDayButton.interactable = true;
    }

    public void OnNextDayButtonClicked()
    {
        StartCoroutine(NextDayRoutine());
    }

    private IEnumerator NextDayRoutine()
    {
        // Fade to black
        SceneTransitionManager.instance.FadeAndLoadScene("Main");

        // Update game data
        GameData data = GameDataManager.instance.gameData;
        data.AdvanceMonth();

        // Simulate some delay
        yield return new WaitForSeconds(1.0f);

        // Update the UI after returning from the computer scene
        UpdateUI();
        UpdateData();

        // Optionally disable the button again
        nextDayButton.interactable = false;

        // Reset PlayerPrefs flag
        PlayerPrefs.SetInt("HasSubmitted", 0);
        PlayerPrefs.Save();
    }
}
