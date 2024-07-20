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
    [SerializeField] private TMP_Dropdown dropdown4;
    [SerializeField] private Button submitButton;

    private int[] pickedEntries = new int[4];
    private int choiceBias = 0;

    private GameData gameData;

    private Color lightGrey = new Color(211 / 255f, 211 / 255f, 211 / 255f);
    private Color semiTransparentRed = new Color(190 / 255f, 28 / 255f, 28 / 255f, 128 / 255f);

    private void Start()
    {
        // Initialize game data (you might want to reference an existing GameData instance instead of creating a new one)
        gameData = GameDataManager.instance.gameData;

        // Add listeners to dropdowns to update picked entries
        dropdown1.onValueChanged.AddListener(delegate { UpdatePickedEntry(0, dropdown1.value); });
        dropdown2.onValueChanged.AddListener(delegate { UpdatePickedEntry(1, dropdown2.value); });
        dropdown3.onValueChanged.AddListener(delegate { UpdatePickedEntry(2, dropdown3.value); });
        dropdown4.onValueChanged.AddListener(delegate { UpdatePickedEntry(3, dropdown4.value); });

        // Initialize the submit button state and color
        UpdateSubmitButtonState();
    }

    private void UpdatePickedEntry(int dropdownIndex, int value)
    {
        pickedEntries[dropdownIndex] = value;
        UpdateChoiceBias();
        UpdateSubmitButtonState();
    }

    private void UpdateChoiceBias()
    {
        choiceBias = 0;
        for (int i = 0; i < pickedEntries.Length; i++)
        {
            switch (pickedEntries[i])
            {
                case 1:
                    choiceBias -= 1;
                    break;
                case 3:
                    choiceBias += 1;
                    break;
                    // case 1 does nothing
            }
        }
    }

    private void UpdateSubmitButtonState()
    {
        // Check if any dropdown has selected index 0
        bool canSubmit = true;
        for (int i = 0; i < pickedEntries.Length; i++)
        {
            if (pickedEntries[i] == 0)
            {
                canSubmit = false;
                break;
            }
        }
        submitButton.interactable = canSubmit;

        // Update the submit button color based on interactable state
        ColorBlock colorBlock = submitButton.colors;
        colorBlock.disabledColor = canSubmit ? Color.white : lightGrey;
        submitButton.colors = colorBlock;
    }

    public void SubmitSelections()
    {
        Debug.Log("choice bias is" + choiceBias);

        if (choiceBias < 0)
        {
            gameData.IncreaseBossSatisfaction(2.0f); // Increase boss satisfaction
            gameData.DecreasePollution(2.0f); // Decrease pollution percentage
            MessageManager.bossSatisfaction = 1; // Set message manager boss satisfaction
            MessageManager.environmentLevel = -1; // Set message manager environment level
        }
        else if (choiceBias > 0)
        {
            gameData.DecreaseBossSatisfaction(2.0f); // Decrease boss satisfaction
            gameData.IncreasePollution(2.0f); // Increase pollution percentage
            MessageManager.bossSatisfaction = -1; // Set message manager boss satisfaction
            MessageManager.environmentLevel = 1; // Set message manager environment level
        }
        else
        {
            MessageManager.bossSatisfaction = 0; // Set message manager boss satisfaction
            MessageManager.environmentLevel = 0; // Set message manager environment level
        }

        Debug.Log($"Pollution Percentage: {gameData.pollutionPercentage}");
        Debug.Log($"Boss Satisfaction: {gameData.bossSatisfaction}");

        // Lock down further changes to the dropdowns
        LockDownDropdowns();

        // Change the submit button color to red and make it non-interactable
        submitButton.interactable = false;
        ColorBlock colorBlock = submitButton.colors;
        colorBlock.disabledColor = semiTransparentRed;
        submitButton.colors = colorBlock;

        // Set GameStateManager flag to indicate submission
        GameStateManager.instance.hasSubmitted = true;
    }

    private void LockDownDropdowns()
    {
        dropdown1.interactable = false;
        dropdown2.interactable = false;
        dropdown3.interactable = false;
        dropdown4.interactable = false;
    }
}
