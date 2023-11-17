using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;
    public GameObject followTransform;
    
    //Player's speed
    public float speed = 12f;
    public float gravity = (-9.81f);
    public float jumpHeight = 3f;
    public float sensitivity = 0.5f;

    //Player health
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBarScript healthBar;
    
    //Player ground checking
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //Player movement
    Vector3 velocity;
    bool isGrounded;

    //Player teleport?
    public bool isDisabled = false;

    //Time for player health drainage
    [SerializeField] private float coef = 0.1f;
    [SerializeField] private float healthPercentRestore = 0.1f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //currentHealth -= coef * Time.deltaTime;
        if (currentHealth > 0.0)
        {
            TakeDamage(coef * Time.deltaTime);
        }
        

        //Debug health lines
        if (Input.GetKeyDown(KeyCode.M))
        {
            TakeDamage(1);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage(-1);
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float cameraX = Input.GetAxis("Mouse X") * sensitivity;
        float cameraY = Input.GetAxis("Mouse Y") * sensitivity;

        Vector3 move = transform.right * x + transform.forward * z;

        if (!isDisabled)
        {
            PlayerMovement(move);

            MoveCamera(cameraX, cameraY);  
        }
    }

    void PlayerMovement(Vector3 move)
    {
        //Player movement
        controller.Move(move * speed * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //Player falling down
        velocity.y += (gravity * Time.deltaTime);

        //Playing falling due to gravity
        controller.Move(velocity * Time.deltaTime);
    }

    void MoveCamera(float cameraX, float cameraY)
    {
        //Rotates the character left and right with mouse movement
        transform.Rotate(0, cameraX, 0);

        //Rotates the character up and down with the mouse movement
        followTransform.transform.rotation *= Quaternion.AngleAxis(((-1) * (cameraY * sensitivity)), Vector3.right);

        var angles = followTransform.transform.localEulerAngles;
        
        var angle = followTransform.transform.localEulerAngles.x;

        //Limits up/down rotation of the character
        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 80)
        {
            angles.x = 80;
        }

        followTransform.transform.localEulerAngles = angles;
        followTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
    }

    void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        //healthBar.SetHealth((int) currentHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Grabs a certain percentage of the maxHealth so it can be used
        //to restore the player's health
        float addedHealth = (maxHealth * healthPercentRestore);

        if (collision.gameObject.CompareTag("NewRoomTrigger"))
        {
            currentHealth += addedHealth;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            healthBar.SetHealth(currentHealth);
            
            Destroy(collision.gameObject);
        }
    }
}
