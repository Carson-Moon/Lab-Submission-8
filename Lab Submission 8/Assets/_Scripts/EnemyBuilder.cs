using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyBuilder
{
    private float health;
    private float moveSpeed;
    private int moveDirection;


    public EnemyBuilder SetHealth(float health){
        this.health = health;
        return this;
    }

    public EnemyBuilder SetMoveSpeed(float moveSpeed){
        this.moveSpeed = moveSpeed;
        return this;
    }

    public EnemyBuilder SetMoveDirection(int moveDirection){
        this.moveDirection = moveDirection;
        return this;
    }

    public Enemy Build(GameObject enemy, Vector2 spawnPosition){
        GameObject thisEnemy = Object.Instantiate(enemy, spawnPosition, quaternion.identity);

        // Give this enemy the configured values.
        Enemy enemyScript = thisEnemy.GetComponent<Enemy>();
        enemyScript.Initialize(health, moveSpeed, moveDirection);

        return enemyScript;
    }

}
