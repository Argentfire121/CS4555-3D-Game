using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchObject : MonoBehaviour, Interactable
{

    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        Debug.Log(message: "Wrenched interacted with");
        return true;
    }

    public IEnumerator Teleport()
    {
        throw new System.NotImplementedException();
    }
}
