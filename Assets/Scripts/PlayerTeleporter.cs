using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    //Gets the player script
    PlayerMovementScript moveScript;

    void Start()
    {
        //Grabs the player script to use it later
        moveScript = gameObject.GetComponent<PlayerMovementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug line to test if the player will teleport
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(Teleport());
        }
    }

    //Acts like a method
    IEnumerator Teleport()
    {
        moveScript.isDisabled = true;
        yield return new WaitForSeconds(0.01f);
        gameObject.transform.position = new Vector3(0f, 2f, 0f);
        yield return new WaitForSeconds(0.01f);
        moveScript.isDisabled = false;
    }
}
