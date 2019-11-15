using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    Rigidbody playerRb;

    public float enemiesSpawned = 0;

    public float XLimit;
    public float upperZLimit;
    public float lowerZLimit;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //Sets the scenes boundaries
        //Boundaries on the x-axis
        if (player.transform.position.x < -XLimit)
        {
            playerRb.AddForce(new Vector3(3,0,0),ForceMode.Impulse);
            //playerRb.Add
        }
        if (player.transform.position.x > XLimit)
        {
            playerRb.AddForce(new Vector3(-3, 0, 0), ForceMode.Impulse);
        }
        //Boundaries on the z-axis
        if (player.transform.position.z > upperZLimit)
        {
            playerRb.AddForce(new Vector3(0, 0, -3), ForceMode.Impulse);
        }
        if (player.transform.position.z < lowerZLimit)
        {
            playerRb.AddForce(new Vector3(0, 0, 3), ForceMode.Impulse);
        }
    }
}
