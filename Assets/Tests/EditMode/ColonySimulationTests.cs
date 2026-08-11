using NUnit.Framework;

public class ColonySimulationTests
{
    private const float GameDayDuration = 1f;

    [Test]
    public void AdvanceTime_AfterThreeDays_ConsumesCorrectResources()
    {
        // Arrange
        var population = new PopulationConfig
        {
            villagers = 10,
            food = 100f,
            water = 150f
        };

        var consumption = new ConsumptionConfig
        {
            foodPerVillager = 1f,
            waterPerVillager = 2f
        };

        var simulation = new ColonySimulation(
            population,
            consumption,
            GameDayDuration);

        // Act
        simulation.AdvanceTime(3f);

        // Assert
        Assert.AreEqual(3, simulation.State.Day);
        Assert.AreEqual(70f, simulation.State.Food);
        Assert.AreEqual(90f, simulation.State.Water);
    }
    [Test]
    public void AdvanceDay_WhenResourcesReachZero_ColonyIsStarving()
    {
        // Arrange
        var population = new PopulationConfig
        {
            villagers = 10,
            food = 10f,
            water = 20f
        };

        var consumption = new ConsumptionConfig
        {
            foodPerVillager = 1f,
            waterPerVillager = 2f
        };

        var simulation = new ColonySimulation(
            population,
            consumption,
            GameDayDuration);

        // Act
        simulation.AdvanceDay();

        // Assert
        Assert.AreEqual(0f, simulation.State.Food);
        Assert.AreEqual(0f, simulation.State.Water);
        Assert.IsTrue(simulation.IsStarving());
    }
    [Test]
    public void JsonLoader_LoadsPopulationConfig()
    {
        // Act
        var population = JsonLoader.Load<PopulationConfig>("population.json");

        // Assert
        Assert.IsNotNull(population);
        Assert.AreEqual(10, population.villagers);
        Assert.AreEqual(100f, population.food);
        Assert.AreEqual(150f, population.water);
    }
}