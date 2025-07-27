using System.Collections;
using UnityEngine;

public class SelectableSlot : MonoBehaviour, ISelectable
{
    [SerializeField] Transform pickupHolder;
    [SerializeField] TMPro.TextMeshProUGUI warningText;
    [SerializeField] string slotID;

    private Transform box;

    public void Start()
    {
        if (transform.childCount == 1)
        {
            box = transform.GetChild(0);
        }
    }
    public string GetItemID()
    {
        return slotID;
    }

    public void SetChildPosition(Transform newChild)
    {
        newChild.SetParent(transform);
        newChild.localPosition = new Vector3(0, 0, 0);
        newChild.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
    }

    public void Use()
    {
        if (pickupHolder.childCount == 0)
        {
            if (box == null)
            {
                return;
            }
            else
            {
                box.parent = null;
                box.GetComponent<PhysicsPickup>().Grab(pickupHolder.GetComponentInParent<PickupController>());
            }
        }
        else
        {
            if (!GetItemID().Equals(box.GetComponent<IPickupable>().GetItemID())) //wrong slot
            {
                StartCoroutine(ShowWarningText("Wrong spot for this box."));
            }
            else if (box != null)
            {
                StartCoroutine(ShowWarningText("No space for box here."));
            }
            else
            {
                box = pickupHolder.GetChild(0);
                SetChildPosition(pickupHolder.GetChild(0));
                pickupHolder.GetComponentInParent<PickupController>().RemovePickup();
            }
        }
    }

    IEnumerator ShowWarningText(string message)
    {
        warningText.text = message;
        yield return new WaitForSeconds(1.5f);
        warningText.text = string.Empty;
    }
}
