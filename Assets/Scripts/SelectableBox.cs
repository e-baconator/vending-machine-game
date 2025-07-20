using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableBox : MonoBehaviour, ISelectable
{
    [SerializeField] string boxID;
    [SerializeField] Transform pickupHolder;
    [SerializeField] TMPro.TextMeshProUGUI warningText;
    private int bottleCapacity = 12;
    private int numBottles;
    private static Vector3 slot1 = new Vector3(-0.096f, 0.022f, 0.153f);
    private static Vector3 slot2 = new Vector3(-0.096f, 0.022f, -0.068f);
    private static Vector3 slot3 = new Vector3(-0.096f, 0.022f, 0.014f);
    private static Vector3 slot4 = new Vector3(-0.096f, 0.022f, 0.098f);
    private List<Vector3> slots = new List<Vector3>() { slot1, slot2, slot3, slot4 };
    public string GetItemID()
    {
        return boxID;
    }

    public void SetChildPosition(Transform newChild)
    {
        newChild.SetParent(transform);
        newChild.localPosition = slots[numBottles];
        newChild.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }

    public void Use()
    {
        if (numBottles > 0 && pickupHolder.childCount == 0)
        {
            Transform bottle = transform.GetChild(numBottles - 1);
            bottle.parent = null;
            bottle.GetComponent<PhysicsPickup>().Grab(pickupHolder.GetComponentInParent<PickupController>());
            numBottles = numBottles - 1;
        }
        else if (!GetItemID().Equals(pickupHolder.GetChild(0).GetComponent<IPickupable>().GetItemID()))
        {
            StartCoroutine(ShowWarningText("Wrong box for this bottle."));
        }
        else if (numBottles < bottleCapacity && pickupHolder.childCount != 0)
        {
            SetChildPosition(pickupHolder.GetChild(0));
            pickupHolder.GetComponentInParent<PickupController>().RemovePickup();
            numBottles = numBottles + 1;
        }
        else if (numBottles == bottleCapacity && pickupHolder.childCount != 0)
        {
            StartCoroutine(ShowWarningText("This box is full."));
        }
    }

    IEnumerator ShowWarningText(string message)
    {
        warningText.text = message;
        yield return new WaitForSeconds(1.5f);
        warningText.text = string.Empty;
    }

    public void Start()
    {
        numBottles = 0;
        warningText.text = string.Empty;
    }
}
