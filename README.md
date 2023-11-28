# GameFeelBasics
This project contains some basic features to make the moment to moment gameplay of you Unity project more enjoyable, as well as providing a movement controller built with functionality to improve accessability and enjoyment. There is a focus on customizability and ease of use, allowing you to jumpstart your new projects with little setup required.


## Content
- First person rigidbody controller
- Screen shake
- Hit stop
- Rising pitch


## First Person Rigidbody Controller
The included first person rigidbody controller features customizability over all aspects of movement; 
- Speed
- Jump
- Air control
- Mouse controlled camera

while also implementing these gamefeel tricks;
- Coyote time
- Variable jump height
- Jump input cache

### Usage
Import the unity package and drop the '**Player + Camera**' prefab into the scene. Make sure your ground is set to the same layer as the variable '**Ground Check Layer**' in the '**Player Movement**' script component on the game object '**Player**'.
#
## Effects
There are three included effects which can be easily implemented and customized;
### Screen shake
The '**ScreenShaker**' script can be used to shake the screen. Use the method '**AddShake**' to increase the current shake intensity, this is decreased over time along a animation curve. This is best used to give players quick feedback for short actions like e.g. being hit, hitting an enemy or making a rough landing. The '**intensity**' parameter is also the time it will take before it has reduced to zero shaking. Use the '**ShakeForTime**' methods to override all shaking and shake the screen for a specified amount of time, this could be used to highlight events, e.g. boss spawning or an earthquake. 

### Hit stop
The '**HitStopper**' script can be used to temporarily freeze the screen. This is often used to give more impact to being hit or dealing damage. Use the method '**HitStop**' to pause the screen for the time specified in the inspector.

### Rising pitch
The '**SoundPitcher**' can be used to play a sound in an increasingly higher pitch if they are played within a specified interval. This can be used to inform players they're meeting a combo requirement, or just because it sounds nice. Use the method "**PlayPitchedSound**" to play the Audiosource specified in the inspector, if it is then played again within the '**maxIntervalInSeconds**', it will play in a higher pitch.

### Usage
Import the unity package and place any of these scripts on any object. Assign the necessary variables (currently only audiosource in SoundPitcher) and customize the values to your games' needs. Then call the methods mentioned above with your desired parameters. The values in the '**Player + Camera**' prefab are good starting point for most games.
