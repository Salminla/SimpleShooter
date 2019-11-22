using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    GameManager gameManager;

    Rigidbody rb;
    //The object the enemy follows
    GameObject followedObject;
    //Enemys projectile
    public GameObject projectile;

    public GameObject destroyedPrefab;

    //Healthbar
    Canvas health;
    RectTransform rectTransform;
    public Image healthBar;

    public float enemySpeed = 5;

    public float enemyHealth = 3;

    //Projectile stuff---------------
    float spawnDistance = 4;
    Vector3 spawnPos;
    bool isSpawning;
    public float shootInterval = 2;
    //-------------------------------

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //Find the player object and set it to followedObject
        followedObject = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();

        
        //health = GetComponentInChildren<Canvas>();

        rectTransform = GetComponentInChildren<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //rectTransform.Rotate(new Vector3(1, 0, 0));
        rectTransform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);

        if (gameManager.playerAlive)
        {
            //Rotate towards the set object
            transform.LookAt(followedObject.transform);
        }
        
        //Add force to this objects RigidBody
        rb.AddForce(this.transform.forward * enemySpeed);

        //Healthbar update
        healthBar.fillAmount = enemyHealth / 3;

        //Prjectile spawning
        if (!isSpawning)
        {
            Invoke("SpawnProjectile", shootInterval);
            isSpawning = true;
        }

        if (enemyHealth < 1)
        {
            Explode();
            gameManager.score+=2;
            Destroy(gameObject);
        }
    }

    void SpawnProjectile()
    {
        //Sets the projectiles spawn position
        spawnPos = this.transform.position + this.transform.forward * spawnDistance;
        //Spawn the projectile
        Instantiate(projectile, spawnPos, this.transform.rotation);
        isSpawning = false;
    }
    void Explode()
    {
        GameObject explosion = Instantiate(destroyedPrefab, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        explosion.GetComponent<AudioSource>().Play();
    }
}
