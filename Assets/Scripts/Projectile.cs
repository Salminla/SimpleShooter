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

    public AudioSource audioSource;
    public AudioClip impactSound;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //Sets the projectiles own collider into the variable
        ownCollider = gameObject.GetComponent<SphereCollider>();
        //Find the player objects collider
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        audioSource = gameObject.GetComponent<AudioSource>();

        Destroy(this.gameObject, destroyTime);
        //Ignores the collisions between the player and the projectile
        Physics.IgnoreCollision(playerCollider, ownCollider);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            audioSource.clip = impactSound;
            audioSource.Play();
            gameManager.score++;
            other.gameObject.GetComponent<EnemyControl>().enemyHealth--;
            Debug.Log("Enemy hit!");
            Destroy(gameObject);
        }
        
    }
}
