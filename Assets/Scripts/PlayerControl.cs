using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;

    GameManager gameManager;

    //Movement axis
    float Horizontal;
    float Vertical;

    public GameObject projectile;
    //Stores the force the player is being pushed with
    public float forwardForce = 10;
    //Stores the players rotate speed in degrees 
    public float rotateSpeed = 5;

    //Projectile stuff---------------
    float spawnDistance = 3;
    Vector3 spawnPos;
    //-------------------------------

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Forward and back turn axis/input
        this.Vertical = Input.GetAxis("Vertical");
        //Left and right turn axis
        this.Horizontal = Input.GetAxis("Horizontal");
        //Input.GetButtonDown("");
        if (Input.GetButtonDown("Fire1"))
        {
            //Set the spawn position for the projectile
            spawnPos = this.transform.position + this.transform.forward * spawnDistance;
            //Spawn the projectile
            Instantiate(projectile, spawnPos, this.transform.rotation);
        }

        //Forward movement
        rb.AddForce(this.transform.forward * forwardForce * this.Vertical);

        //Rotate player
        this.transform.Rotate(new Vector3(0, rotateSpeed * this.Horizontal * Time.deltaTime * 10));

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameManager.playerHealth--;
            
        }
    }
}