using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    GameManager gameManager;

    public float minWait = 3;
    public float maxWait = 6;

    private bool isSpawning;

    public float spawnAmount = 10;
    public float spawnIncrease = 10;
    public float spawnRate;
    public float topSpawnerXRange;
    public float topSpawnerZPosition;
    //NOT USED
    public float rightSpawnerZRange;
    public float rightSpawnerXPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        isSpawning = false;

        //Enemy = GameObject.FindGameObjectWithTag("Enemy");
        //InvokeRepeating("SpawnOne", 1.0f, Random.Range(3, 10));
    }

    // Update is called once per frame
    void Update()
    {
        //Increases the amount of enemies to spawn when the value in spawnAmount is reached and all enemies are killed
        if (gameManager.enemiesSpawned >= spawnAmount && GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            gameManager.TenSpawned();
            spawnAmount += spawnIncrease;
            Debug.Log("Spawn amount increased");
            //Shortens the amount of time between each enemy spawn
            if (minWait >= 0.5f)
            {
                minWait -= 0.5f;
                maxWait -= 0.5f;
            }
        }
        //Spawns enemies at random intervals within a given range
        if (!isSpawning && gameManager.enemiesSpawned <= spawnAmount)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnOne", timer);
            isSpawning = true;
        }
    }

    private void SpawnOne()
    {
        //Top spawn
        Instantiate(Enemy, new Vector3(Random.Range(-topSpawnerXRange, topSpawnerXRange), 1, topSpawnerZPosition), Enemy.transform.rotation);
        gameManager.enemiesSpawned++;
        isSpawning = false;
    }
}
