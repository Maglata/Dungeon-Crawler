# Unity3D Dungeon Crawler
A simple implementation of a Dungeon Crawler game in Unity3D 
## Game Information
The game has a fully functional level + coin management system implemented. 
The player attacks on their own and manages to hit enemies when it detects them nearby.
After an enemy is killed a coin spawns which upon the player stepping on it gets collected and stored even after quitting.
After all enemies are killed the door opens and allows the player to progress to another level.
## Level Creation
Levels are created at the start of the launch of the game. A random amount of enemies are spawned between 2 and 6(which can be adjusted in the editor).
The player is spawned in one exact spot and from then on the game begins as normal.
After finishing the level the same function is called and a new level is created with a different amount of enemies.
## Implementation
- Player Detection - Currently all of the enemies cast a sphere around them with a certain range to allow them to detect the player.
(Initially, it was required to use a sphere ray cast but with that, the enemies only looked in front of them and the player could easily juke them)
- Enemy Movement - All Enemies use NavMesh to traverse with the shortest path possible.
- Attack - Whenever a target is in range an attack animation is played and if at a certain keyframe, the target is still present the damage is dealt to the target.
- Player Movement - The Player is currently moving via a RigidBody-based controller.
- Health Bars - HealthBars are implemented to follow the camera and always face the camera even if it moves.
- Game View - The game currently is implemented to work in a top-down view but you can always edit the camera position and adjust it to your liking.
- Coins - Coins are fully implemented and on enemy death, a coin is instanced. Whenever the coin is picked up it is stored locally hence why the progress is saved after quitting.
### Important information
- Most of the characters + Animations are from mixamo
- Environment Assets - https://assetstore.unity.com/packages/3d/environments/dungeons/ultimate-low-poly-dungeon-143535
- Enemy Assets - https://assetstore.unity.com/packages/3d/characters/creatures/dungeon-skeletons-demo-71087
