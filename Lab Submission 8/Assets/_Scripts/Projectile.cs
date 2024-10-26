using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float moveSpeed;
    public float totalLifetime;
    public float lifetime;

    // Event for destroying this projectile.
    public event Action<GameObject> OnProjectileDeath;

    // Event for if we hit an enemy.
    public event Action OnEnemyHit;


    private void Update() {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Subtract from our lifetime.
        lifetime -= Time.deltaTime;

        // If we dip below zero, destroy.
        if(lifetime < 0)
        {
            DestroyProjectile();
        }
    }

    // When we die, call our onprojectiledeath action.
    private void DestroyProjectile(){
        // Invoke our action.
        OnProjectileDeath?.Invoke(gameObject);

        // Reset lifetime.
        lifetime = totalLifetime;
    }

    // If we hit an enemy...
    private void OnTriggerEnter2D(Collider2D other) {
         print("HAHAHAHAH");

        if(other.gameObject.layer == 6)     // 6 -> Enemy
        {
            Destroy(other.gameObject);
           
            OnEnemyHit?.Invoke();

            DestroyProjectile();
        }
    }
}
