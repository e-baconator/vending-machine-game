using UnityEngine;

public class SimpleGrabSystem : MonoBehaviour
{
    [SerializeField] private Camera characterCamera;
    [SerializeField] private Transform slot;
    private Pickable pickedItem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (pickedItem)
            {
                DropItem(pickedItem);
            }
            else
            {
                var ray = characterCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1.5f))
                {
                    var pickable = hit.transform.GetComponent<Pickable>();
                    if (pickable)
                    {
                        PickItem(pickable);
                    }
                }
            }
        }
    }

    private void PickItem(Pickable item)
    {
        pickedItem = item;
        item.Rb.isKinematic = true;
        item.Rb.linearVelocity = Vector3.zero;
        item.Rb.angularVelocity = Vector3.zero;
        item.transform.SetParent(slot);
        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
    }

    private void DropItem(Pickable item)
    {
        pickedItem = null;
        item.transform.SetParent(null);
        item.Rb.isKinematic = false;
        item.Rb.AddForce(item.transform.forward * 2, ForceMode.VelocityChange);
    }
}
