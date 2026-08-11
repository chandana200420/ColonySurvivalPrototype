using System;

public class ColonySimulation
{
    private readonly float foodRate;
    private readonly float waterRate;
    private readonly float gameDayDurationSeconds;

    private float elapsedSeconds;

    public ColonyState State { get; }

    public ColonySimulation(
        PopulationConfig population,
        ConsumptionConfig consumption,
        float gameDayDurationSeconds)
    {
        if (population == null)
            throw new ArgumentNullException(nameof(population));

        if (consumption == null)
            throw new ArgumentNullException(nameof(consumption));

        if (gameDayDurationSeconds <= 0f)
            throw new ArgumentOutOfRangeException(
                nameof(gameDayDurationSeconds));

        State = new ColonyState
        {
            Villagers = population.villagers,
            Food = population.food,
            Water = population.water,
            Day = 0
        };

        foodRate = consumption.foodPerVillager;
        waterRate = consumption.waterPerVillager;
        this.gameDayDurationSeconds = gameDayDurationSeconds;
    }

    public void AdvanceTime(float deltaTimeSeconds)
    {
        if (deltaTimeSeconds < 0f)
            throw new ArgumentOutOfRangeException(
                nameof(deltaTimeSeconds));

        if (IsStarving())
            return;

        elapsedSeconds += deltaTimeSeconds;

        while (elapsedSeconds >= gameDayDurationSeconds &&
               !IsStarving())
        {
            elapsedSeconds -= gameDayDurationSeconds;
            AdvanceDay();
        }
    }

    public void AdvanceDay()
    {
        if (IsStarving())
            return;

        State.Day++;

        State.Food = Math.Max(
            0f,
            State.Food - State.Villagers * foodRate);

        State.Water = Math.Max(
            0f,
            State.Water - State.Villagers * waterRate);
    }

    public float FoodDaysRemaining()
    {
        float dailyConsumption = State.Villagers * foodRate;

        return dailyConsumption <= 0f
            ? float.PositiveInfinity
            : State.Food / dailyConsumption;
    }

    public float WaterDaysRemaining()
    {
        float dailyConsumption = State.Villagers * waterRate;

        return dailyConsumption <= 0f
            ? float.PositiveInfinity
            : State.Water / dailyConsumption;
    }

    public bool IsStarving()
    {
        return State.Food <= 0f || State.Water <= 0f;
    }
}