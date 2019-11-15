using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    GameManager gameManager;

    public float spawnRate;
    public float spawnerXRange;
    public float spawnerZPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        //Enemy = GameObject.FindGameObjectWithTag("Enemy");
        InvokeRepeating("SpawnOne", 1.0f, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnOne()
    {
        if (gameManager.enemiesSpawned < 10)
        {
            spawnRate = Random.Range(3, 10);
            Instantiate(Enemy, new Vector3(Random.Range(-spawnerXRange, spawnerXRange), 1, spawnerZPosition), Enemy.transform.rotation);
            gameManager.enemiesSpawned++;
        } 
    }
}
