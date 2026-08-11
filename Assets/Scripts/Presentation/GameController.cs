using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController uiController;

    private ColonySimulation simulation;

    // Assignment-defined clock:
    // 1 real second = 1 game day.
    private const float GameDayDurationSeconds = 1f;

    private void Start()
    {
        PopulationConfig population =
            JsonLoader.Load<PopulationConfig>("Population.json");

        ConsumptionConfig consumption =
            JsonLoader.Load<ConsumptionConfig>("consumption.json");

        if (population == null || consumption == null)
        {
            enabled = false;
            return;
        }

        simulation = new ColonySimulation(
            population,
            consumption,
            GameDayDurationSeconds);

        uiController.UpdateUI(simulation);
    }

    private void Update()
    {
        if (simulation == null || simulation.IsStarving())
            return;

        simulation.AdvanceTime(Time.deltaTime);

        uiController.UpdateUI(simulation);

        if (simulation.IsStarving())
            enabled = false;
    }
}