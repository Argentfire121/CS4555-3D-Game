using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;
    public GameObject followTransform;
    
    public float speed = 12f;
    public float gravity = (-9.81f);
    public float jumpHeight = 3f;
    public float sensitivity = 0.5f;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
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

        //Rotates the character left and right with mouse movement
        transform.Rotate(0, cameraX, 0);

        //Rotates the character up and down with the mouse movement
        followTransform.transform.rotation *= Quaternion.AngleAxis((-1) * (cameraY * sensitivity), Vector3.right);

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
}
