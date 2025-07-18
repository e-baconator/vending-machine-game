using UnityEngine;

public class VMInteract : MonoBehaviour, IInteractable
{
    public string InteractMessage => objectInteractMessage;

    string objectInteractMessage;

    [SerializeField] Animator animator;

    bool isOpen;

    public void Interact(InteractionController interactionController)
    {
        animator.enabled = true;
        if (isOpen)
        {
            animator.Play("VMDoorClose");
            objectInteractMessage = "Press E to open";
            isOpen = false;
        }
        else
        {
            animator.Play("VMDoorOpen");
            objectInteractMessage = "Press E to close";
            isOpen = true;
        }
    }

    public void Start()
    {
        isOpen = false;
        objectInteractMessage = "Press E to open";
        animator.enabled = false;
    }
}
