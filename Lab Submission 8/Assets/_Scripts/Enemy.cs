using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ISaveable
{
    [Header("Enemy Attributes")]
    public float health;
    public float moveSpeed;
    public int moveDirection;


    public void Initialize(float health, float moveSpeed, int direction){
        this.health = health;
        this.moveSpeed = moveSpeed;
        this.moveDirection = direction;
    }

    public void Load(SaveData data)
    {
        throw new System.NotImplementedException();
    }

    public SaveData Save()
    {
        throw new System.NotImplementedException();
    }

    private void Update() {
        transform.Translate(Vector2.right * moveDirection * moveSpeed * Time.deltaTime);

        if(transform.position.x > 12)
        {
            Destroy(gameObject);
        }
    }
}
