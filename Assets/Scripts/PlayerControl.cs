using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rb;

    GameManager gameManager;

    //Movement axis
    float Horizontal;
    float Vertical;
    float Vertical1;
    float Strafe;

    //Input actions
    PlayerInputActions inputAction;

    //Move
    Vector2 movementInput;
    //FireDirection
    Vector2 lookDirection;

    public Camera mainCamera;
    public GameObject projectile;
    //Stores the force the player is being pushed with
    public float forwardForce = 10;
    //Stores the players rotate speed in degrees 
    public float rotateSpeed = 5;

    //Audio
    public AudioClip shoot;
    public AudioClip thruster;

    //Paricle effects
    public ParticleSystem thrustEffect;
    public ParticleSystem shootEffect;
    public GameObject destroyedPrefab;

    private AudioSource myAudioSource;

    //Projectile stuff---------------
    float spawnDistance = 3;
    Vector3 spawnPos;
    bool isSpawning;
    //-------------------------------

    float test;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        rb = GetComponent<Rigidbody>();

        myAudioSource = gameObject.GetComponent<AudioSource>();

        thrustEffect = GameObject.FindGameObjectWithTag("Thrust").GetComponent<ParticleSystem>();
        //shootEffect = GameObject.FindGameObjectWithTag("Shoot").GetComponent<ParticleSystem>();

        isSpawning = false;

        inputAction = new PlayerInputActions();
        inputAction.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.Player.FireDirection.performed += ctx => lookDirection = ctx.ReadValue<Vector2>();

        //thrustEffect.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //NEW INPUT SYSTEM
        //Get the last used gamepad
        var gamepad = Gamepad.current;
        //Get controllers analog sticks, use if no InputActions in use
        Vector2 lsMove = gamepad.leftStick.ReadValue();
        Vector2 rsMove = gamepad.rightStick.ReadValue();
        //New input system
        float h = movementInput.x;
        float v = movementInput.y;

        float look = lookDirection.x;

        //OLD UNITY INPUT SYSTEM
        ////Forward and back turn axis/input
        //this.Vertical = Input.GetAxis("Vertical");
        ////Left and right turn axis
        //this.Horizontal = Input.GetAxis("Horizontal");
        ////Up down facing axis
        //this.Vertical1 = Input.GetAxis("Vertical1");
        ////Strafing
        //this.Strafe = Input.GetAxis("Strafe");

        if (gamepad.rightTrigger.IsActuated(0.1f))//Keyboard.current.spaceKey.isPressed)
        {
            //Set the spawn position for the projectile
            if (!isSpawning)
            {
                Invoke("SpawnProjectile", 0.1f);
                myAudioSource.PlayOneShot(shoot);
                isSpawning = true;
            }
           
        }

        //Forward movement
        rb.AddForce(this.transform.forward * forwardForce * v);

        //Move player left and right
        rb.AddForce(this.transform.right * forwardForce * h);

        //Rotate the player in direction the joystick is pointing.
        TurnThePlayer();

        if (v < 0.1f)
            thrustEffect.Play();


        Debug.Log(v);

        if (gameManager.playerHealth < 1)
        {
            Explode();
            gameObject.SetActive(false);
        }

        //Rotate player clockwise and counter-clockwise
        //this.transform.Rotate(new Vector3(0, rotateSpeed * look * Time.deltaTime * 10

        //Vector3 relativePos = new Vector3((transform.position.x + this.Horizontal), transform.position.y, transform.position.z + this.Vertical1) - transform.position;
        //Quaternion targetRotation = Quaternion.LookRotation(relativePos);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime);

        //Vector3 lookDirection = new Vector3(Horizontal, 0, Vertical1);
        //this.gameObject.transform.rotation = Quaternion.Euler(lookDirection);

        //Vector3 lookVec = new Vector3(this.Horizontal, this.Vertical1, 4096);
        //if (lookVec.x != 0 && lookVec.y != 0)
        //    transform.rotation = Quaternion.LookRotation(lookVec, Vector3.back);
    }
    void TurnThePlayer()
    {
        Vector3 playerDirection = Vector3.right * lookDirection.x + Vector3.forward * lookDirection.y;
        if (playerDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        }
        //Vector2 input = lookDirection;

        ////Convert input to a Vector3 where the Y axis will be used as the Z axis
        //Vector3 lookPosition = new Vector3(input.x, 0, input.y);
        //var lookRot = mainCamera.transform.TransformDirection(lookPosition);
        //lookRot = Vector3.ProjectOnPlane(lookRot, Vector3.up);

        //if (lookRot != Vector3.zero)
        //{
        //    Quaternion newRotation = Quaternion.LookRotation(lookRot);
        //    rb.MoveRotation(newRotation);
        //}
    }
    void SpawnProjectile()
    {
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
    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }
}