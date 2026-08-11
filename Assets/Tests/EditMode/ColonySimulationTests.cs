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
}