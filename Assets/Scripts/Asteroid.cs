using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    GameManager gameManager;

    Rigidbody rb;

    public GameObject asteroidObject;
    public GameObject explosionPrefab;
    public GameObject RandomDrop;

    public float initialPushX = -150;
    public float initialPushZ = 0;
    bool asteroidSpawned = false;

    public float asteroidHealth = 4;
    public float currentHealth;
    float sizeReduction = 1.5f;
    float massReduction = 15f;
    int spawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        rb = gameObject.GetComponent<Rigidbody>();

        rb.AddTorque(new Vector3(5000,0,5000));

        currentHealth = asteroidHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //When the asteroid gets splitted
        if (currentHealth < 1 && !asteroidSpawned)
        {
            if (Random.Range(0,6) == 0)
            {
                Instantiate(RandomDrop, transform.position, Quaternion.identity);
            }
            gameManager.score++;
            Explode();
            asteroidSpawned = true;
            SpawnAsteroids();
            Destroy(gameObject);
        }
        //When this object has splitted into small enough size, destroy it.
        if (this.gameObject.transform.localScale.x < 1)
        {
            Debug.Log("Asteroid destroyed");
            
            Destroy(gameObject);
        }

        //Boundaries on the x-axis
        if (transform.position.x < -gameManager.XLimit * 2 || transform.position.x > gameManager.XLimit * 2)
        {
            Destroy(gameObject);
        }

        //Boundaries on the z-axis
        if (transform.position.z > gameManager.upperZLimit * 2 || transform.position.z < gameManager.lowerZLimit * 2)
        {
            Destroy(gameObject);
        }
    }
    //Spawns new asteroids in the location of the old one at reduced size and mass.
    void SpawnAsteroids()
    {
        //Asteroid 1
        //GameObject newAsteroid = Instantiate(asteroidObject, transform.position, Quaternion.identity);
        GameObject newAsteroid = Instantiate(asteroidObject, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), Quaternion.Euler(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360)));
        newAsteroid.transform.localScale = new Vector3(newAsteroid.transform.localScale.x - sizeReduction, newAsteroid.transform.localScale.y - sizeReduction, newAsteroid.transform.localScale.z - sizeReduction);
        newAsteroid.GetComponent<Rigidbody>().mass = newAsteroid.GetComponent<Rigidbody>().mass - massReduction;
        //Asteroid 2
        GameObject newAsteroid2 = Instantiate(asteroidObject, transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
        newAsteroid2.transform.localScale = new Vector3(newAsteroid2.transform.localScale.x - sizeReduction, newAsteroid2.transform.localScale.y - sizeReduction, newAsteroid2.transform.localScale.z - sizeReduction);
        newAsteroid2.GetComponent<Rigidbody>().mass = newAsteroid2.GetComponent<Rigidbody>().mass - massReduction;
    }
    //Explosion effect
    void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(explosion.transform.localScale.x - sizeReduction, explosion.transform.localScale.y - sizeReduction, explosion.transform.localScale.z - sizeReduction);
        explosion.GetComponent<ParticleSystem>().Play();
        //explosion.GetComponent<AudioSource>().Play();
    }
}
