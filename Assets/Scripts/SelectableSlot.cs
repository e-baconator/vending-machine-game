using System.Collections;
using UnityEngine;

public class SelectableSlot : MonoBehaviour, IPickupable
{
    [SerializeField] Transform pickupHolder;
    [SerializeField] TMPro.TextMeshProUGUI warningText;
    [SerializeField] string slotID;
    private Transform box;

    private bool hasBox;

    private Vector3 pickupPositionOffset = new Vector3(0.02f, -0.18f, 0.08f);
    private Quaternion pickupRotationOffset = Quaternion.Euler(0, 90, 0);

    private string slotMessage = "Press E to pick up box.";
    public string InteractMessage => slotMessage;

    public void Start()
    {
        box = transform.GetChild(0);
        hasBox = true;
    }

    public void Drop(PickupController pickupController)
    {
        if (pickupHolder.childCount == 0)
        {
            return;
        }
        else
        {
            if (!pickupHolder.GetComponentInChildren<IPickupable>().GetItemID().Equals(GetItemID()))
            {
                StartCoroutine(ShowWarningText("Wrong box for this slot."));
            }
            else if (pickupHolder.GetChild(0).Find("Bottles").childCount == 0)
            {
                StartCoroutine(ShowWarningText("This box is empty."));
            }
            else
            {
                box = pickupHolder.GetChild(0);
                box.parent = transform;
                box.localPosition = new Vector3(0, 0, 0);
                box.localRotation = Quaternion.Euler(0, 0, -90);
                pickupHolder.GetComponentInParent<PickupController>().RemovePickup();

                hasBox = true;
                SetPhysicsValues(false);
                slotMessage = "Press E to pick up box.";
            }
        }
    }

    public string GetItemID()
    {
        return slotID;
    }

    public void Grab(PickupController pickupController)
    {
        if (pickupController == null || pickupController.HasPickup)
        {
            return;
        }
        pickupController.GrabPickup(this);
        box = pickupHolder.GetChild(0);
        SetPhysicsValues(true);
        hasBox = false;
        slotMessage = "Press E to place box.";
    }

    public void Interact(InteractionController interactionController)
    {
        var pickupController = interactionController.GetComponent<PickupController>();
        if (hasBox)
        {
            Grab(pickupController);
        }
        else
        {
            Drop(pickupController);
        }
    }

    public void SetPositionInParent(Transform newParent)
    {
        box.parent = newParent;
        box.localPosition = pickupPositionOffset;
        box.localRotation = pickupRotationOffset;
    }

    public void Use()
    {
        box.GetComponent<IPickupable>().Use();
    }

    public void SetPhysicsValues(bool wasPickedUp)
    {
        box.GetComponent<Rigidbody>().isKinematic = wasPickedUp;
        box.GetComponent<BoxCollider>().enabled = !wasPickedUp;
    }

    IEnumerator ShowWarningText(string message)
    {
        warningText.text = message;
        yield return new WaitForSeconds(1.5f);
        warningText.text = string.Empty;
    }
}
