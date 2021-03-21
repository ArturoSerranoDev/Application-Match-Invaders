# Project Review

## Arturo Serrano

---

<!-- Your review goes here -->
<!-- Explain why you did the things that way or any snippet that is word mentioning -->
<!-- If you had any issue and how you resolved them -->

First of all, this was a pretty interesting project to complete, I had a lot of fun playing it after completion.

My estimations were pretty optimistic **(8:20 hours)**, but I completed it in over **12 hours** (without taking into account breaks). It wasn't a difficulty issue but more related to the time needed to implement all the requested features while doing so with SOLID principles in mind.

This was the time it took me for each commit until I fell asleep

![image](https://user-images.githubusercontent.com/79790514/111916588-f28efe00-8a7b-11eb-9bb8-9b5494cb1463.png)

My goal was to try to do this project with a TDD approach since the requirements were pretty clear and strict, but since I saw that I had to implemente a several functionalities I left the unit tests for the second half of the project.

Things I'm proud of:

  - **PoolManager.cs:** Manages the creation of different Pools of objects dinamically. It also creates the parents for each of the different pool of objects requested.

  - **UnitySingletonPersistent<T>.cs:** Generic abstract class used to implement a Monobehaviour as Singleton. 

  - **EnemyShootLogic.cs:** Separated the logic of enemy shooting to its own Controller. To implement the shooting of enemies I used a ```Dictionary<int, List<Enemy>>``` enemiesPerColumn that separates each enemy into its own column and the iterates over it to get the closest enemy to player, and then make it shoot.
  
  - Kept **modularity** and **extension** from start. Separation of concerns.

  - Used several **patterns** over the project when needed that were pretty useful in the long run(Object Pooling, Observer Pattern with delegates, "MVC", Singleton). I did not used a StateMachine for handling Win/Lose scenarios and instead went along with subscription to static events of LevelManager to handle the state.

Also pretty good:

  - Unit Testing of Correct level Building, Player and Enemy shooting and moving, and bunker logic.

  - Since it was prohibited to use PlayerPrefs to store data, I used **JsonUtility** to store the highscore with a **JSON**.
  
  - **LevelBuilder.cs and neighbour logic**: It took me much less time that I though of to implement the enemy creation and assigning its neighbours (Estimated 1 hour but took 20 min), but since I have worked a lot already with two-dimensional arrays it was pretty simple. What really took me time from this requirement was the Object Pool.

  - Implemented Audio and base animation at start.

Things to be improved:

  - My biggest regret is the lack of time I had to implement some creative twist to this project. I had several ideas (switching player for enemies setting the enemies as: US, task and bug Tickets that the dev has to destroy...) but I already took over 12 hours to finish it, so I though it was not fair to keep implementing elements into this project.

  - I wanted to decouple User's input from its view on MainCharacter.cs but I did not have time when I noticed.

  - More Unit Testing for implementation LevelManager.

  - Since I didn't implement a StateMachine and instead relied on subscribing to LevelManager's events, I had some UX issues after restarting/going back to the menu.

## Estimations

TOTAL: ESTIMATED - 8 hours and 20 minutes | REAL - ~12 hours

The estimations are done taking into account the time it took for each commit.
- Project Setup(Folder Structure, git setup) - Estimated 20 min - Done in 30 min
- Project Structuring(Created needed Managers, created config Scriptables of each element...) - Estimated 40 min - Done in 36 min
- Enemy Creation(Pool Creation, LevelBuilder class) - Estimated 60 min - Done in 60 min
- MVP Player,Enemy,Bunker Prefabs - Estimated 60 min - Done in 50 min
- Enemy Movement - Estimated 40 min - Done in 70 min
- Enemy Shooting - Estimated 50 min - Done in 60 min
- Win/Lose Condition - Estimated 20 min - Done in 60 min
- Score Calculation - Estimated 30 min - Done in 30 min
- Menu UI and Logic - Estimated 40 min - Done in 80 min
- HighScore/SaveLoad Functionality - Estimated 40 min - Done in 40 min
- Unit Testing - Estimated 60 min - Done in 80 min
- Tweaking and Refactoring - Estimated 20 min - Done in 60 min
- Polish - Estimated 20 min - Done in 80 min

## Review

### Project Setup ~30min
- Created Folder structure
- Added simple unit test to project with two small examples

### Project Structuring ~36min
- Created ScriptableObjects to handle configs of elements in Game.
- Created base LevelBuilder, EnemyShootController and EnemyMoveController to separate concerns.

### Enemy Creation ~60min
- Created Singleton abstract parent class
- Created Pool Manager
- Level Creation. Enemies add their neighbours
- LevelBuilder was created faster than I though

### MVP Player,Enemy,Bunker Prefabs ~50min
- Created Prefabs of all needed elements
- Implement Enemy, Bunker, and Player functionality (Triggers, Shoot, Move)

### Enemy Movements ~70min
- Implemented EnemyMoveManager
- Lost a lot of time with the move coroutine since I had a stack overflow on the movement coroutine looping within a while (Dangerous!)

### Enemy Shooting ~60min
- Implemented Enemy shooting.

### Score Calculation ~30 min
- Created Score calculation scripts to return score based on Fibonacci and the requested custom formula.
- Updated Player and LevelManager scripts to handle highScores (Still no save functionality)

### Save/Load ~40 min
- Save and Load highscore implemented using JsonUtility
- Had some crashes so I lost some time here

### Implement UI ~80 min
- Created UIManagers to handle UI on both scenes
- I lost a lot of time on this task since I did not used the Canvas Scaler beforehand and I lost some progress on my Layout :(

### Create Win/Lose Scenarios ~60 min

- Created events for Win, Lose, EndGame, and HighScore Scenarios.
- Lost some time in here also because I had to update some Unit tests in the middle (and also manual testing of Winning/Losing Scenarios)

### Unit Testing ~ 80 min

- This task was 30% implementation, 70% bugfixing. 40 minutes or so were spent working on all the Unit test, the rest is time lost tweaking some scripts after adding elements to previous scripts (In a proper SOLID world, this shouldn't happen).
- Since I was using ScriptableObjects, I had some issues testing them without proper initialitation of those config files, so a lot of variables of config files have values already set up.
- I wasn't able to finish all the tests I wanted but I already spent a lot of my time working on them.

### Refactor and Tweaking ~ 60 min

- Checked out all created code and refactor some functionality.
- Tweaked some parameters
- Playtesting

### Polish ~ 80 min

- Added Sounds to game
- Updated UI


I hope you like it! Have a nice day.
