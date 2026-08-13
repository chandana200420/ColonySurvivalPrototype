Colony Survival Prototype

A mobile-first colony survival simulation prototype built with Unity. The simulation models a small colony that consumes food and water over time, displays the colony state through a simple mobile-oriented UI, and stops when either resource reaches zero.

Overview

The prototype demonstrates a separation between:

Configuration — population and consumption values loaded from JSON.
Core simulation — pure C# logic for time, resource consumption, survival calculations, and starvation.
Presentation — Unity MonoBehaviour controllers that connect the simulation to the UI.
Testing — EditMode tests covering resource consumption, starvation, and JSON loading.

The simulation uses the assignment-defined clock:

1 real second = 1 game day

Features
Population configuration through JSON
Food and water resource tracking
Per-villager daily consumption
Automatic day progression
Food days remaining calculation
Water days remaining calculation
Starvation detection
Automatic simulation stop when the colony is starving
Mobile-oriented portrait UI with color-coded resource warnings
Healthy/starving status indicator
EditMode unit tests
Project Structure
text
Assets/
├── Scenes/
│   └── Main.unity
│
├── Scripts/
│   ├── Config/
│   │   ├── ConsumptionConfig.cs
│   │   ├── JsonLoader.cs
│   │   └── PopulationConfig.cs
│   │
│   ├── Core/
│   │   ├── ColonySimulation.cs
│   │   └── ColonyState.cs
│   │
│   ├── Presentation/
│   │   ├── GameController.cs
│   │   └── UIController.cs
│   │
│   └── Game.asmdef
│
├── StreamingAssets/
│   ├── Population.json
│   └── consumption.json
│
└── Tests/
    └── EditMode/
        ├── ColonySimulationTests.cs
        └── ColonySurvival.Tests.asmdef
Architecture
Configuration

PopulationConfig contains:

Villager count
Starting food
Starting water

ConsumptionConfig contains:

Food consumed per villager per day
Water consumed per villager per day

JsonLoader loads these values from StreamingAssets using Unity's JsonUtility.

Core Simulation

ColonySimulation is a plain C# class and does not depend on Unity APIs.

It is responsible for:

Advancing simulation time
Advancing game days
Consuming resources
Calculating remaining food days
Calculating remaining water days
Detecting starvation

ColonyState stores the current:

Villagers
Food
Water
Day
Presentation

GameController:

Loads configuration.
Creates the ColonySimulation.
Advances the simulation using Time.deltaTime.
Updates the UI.
Stops the controller when starvation occurs.

UIController displays:

Day
Food (color-coded green/amber/red based on days remaining)
Food days remaining
Water (color-coded green/amber/red based on days remaining)
Water days remaining
Colony status (HEALTHY / STARVING)
Default Simulation Values

The included configuration uses:

json
{
    "villagers": 10,
    "food": 100,
    "water": 150
}

and:

json
{
    "foodPerVillager": 1,
    "waterPerVillager": 2
}

Therefore the colony consumes per day:

text
Food:  10 units/day
Water: 20 units/day

With the default values, water reaches zero on Day 8, so the colony becomes starving at that point.

Example Day 8 state:

text
DAY 8

FOOD
20.0
Food Days Left: 2.0

WATER
0.0
Water Days Left: 0.0

STATUS: COLONY STARVING
Testing

The EditMode test suite includes coverage for:

Three-day resource consumption
Verifies Day 3
Verifies Food = 70
Verifies Water = 90
Starvation when resources reach zero
Verifies Food = 0
Verifies Water = 0
Verifies IsStarving() returns true
JSON population loading
Verifies the population configuration is loaded correctly.

Run the tests from:

Window → General → Test Runner → EditMode

The project was verified with 3 passing tests during development.

Running the Prototype
Open the Unity project.
Open Assets/Scenes/Main.unity.
Ensure the scene contains the GameController and the Canvas/UI setup.
Press Play.
The simulation advances automatically.
Observe the resource values and remaining days.
The simulation stops when food or water reaches zero.
UI Layout
text
Canvas
├── BackGround
├── DayText
├── FoodPanel
│   ├── FoodText
│   └── FoodDaysText
├── WaterPanel
│   ├── WaterText
│   └── WaterDaysText
└── StatusText

FoodPanel and WaterPanel group each resource's value and days-remaining label together with their own colored background, visually separating food and water at a glance. Food and water values are also color-coded (green / amber / red) based on days remaining, giving an additional warning before the colony starves.

Target Platform

The assignment targets:

Android
iOS

The prototype is designed around a mobile/portrait UI. The included work is intended to be tested in the Unity Editor/Device Simulator; an actual Android or iOS build is not required unless requested by the assignment.

Technical Notes
Simulation logic is kept separate from Unity presentation code.
Configuration values are externalized into JSON rather than hardcoded into the simulation.
ColonySimulation uses constructor validation for invalid configuration and time values.
Resources are clamped to zero and cannot become negative.
Once starvation occurs, further time advancement is ignored.
AI Tools Used

I used Claude (Anthropic) throughout this trial as a coding assistant. Specifically, I used it to help debug a runtime issue where the UI was displaying blank values — Claude helped trace this back to a case-sensitivity mismatch between the JSON filename (Population.json) and the string used in JsonLoader (population.json), which fails on case-sensitive filesystems like Android's even though it worked silently in the Unity Editor. I also used Claude to review my project structure for leftover/unused files (a stray template folder, an empty JSON file, and a duplicate test assembly definition) and to get feedback on improving the UI — including adding color-coded warnings and grouping the food/water displays into their own panels. I wrote and understood the core simulation logic, architecture, and JSON config classes myself; Claude was used as a second pair of eyes for debugging, cleanup, and polish rather than to generate the core game logic from scratch.

Decisions & Trade-offs
Resources are clamped at zero rather than allowed to go negative, since the "days remaining" calculation assumes non-negative reserves and negative values wouldn't make sense to display.
Once the colony starts starving, the simulation stops advancing time rather than continuing to tick — this keeps the final state stable and readable rather than flickering between states.
Food and water values are displayed as whole numbers in the UI for readability, while "days remaining" keeps one decimal place since that's the more meaningful countdown for the player.
Grouped Food and Water into separate panels with their own background color, per the brief's instruction not to spend time on visuals — this uses only placeholder shapes and default Unity text, no custom art.
Unity Version
text
Unity: 6.3 LTS (6000.3.0f1)
Demo Video

https://drive.google.com/file/d/1Pq39c-NjMqv8Qwt1JirzAohpB2nUdLwB/view?usp=drivesdk

Repository

The project is maintained in Git with the main development branch:

text
main

Repository: https://github.com/chandana200420/ColonySurvivalPrototype
