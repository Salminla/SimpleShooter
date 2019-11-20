using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    Rigidbody playerRb;

    public float enemiesSpawned = 0;
    public float score = 0;
    public float playerHealth = 10;
    //public float maxHealth = 10;

    public float XLimit;
    public float upperZLimit;
    public float lowerZLimit;

    //Reference to current score text
    public Text currScoreText;
    public Text healthText;

    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerHealth < 1)
        {
            //Debug.Log("Player ded");
        }

        //Pressing R resets the scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        UiPrint();
        
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

    void UiPrint()
    {
        //UI Updates
        currScoreText.text = "Score: " + score.ToString();
        healthBar.fillAmount = playerHealth / 10;
        healthText.text = "Health " + playerHealth.ToString();

    }

    public void TenSpawned()
    {
        if (playerHealth < 10)
        {
            playerHealth++;
        }
    }
}
