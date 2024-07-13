using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdateDropdownsAndReport : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown1;
    [SerializeField] private TMP_Dropdown dropdown2;
    [SerializeField] private TMP_Dropdown dropdown3;
    [SerializeField] private TMP_Dropdown dropdown4;

    [SerializeField] private Text reportText1;
    [SerializeField] private Text reportText2;
    [SerializeField] private Text reportText3;
    [SerializeField] private Text reportText4;

    private GameData gameData;
    private List<Dictionary<string, object>> data;

    private void Start()
    {
        // Initialize game data
        gameData = GameDataManager.instance.gameData;

        // Load data from CSV
        data = CSVReader.Read("dropdown_report_data");

        //// Debug: Check loaded data
        //foreach (var row in data)
        //{
        //    foreach (var key in row.Keys)
        //    {
        //        Debug.Log($"Key: {key}, Value: {row[key]}");
        //    }
        //}

        // Update dropdown and report values based on currentMonth
        UpdateValues(gameData.currentMonth);
    }

    private void UpdateValues(int month)
    {
        foreach (var row in data)
        {
            int rowMonth;
            if (int.TryParse(row["Month"].ToString(), out rowMonth) && rowMonth == month)
            {
                UpdateDropdown(dropdown1, row, "Dropdown1_1", "Dropdown1_2", "Dropdown1_3");
                UpdateDropdown(dropdown2, row, "Dropdown2_1", "Dropdown2_2", "Dropdown2_3");
                UpdateDropdown(dropdown3, row, "Dropdown3_1", "Dropdown3_2", "Dropdown3_3");
                UpdateDropdown(dropdown4, row, "Dropdown4_1", "Dropdown4_2", "Dropdown4_3");

                UpdateReportText(reportText1, row, "Report1");
                UpdateReportText(reportText2, row, "Report2");
                UpdateReportText(reportText3, row, "Report3");
                UpdateReportText(reportText4, row, "Report4");
                break;
            }
        }
    }


    private void UpdateDropdown(TMP_Dropdown dropdown, Dictionary<string, object> row, string key1, string key2, string key3)
    {
        if (row.ContainsKey(key1) && row.ContainsKey(key2) && row.ContainsKey(key3))
        {
            dropdown.options[1].text = row[key1].ToString();
            dropdown.options[2].text = row[key2].ToString();
            dropdown.options[3].text = row[key3].ToString();
        }
        else
        {
            Debug.LogError($"Keys {key1}, {key2}, or {key3} not found in the row.");
        }
    }

    private void UpdateReportText(Text text, Dictionary<string, object> row, string key)
    {
        if (row.ContainsKey(key))
        {
            text.text = row[key].ToString();
        }
        else
        {
            Debug.LogError($"Key {key} not found in the row.");
        }
    }
}
