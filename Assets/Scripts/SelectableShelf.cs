using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableShelf : MonoBehaviour, ISelectable
{
    [SerializeField] Transform pickupHolder;

    [SerializeField] TMPro.TextMeshProUGUI warningText;

    [SerializeField] string shelfID;
    private int numBottles;
    private int bottleCapacity = 4;
    private static Vector3 slot1 = new Vector3(0, -0.0034f, 0.1432f);
    private static Vector3 slot2 = new Vector3(0, -0.002f, 0.028f);
    private static Vector3 slot3 = new Vector3(0, 0, -0.105f);
    private static Vector3 slot4 = new Vector3(0, 0, -0.199f);
    private List<Vector3> slots = new List<Vector3>() { slot1, slot2, slot3, slot4 };

    public string GetItemID()
    {
        return shelfID;
    }
    
    public void SetChildPosition(Transform newChild)
    {
        newChild.SetParent(transform);
        newChild.localPosition = slots[numBottles];
        newChild.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
    }

    public void Use()
    {
        // if (numBottles > 0 && pickupHolder.childCount == 0)
        // {
        //     Transform bottle = transform.GetChild(numBottles - 1);
        //     bottle.parent = null;
        //     bottle.GetComponent<PhysicsPickup>().Grab(pickupHolder.GetComponentInParent<PickupController>());
        //     numBottles = numBottles - 1;
        // }
        // else if (!GetItemID().Equals(pickupHolder.GetChild(0).GetComponent<IPickupable>().GetItemID()))
        // {
        //     StartCoroutine(ShowWarningText("Wrong shelf for this bottle."));
        // }
        // else if (numBottles < bottleCapacity && pickupHolder.childCount != 0)
        // {
        //     SetChildPosition(pickupHolder.GetChild(0));
        //     pickupHolder.GetComponentInParent<PickupController>().RemovePickup();
        //     numBottles = numBottles + 1;
        // }
        // else if (numBottles == bottleCapacity && pickupHolder.childCount != 0)
        // {
        //     StartCoroutine(ShowWarningText("This shelf is full."));
        // }

        Transform box;

        if (pickupHolder.childCount == 0) // no box held
        {
            return;
        }
        else //box held
        {
            box = pickupHolder.GetChild(0);
        }

        if (!GetItemID().Equals(box.GetComponent<IPickupable>().GetItemID())) //wrong box
        {
            StartCoroutine(ShowWarningText("Wrong shelf for this bottle."));
        }
        else if (numBottles < bottleCapacity) //space on shelf available
        {
            Transform bottlesInBox = box.Find("Bottles");
            if (bottlesInBox.childCount == 0) //box has no bottles
            {
                StartCoroutine(ShowWarningText("This box is empty."));
            }
            else //box does have bottles
            {
                SetChildPosition(bottlesInBox.GetChild(bottlesInBox.childCount - 1));
                numBottles = numBottles + 1;
            }
        }
        else if (numBottles == bottleCapacity) //no space on shelf left
        {
            StartCoroutine(ShowWarningText("This shelf is full."));
        }
    }

    void Start()
    {
        numBottles = 0;
        warningText.text = string.Empty;
    }

    IEnumerator ShowWarningText(string message)
    {
        warningText.text = message;
        yield return new WaitForSeconds(1.5f);
        warningText.text = string.Empty;
    }
}
