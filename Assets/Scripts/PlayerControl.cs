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
    float Vertical1;
    float Strafe;

    public GameObject projectile;
    //Stores the force the player is being pushed with
    public float forwardForce = 10;
    //Stores the players rotate speed in degrees 
    public float rotateSpeed = 5;

    //Projectile stuff---------------
    float spawnDistance = 3;
    Vector3 spawnPos;
    bool isSpawning;
    //-------------------------------

    float test;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody>();

        isSpawning = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Forward and back turn axis/input
        this.Vertical = Input.GetAxis("Vertical");
        //Left and right turn axis
        this.Horizontal = Input.GetAxis("Horizontal");
        //Up down facing axis
        this.Vertical1 = Input.GetAxis("Vertical1");
        //Strafing
        this.Strafe = Input.GetAxis("Strafe");

        if (Input.GetButton("Fire1"))
        {
            //Set the spawn position for the projectile
            if (!isSpawning)
            {
                Invoke("SpawnProjectile", 0.1f);
                isSpawning = true;
            }
           
        }

        //Forward movement
        rb.AddForce(this.transform.forward * forwardForce * this.Vertical);

        //Move player left and right
        rb.AddForce(this.transform.right * forwardForce * this.Strafe);

        //Rotate player
        this.transform.Rotate(new Vector3(0, rotateSpeed * this.Horizontal * Time.deltaTime * 10));

        //Vector3 relativePos = new Vector3((transform.position.x + this.Horizontal), transform.position.y, transform.position.z + this.Vertical1) - transform.position;
        //Quaternion targetRotation = Quaternion.LookRotation(relativePos);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime);

        //Vector3 lookDirection = new Vector3(Horizontal, 0, Vertical1);
        //this.gameObject.transform.rotation = Quaternion.Euler(lookDirection);

        //Vector3 lookVec = new Vector3(this.Horizontal, this.Vertical1, 4096);
        //if (lookVec.x != 0 && lookVec.y != 0)
        //    transform.rotation = Quaternion.LookRotation(lookVec, Vector3.back);

        //Twist();

        //if (gameObject.transform.rotation.eulerAngles.y > 90 && this.gameObject.transform.rotation.eulerAngles.y < 260)
        //{
        //  Move player left and right
        //  rb.AddForce(this.transform.right * -forwardForce * this.Strafe);
        //}
        //  else
        //{
        //  Move player left and right
        //  rb.AddForce(this.transform.right * forwardForce * this.Strafe);
        //}

        
        //Debug.Log(this.gameObject.transform.rotation.eulerAngles.y);
        //Debug.Log(lookVec);

    }
   //void Twist()
   // {
   //     if (Horizontal == 0f && Vertical1 == 0f)
   //     { // this statement allows it to recenter once the inputs are at zero 
   //         Vector3 curRot = gameObject.transform.localEulerAngles; // the object you are rotating
   //         Vector3 homeRot;

   //         // this section determines the direction it returns home 
   //         if (curRot.y > 180f)
   //         {
   //             Debug.Log(curRot.y);
   //             homeRot = new Vector3(0f, 359.999f, 0f); //it doesnt return to perfect zero 
   //         }
   //         // otherwise it rotates wrong direction 
   //         else
   //         {                                                                      
   //             homeRot = Vector3.zero;
   //         }
   //         gameObject.transform.localEulerAngles = Vector3.Slerp(curRot, homeRot, Time.deltaTime * 2);
   //     }
   //     else
   //     {
   //         gameObject.transform.localEulerAngles = new Vector3(0f, Mathf.Atan2(Horizontal, Vertical1) * 180 / Mathf.PI, 0f); // this does the actual rotaion according to inputs
   //     }
   // }
    void SpawnProjectile()
    {
        spawnPos = this.transform.position + this.transform.forward * spawnDistance;
        //Spawn the projectile
        Instantiate(projectile, spawnPos, this.transform.rotation);
        isSpawning = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            gameManager.playerHealth--;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Health"))
        {
            Destroy(other.gameObject);
            gameManager.playerHealth++;
        }
    }
}