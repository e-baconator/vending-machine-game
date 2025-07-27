using System.Collections;
using UnityEngine;

public class SelectableRecycling : MonoBehaviour, ISelectable
{
    [SerializeField] PickupController pickupController;
    [SerializeField] Transform pickupHolder;

    [SerializeField] TMPro.TextMeshProUGUI warningText;
    public string GetItemID()
    {
        return string.Empty;
    }

    public void SetChildPosition(Transform newChild)
    {
        pickupController.RemovePickup();
        newChild.SetParent(transform);
        newChild.localPosition = new Vector3(0, 0, 0);
        newChild.localScale = new Vector3(0.001f, 0.001f, 0.001f);
    }

    public void Use()
    {
        Transform box;

        if (pickupHolder.childCount == 0) // no box held
        {
            return;
        }
        else //box held
        {
            box = pickupHolder.GetChild(0);
        }

        Transform bottles = box.Find("Bottles");

        if (bottles.childCount == 0) // can recycle
        {
            SetChildPosition(box);
        }
        else // can't recycle
        {
            StartCoroutine(ShowWarningText("Box must be empty for recycling."));
        }
    }
    
    IEnumerator ShowWarningText(string message)
    {
        warningText.text = message;
        yield return new WaitForSeconds(1.5f);
        warningText.text = string.Empty;
    }
}
