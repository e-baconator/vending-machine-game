using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class BoxInteract : MonoBehaviour, IInteractable
{
    [SerializeField] Transform boxTop;
    public string InteractMessage => boxInteractMessage;

    string boxInteractMessage;

    public bool isOpen;

    public void Interact(InteractionController interactionController)
    {
        if (!isOpen)
        {
            boxInteractMessage = string.Empty;
            isOpen = true;
            boxTop.GetComponent<MeshRenderer>().enabled = false;
            boxTop.GetComponent<BoxCollider>().enabled = false;
        }
        int initialChildCount = transform.childCount;
        for (int child = 0; child < initialChildCount; child++)
        {
            PhysicsPickup script = transform.GetChild(child).GetComponent<PhysicsPickup>();
            if (script != null)
            {
                //script.SetPhysicsValues(false);
                transform.parent = null;
            }
        }
    }

    public void Start()
    {
        boxInteractMessage = "Press E to open box.";
        isOpen = false;
        boxTop.GetComponent<MeshRenderer>().enabled = true;
        boxTop.GetComponent<BoxCollider>().enabled = true;
        for (int child = 0; child < transform.childCount; child++)
        {
            PhysicsPickup script = transform.GetChild(child).GetComponent<PhysicsPickup>();
            if (script != null)
            {
                script.SetPhysicsValues(true);
            }
        }
    }
}
