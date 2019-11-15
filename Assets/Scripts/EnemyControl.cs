using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    GameManager gameManager;

    Rigidbody rb;

    GameObject followedObject;
   
    Canvas health;
    RectTransform rectTransform;
    public Image healthBar;

    public float enemySpeed = 5;

    public float enemyHealth = 3;

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

        //Rotatate towards the set object
        transform.LookAt(followedObject.transform);
        //Add force to this objects RigidBody
        rb.AddForce(this.transform.forward * enemySpeed);

        //Healthbar update
        healthBar.fillAmount = enemyHealth / 3;

        if (enemyHealth < 1)
        {
            gameManager.score+=2;
            Destroy(gameObject);
        }
    }
}
