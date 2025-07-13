using UnityEngine;

public class VMInteract : MonoBehaviour, IInteractable
{
    public string InteractMessage => objectInteractMessage;

    string objectInteractMessage;

    [SerializeField] GameObject hinge;

    bool isOpen;

    public void Interact()
    {
        hinge.GetComponent<Animator>().enabled = true;
        if (isOpen)
        {
            hinge.GetComponent<Animator>().Play("VMDoorClose");
            objectInteractMessage = "Press E to open";
            isOpen = false;
        }
        else
        {
            hinge.GetComponent<Animator>().Play("VMDoorOpen");
            objectInteractMessage = "Press E to close";
            isOpen = true;
        }
    }

    public void Start()
    {
        isOpen = false;
        objectInteractMessage = "Press E to open";
        hinge.GetComponent<Animator>().enabled = false;
    }
}
