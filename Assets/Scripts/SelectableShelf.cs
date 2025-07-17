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
    }

    public void Use()
    {
        if (numBottles < bottleCapacity && pickupHolder.childCount != 0)
        {
            //pickedObject = (IPickupable)pickupHolder.GetChild(0);
            SetChildPosition(pickupHolder.GetChild(0));
            pickupHolder.GetComponentInParent<PickupController>().RemovePickup();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numBottles = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
