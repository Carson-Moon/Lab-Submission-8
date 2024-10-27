using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ISaveable
{
    // Runtime
    PlayerInput pInput;

    [Header("Movement Settings")]
    public float moveSpeed;

    [Header("Shoot Settings")]
    public int poolSize;
    public Transform poolHolder;
    public GameObject projectile;
    public Transform shotPosition;
    public float cooldown = 0;
    public float cooldownLength;
    private Queue<GameObject> projectilePool;

    [Header("Score Settings")]
    public int totalScore;
    public TextMeshProUGUI scoreUI;


    private void Awake() {
        pInput = GetComponent<PlayerInput>();

        SetupPool();

        cooldown = 0;
    }

    private void Update() {
        // Movement
        // Get horizontal direction.
        Vector2 horizontal = new Vector2(pInput.GetMoveDirection().x, 0);

        transform.Translate(horizontal * moveSpeed * Time.deltaTime);

        // Update cooldown.
        cooldown -= Time.deltaTime;
    }

#region Shooting
    // Setup our projectile pool.
    private void SetupPool(){
        // Create a new queue to hold our pool.
        projectilePool = new Queue<GameObject>();

        // Create and add projectiles to our pool for our desired size.
        for(int i=0; i<poolSize; i++)
        {
            // Create and set object to false.
            GameObject thisProjectile = Instantiate(projectile);
            thisProjectile.SetActive(false);

            // Add this object to the holder and name accordingly.
            thisProjectile.transform.SetParent(poolHolder);
            thisProjectile.name = "Projectile " + i.ToString();

            // Enqueue this object.
            projectilePool.Enqueue(thisProjectile);

            // Subscribe this objects on death action to our return to pool method.
            Projectile projectileScript = thisProjectile.GetComponent<Projectile>();
            projectileScript.OnProjectileDeath += ReturnToPool;
            projectileScript.OnEnemyHit += AddToScore;
        }
    }

    // Shoot a projectile!
    public void Shoot(){
        // Only shoot if we are below our cooldown.
        if(cooldown > 0)
            return;

        // Retrieve the top object from the pool.
        GameObject thisProjectile = projectilePool.Dequeue();

        // Move it to the shot position.
        thisProjectile.transform.position = shotPosition.position;

        // Set cooldown.
        thisProjectile.GetComponent<Projectile>().lifetime = 1;

        // Set the object to true.
        thisProjectile.SetActive(true);

        // Reset cooldown.
        cooldown = cooldownLength;
    }

    // Return this object to the pool.
    public void ReturnToPool(GameObject thisProjectile){
        thisProjectile.SetActive(false);

        projectilePool.Enqueue(thisProjectile);
    }

#endregion

#region Score
    // Add one to our score.
    public void AddToScore(){
        totalScore++;

        scoreUI.text = totalScore.ToString();
    }

    // Set our score.
    public void SetScore(int value){
        totalScore = value;

        scoreUI.text = totalScore.ToString();
    }

    public SaveData Save()
    {
        throw new System.NotImplementedException();
    }

    public void Load(SaveData data)
    {
        throw new System.NotImplementedException();
    }
#endregion
}
