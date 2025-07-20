using UnityEngine;

public class BoxInteract : MonoBehaviour, IInteractable
{
    [SerializeField] Transform boxTop;
    public string InteractMessage => boxInteractMessage;

    string boxInteractMessage;

    bool isOpen;

    public void Interact(InteractionController interactionController)
    {
        if (!isOpen)
        {
            boxInteractMessage = string.Empty;
            isOpen = true;
            boxTop.GetComponent<MeshRenderer>().enabled = false;
            boxTop.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void Start()
    {
        boxInteractMessage = "Press E to open box.";
        isOpen = false;
        boxTop.GetComponent<MeshRenderer>().enabled = true;
        boxTop.GetComponent<BoxCollider>().enabled = true;
    }
}
