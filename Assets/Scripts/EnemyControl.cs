using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    Rigidbody rb;

    GameObject followedObject;

    public float enemySpeed = 5;

    public float enemyHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        //Find the player object and set it to followedObject
        followedObject = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rotatate towards the set object
        transform.LookAt(followedObject.transform);
        //Add force to this objects RigidBody
        rb.AddForce(this.transform.forward * enemySpeed);
    }
}
