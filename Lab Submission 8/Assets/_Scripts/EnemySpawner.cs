using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemy;
    public float spawnCooldown;
    private float cooldown;


    private void Start() {
        SpawnEasyEnemy();
        SpawnMediumEnemy();
    }

    private void Update() {
        if(cooldown <= 0)
        {
            SpawnRandomEnemyType();

            cooldown = spawnCooldown;
        }

        cooldown -= Time.deltaTime;
    }

#region Enemy Spawners
    // Spawns an easy enemy.
    private void SpawnEasyEnemy(){
        EnemyBuilder builder = new EnemyBuilder();

        builder.SetHealth(1)
            .SetMoveSpeed(3)
            .SetMoveDirection(1);

        // Get our spawn position and direction.
        Vector2 spawnPos = new Vector2(-10, GetVerticalSpawn());

        builder.Build(enemy, spawnPos);
    }

    // Spawns a medium enemy.
    private void SpawnMediumEnemy(){
        EnemyBuilder builder = new EnemyBuilder();

        builder.SetHealth(1)
            .SetMoveSpeed(5)
            .SetMoveDirection(1);

        // Get our spawn position and direction.
        Vector2 spawnPos = new Vector2(-10, GetVerticalSpawn());

        builder.Build(enemy, spawnPos);
    }

    // Spawns a hard enemy.
    private void SpawnHardEnemy(){
        EnemyBuilder builder = new EnemyBuilder();

        builder.SetHealth(1)
            .SetMoveSpeed(8)
            .SetMoveDirection(1);

        // Get our spawn position and direction.
        Vector2 spawnPos = new Vector2(-10, GetVerticalSpawn());

        builder.Build(enemy, spawnPos);
    }

#endregion

    // Spawn a random enemy type.
    private void SpawnRandomEnemyType(){
        // Switch statement here.
        int index = Random.Range(0, 3);

        switch(index)
        {
            case 0:
                SpawnEasyEnemy();
                break;
            case 1:
                SpawnMediumEnemy();
                break;
            case 2:
                SpawnHardEnemy();
                break;
            default:
                SpawnMediumEnemy();
                break;
        }
    }

    // Get a random vertical spawn position!
    private int GetVerticalSpawn(){
        return Random.Range(0, 5);
    }
}
