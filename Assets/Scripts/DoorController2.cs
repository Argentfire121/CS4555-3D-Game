using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController2 : MonoBehaviour
{
    Animator doorAnim;
    public GameObject intIcon, DoorRight, DoorLeft;
    public float openTime;

    // Start is called before the first frame update
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intIcon.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                intIcon.SetActive(false);
                doorAnim.SetBool("isOpening", true);
                StartCoroutine(closeDoor());
            }
        }
    }

    IEnumerator closeDoor()
    {
        yield return new WaitForSeconds(openTime);
        doorAnim.SetBool("isOpening", false);
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intIcon.SetActive(false);
        }
    }
}
