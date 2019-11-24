using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameManager gameManager;

    public Collider playerCollider;
    Collider ownCollider;

    public float moveSpeed = 2;
    public float destroyTime = 3;

    public ParticleSystem trailEffect;
    public GameObject impactPrefab;

    public AudioClip impactSound;
    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //Sets the projectiles own collider into the variable
        ownCollider = gameObject.GetComponent<SphereCollider>();
        //Find the player objects collider
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();

        //trailEffect = GameObject.FindGameObjectWithTag("Trail").GetComponent<ParticleSystem>();

        Destroy(this.gameObject, destroyTime);
        //Ignores the collisions between the player and the projectile
        Physics.IgnoreCollision(playerCollider, ownCollider);

        //trailEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        //trailEffect.transform.position = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Explode();
            gameManager.score++;
            other.gameObject.GetComponent<EnemyControl>().enemyHealth--;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Explode();
            other.gameObject.GetComponent<Asteroid>().currentHealth--;
            Destroy(gameObject);
        }

    }
    void Explode()
    {
        GameObject explosion = Instantiate(impactPrefab, transform.position, Quaternion.identity);
        explosion.GetComponent<ParticleSystem>().Play();
        explosion.GetComponent<AudioSource>().Play();
    }
}
