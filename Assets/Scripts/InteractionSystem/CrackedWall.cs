using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedWall : MonoBehaviour, Interactable
{
    [SerializeField] private string _prompt;
    [SerializeField] private GameObject teleportLocation;
    [SerializeField] private GameObject player;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log(message: "Teleporting player...");
        teleportPlayer();
        return true;
    }

    public void teleportPlayer()
    {
        StartCoroutine(Teleport());
    }

    public IEnumerator Teleport() {
        player.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        player.transform.position = teleportLocation.transform.position;
        yield return new WaitForSeconds(0.01f);
        player.SetActive(true);
    }

}
