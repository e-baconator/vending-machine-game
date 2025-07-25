using UnityEngine;

public class VMStatus : MonoBehaviour
{
    [SerializeField] GameObject complete;
    [SerializeField] GameObject incomplete;
    void Start()
    {
        complete.SetActive(false);
        incomplete.SetActive(true);
    }

    void Update()
    {
        if (transform.Find("InteractableShelves").GetComponent<InteractableShelves>().isFull)
        {
            complete.SetActive(true);
            incomplete.SetActive(false);
        }
    }
}
