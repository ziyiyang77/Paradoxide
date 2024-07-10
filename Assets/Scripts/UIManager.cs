using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject pauseMenu;

    public Text incomeText;
    public Text pollutionText;
    public Text monthText;

    private void Update()
    {
        // Update UI elements with current game data
        incomeText.text = "Income: " + GameManager.instance.gameData.playerIncome;
        pollutionText.text = "Pollution: " + GameManager.instance.gameData.pollutionPercentage + "%";
        monthText.text = "Month: " + GameManager.instance.gameData.currentMonth;
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        gameUI.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ShowGameUI()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        mainMenu.SetActive(false);
        gameUI.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
