using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndSceneUI : MonoBehaviour
{
    [SerializeField] private Slider pollutionSlider;
    [SerializeField] private Slider satisfactionSlider;

    private float pollutionPercentage;
    private float bossSatisfaction;

    private void Start()
    {
        UpdateData();
        UpdateUI();
    }

    private void UpdateData()
    {
        GameData data = GameDataManager.instance.gameData;

        pollutionPercentage = data.pollutionPercentage / 10f;
        bossSatisfaction = data.bossSatisfaction / 10f;
    }

    private void UpdateUI()
    {
        pollutionSlider.value = pollutionPercentage;
        satisfactionSlider.value = bossSatisfaction;
    }
}
