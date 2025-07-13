using UnityEngine;

public interface IPickupable : IInteractable
{
    public void Grab(PickupController pickupController);
    public void Drop(PickupController pickupController);
    public void SetPositionInParent(Transform newParent);
    public void Use();
}
