# Lab Submission 8
I used object pooling for the projectiles. They are returned to the pool queue using the Observer design pattern.
When the projectiles are initially created, they are assigned to keep track of the method that returns them to the pool.
When they hit something or run out of lifetime, they call that function and return to the queue.

The Observer design pattern is also used for keeping score. In similar fashion, when the projectiles are initlally created they
are assigned to listen to the score increase function and call it when they hit an enemy.

The builder pattern is used when spawning enemies. The spawner picks a random enemy type to spawn and creates it with an EnemyBuilder.
This EnemyBuilder is used to 'build' the enemy through an enemy prefab.

I used Json Serialization to save and load the positions of the enemies and the player. The ISaveable interface is used to locate
anything that needs saving.

I used the binary formatter to save and load the current score.

[A, D] -> Strafe left and right
[Space] -> Shoot
[S] -> Save
[L] -> Load

This is a short video showcasing the gameplay: https://www.youtube.com/watch?v=p0QagQiQlL4
This is a short video showcasing the save system: https://youtu.be/ojChuKYMZ_M 

Thank you!

Carson Moon
 
