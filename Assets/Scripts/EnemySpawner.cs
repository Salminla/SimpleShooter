using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    GameManager gameManager;

    public float spawnRate;
    public float topSpawnerXRange;
    public float topSpawnerZPosition;

    public float rightSpawnerZRange;
    public float rightSpawnerXPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
        //Enemy = GameObject.FindGameObjectWithTag("Enemy");
        InvokeRepeating("SpawnOne", 1.0f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnOne()
    {
        StartCoroutine(WaitFor());
        if (gameManager.enemiesSpawned < 10)
        {
            //Top spawn
            Instantiate(Enemy, new Vector3(Random.Range(-topSpawnerXRange, topSpawnerXRange), 1, topSpawnerZPosition), Enemy.transform.rotation);
            gameManager.enemiesSpawned++;
        }
        if (gameManager.enemiesSpawned < 10)
        {
            //Side spawn
            Instantiate(Enemy, new Vector3(rightSpawnerXPosition, 1, Random.Range(-rightSpawnerZRange, rightSpawnerZRange)), Enemy.transform.rotation);
            gameManager.enemiesSpawned++;
        }
        spawnRate = Random.Range(3, 10);
    }

    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(spawnRate);
    }
}
