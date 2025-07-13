using UnityEngine;

public class PhysicsPickup : MonoBehaviour, IPickupable
{
    [SerializeField] Rigidbody pickupRigidbody;

    [SerializeField] Collider pickupCollider;
    [SerializeField] Vector3 pickupPositionOffset;
    public virtual string InteractMessage => objectInteractMessage;

    string objectInteractMessage = "Press F to pick up";

    public virtual void Drop(PickupController pickupController)
    {
        transform.parent = null;
        SetPhysicsValues(false);
    }

    public virtual void Grab(PickupController pickupController)
    {
        if (pickupController == null || pickupController.HasPickup)
        {
            return;
        }
        pickupController.GrabPickup(this);
        SetPhysicsValues(true);
    }

    public void Interact(InteractionController interactionController)
    {
        var pickupController = interactionController.GetComponent<PickupController>();
        Grab(pickupController);
    }

    public void SetPositionInParent(Transform newParent)
    {
        transform.parent = newParent;
        transform.localPosition = pickupPositionOffset;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void Use()
    {
        Debug.Log("Pickup used");
    }

    void SetPhysicsValues(bool wasPickedUp)
    {
        pickupRigidbody.isKinematic = wasPickedUp;
        pickupCollider.enabled = !wasPickedUp;
    }
}
