using UnityEngine;

public class SelectableShelf : MonoBehaviour, ISelectable
{
    [SerializeField] Transform pickupHolder;
    private int numBottles;
    private int bottleCapacity = 3;

    private IPickupable pickedObject;
    public void SetChildPosition(Transform newChild)
    {
        newChild.SetParent(transform);
        if (numBottles == 0)
        {
            newChild.localPosition = new Vector3(0, -0.002f, 0.028f);
            newChild.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
    }

    public void Use()
    {
        if (numBottles < bottleCapacity && pickupHolder.childCount != 0)
        {
            SetChildPosition(pickupHolder.GetChild(0));
            pickupHolder.GetComponentInParent<PickupController>().RemovePickup();
            numBottles = numBottles + 1;
        }
        else if (numBottles == bottleCapacity && pickupHolder.childCount != 0)
        {
            // send a message to say the shelf is full
        }
        else if (numBottles > 0 && pickupHolder.childCount == 0)
        {
            Transform bottle = transform.GetChild(numBottles - 1);
            bottle.parent = null;
            //IPickupable pickupableBottle = (IPickupable)bottle;
            bottle.GetComponent<PhysicsPickup>().Grab(pickupHolder.GetComponentInParent<PickupController>());
            //pickupableBottle.Grab(pickupHolder.GetComponentInParent<PickupController>());
            numBottles = numBottles - 1;
        }
    }

    void Start()
    {
        numBottles = 0;
    }
}
