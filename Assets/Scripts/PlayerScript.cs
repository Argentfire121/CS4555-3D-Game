using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;

    float speed = 5.0f;
    float speedRotate = 100.0f;
    float gravity = -9.81f;

    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        //velocity.y += gravity * Time.deltaTime;

        if (Input.GetKey("w"))
        {
            print("w");
            Vector3 movement = new Vector3(0.0f, 0.0f, 1.0f * Time.deltaTime * speed);
            movement = transform.TransformDirection(movement);
            controller.Move(movement);
            //controller.Move(velocity);
        }
        if (Input.GetKey("a"))
        {
            print("a");
            Vector3 rotation = new Vector3(0.0f, -1.0f * Time.deltaTime * speedRotate, 0.0f);
            transform.Rotate(rotation);
            //controller.Move(velocity);
        }
        if (Input.GetKey("s"))
        {
            print("s");
            Vector3 movement = new Vector3(0.0f, 0.0f, -1.0f * Time.deltaTime * speed);
            movement = transform.TransformDirection(movement);
            controller.Move(movement);
            //controller.Move(velocity);
        }
        if (Input.GetKey("d"))
        {
            print("d");
            Vector3 rotation = new Vector3(0.0f, 1.0f * Time.deltaTime * speedRotate, 0.0f);
            transform.Rotate(rotation);
            //controller.Move(velocity);
        }
    }
}