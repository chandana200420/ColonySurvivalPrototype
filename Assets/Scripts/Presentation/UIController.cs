using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("Main UI")]
    [SerializeField] private  TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Food Panel")]
    [SerializeField] private TextMeshProUGUI foodText;
    [SerializeField] private TextMeshProUGUI     foodDaysText;

    [Header("Water Panel")]
    [SerializeField] private TextMeshProUGUI waterText;
    [SerializeField] private TextMeshProUGUI waterDaysText;

    public void UpdateUI(ColonySimulation simulation)
    {
        if (simulation == null)
            return;

        ColonyState state = simulation.State;

        dayText.text = $"DAY {state.Day}";

        foodText.text = $"FOOD\n{state.Food:F1}";
        foodDaysText.text = $"Food Days Left: {simulation.FoodDaysRemaining():F1}";

        waterText.text = $"WATER\n{state.Water:F1}";
        waterDaysText.text = $"Water Days Left: {simulation.WaterDaysRemaining():F1}";

        if (simulation.IsStarving())
        {
            statusText.text = "STATUS: COLONY STARVING";
            statusText.color = Color.red;
        }
        else
        {
            statusText.text = "STATUS: HEALTHY";
            statusText.color = Color.green;
        }
    }
}