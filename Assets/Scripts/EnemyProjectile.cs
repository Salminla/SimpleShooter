using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    GameManager gameManager;

    public float moveSpeed = 2;
    public float destroyTime = 3;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        Destroy(this.gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        //If another enemy is hit destroy the projectile
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        //If player is hit do this
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.playerHealth--;
            Debug.Log("Player hit!");
            Destroy(gameObject);
        }

    }
}
