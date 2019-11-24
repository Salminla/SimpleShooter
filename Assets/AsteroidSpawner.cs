using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject Asteroid;

    GameManager gameManager;

    public float minWait = 10;
    public float maxWait = 20;

    private bool isSpawning;

    public float spawnAmount = 10;
    public float spawnIncrease = 10;
    public float spawnRate;
    public float topSpawnerXRange;
    public float topSpawnerZPosition;

    public float pushDirectionMultiplier = 1000;
    int asteroidsSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        isSpawning = false;

        SpawnOne();
    }

    // Update is called once per frame
    void Update()
    {
        //Spawns enemies at random intervals within a given range
        if (!isSpawning)
        {
            float timer = Random.Range(minWait, maxWait);
            Invoke("SpawnOne", timer);
            isSpawning = true;
        }
    }

    private void SpawnOne()
    {
        //Top spawn
        GameObject newAsteroid = Instantiate(Asteroid, new Vector3(Random.Range(-topSpawnerXRange, topSpawnerXRange), 1, topSpawnerZPosition), Asteroid.transform.rotation);
        newAsteroid.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(7, -7) * 1000, 0, Random.Range(-5, -10) * pushDirectionMultiplier));
        isSpawning = false;
        asteroidsSpawned++;
    }
}
