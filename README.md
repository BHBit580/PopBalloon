This game popup balloon is a very simple game demonstrating integration of python and unity and playfab.
`# Documentation

### SETUP

To run the Python script, include the Iron Python package from Python for Unity. 
Next, navigate to *Edit -> Project Settings -> Player*. Change .NETstandard2.1 to .NET FRAMEWORK or .NET4X framework. 
For more information, refer to this guide: https://mikalikes.men/use-python-with-unity-3d-the-definitive-guide/

For Playfab integration, set up an account first. Then, download the PlayfabEditor extension. 
This will install the SDK automatically after a successful login.


Game Code Logic ------ 

### SCENE MENU LOGIC

When the player presses the play button, it triggers the startGameEvent. The login manager then initiates the login process. 
A loading display is shown during this time. Once the login is complete and the player's name data has been transferred to the server, the game scene opens.

### SCENE GAME LOGIC

Initially, the balloon spawner script executes and initializes the Python code in the start function.
 This Python code, functioning as our difficulty manager, determines the number of balloons to spawn. 
 Every three seconds, this code increases the difficulty level. The values it generates are random but increase over time.

The "data" from the Python script is used by our Balloon Spawner logic to spawn balloons and adjust their speed. 
This class adjusts the game's difficulty over time. The number of balloons that spawn and their speed increase, making the game progressively more difficult.
The getBalloonSpawn method retrieves the number of balloons to spawn, and the balloonSpeed method retrieves the speed of the balloons.

The Balloon Spawner role is to spawn the balloons and communicate with the Python code.

Each balloon has a script that triggers a destroy animation when the player taps on it. This script contains a scriptable object known as 'score'. When the balloon is destroyed, the score value increases. This scriptable object is accessed by various other UI elements on the canvas, such as the final score display and score submission.

When the timer reaches 0, a game-over event is fired. After this, the game over UI screen is shown. When the player presses the submit button, the score data is submitted to the server. The leaderboard is then presented to the player. 
This leaderboard collects all the data and instantiates several rows of data. Pressing retry level will restart the level.`
