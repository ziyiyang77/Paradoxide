using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetValueFromDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown1;
    [SerializeField] private TMP_Dropdown dropdown2;
    [SerializeField] private TMP_Dropdown dropdown3;
    [SerializeField] private Button submitButton;

    private int[] pickedEntries = new int[3];

    private GameData gameData;

    private void Start()
    {
        // Initialize game data (you might want to reference an existing GameData instance instead of creating a new one)
        gameData = GameDataManager.instance.gameData;

        // Add listeners to dropdowns to update picked entries
        dropdown1.onValueChanged.AddListener(delegate { UpdatePickedEntry(0, dropdown1.value); });
        dropdown2.onValueChanged.AddListener(delegate { UpdatePickedEntry(1, dropdown2.value); });
        dropdown3.onValueChanged.AddListener(delegate { UpdatePickedEntry(2, dropdown3.value); });
    }

    private void UpdatePickedEntry(int dropdownIndex, int value)
    {
        pickedEntries[dropdownIndex] = value;
    }

    public void SubmitSelections()
    {
        for (int i = 0; i < pickedEntries.Length; i++)
        {
            switch (pickedEntries[i])
            {
                case 0:
                    gameData.DecreasePollution(10.0f); // Decrease pollution percentage
                    gameData.DecreaseIncome(100.0f); // Decrease income
                    gameData.DecreaseBossSatisfaction(5.0f); // Decrease boss satisfaction
                    break;
                case 1:
                    gameData.IncreasePollution(10.0f); // Increase pollution percentage
                    // No change in income and boss satisfaction
                    break;
                case 2:
                    gameData.IncreasePollution(10.0f); // Increase pollution percentage
                    gameData.IncreaseIncome(100.0f); // Increase income
                    gameData.IncreaseBossSatisfaction(5.0f); // Increase boss satisfaction
                    break;
            }
        }

        Debug.Log($"Player Income: {gameData.playerIncome}");
        Debug.Log($"Pollution Percentage: {gameData.pollutionPercentage}");
        Debug.Log($"Boss Satisfaction: {gameData.bossSatisfaction}");

        // Lock down further changes to the dropdowns
        LockDownDropdowns();

        // Change the submit button color to red and make it non-interactable
        submitButton.interactable = false;
        ColorBlock colorBlock = submitButton.colors;
        colorBlock.normalColor = Color.red;
        submitButton.colors = colorBlock;

        // Set PlayerPrefs flag to indicate submission
        PlayerPrefs.SetInt("HasSubmitted", 1);
        PlayerPrefs.Save();
    }

    private void LockDownDropdowns()
    {
        dropdown1.interactable = false;
        dropdown2.interactable = false;
        dropdown3.interactable = false;
    }
}
