using UnityEngine;

public class VMInteract : MonoBehaviour, IInteractable
{
    public string InteractMessage => objectInteractMessage;

    [SerializeField] string objectInteractMessage = "Press E to open";

    [SerializeField] GameObject hinge;

    public void Interact()
    {
        hinge.SetActive(true);
    }
}
