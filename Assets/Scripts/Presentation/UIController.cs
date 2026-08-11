using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text waterText;
    [SerializeField] private TMP_Text foodDaysText;
    [SerializeField] private TMP_Text waterDaysText;
    [SerializeField] private TMP_Text statusText;

    public void UpdateUI(ColonySimulation simulation)
    {
        ColonyState state = simulation.State;

        dayText.text = $"Day: {state.Day}";
        foodText.text = $"Food: {state.Food:F1}";
        waterText.text = $"Water: {state.Water:F1}";

        foodDaysText.text =
            $"Food Days Left: {simulation.FoodDaysRemaining():F1}";

        waterDaysText.text =
            $"Water Days Left: {simulation.WaterDaysRemaining():F1}";

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