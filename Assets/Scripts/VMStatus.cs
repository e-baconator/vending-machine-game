using Unity.VisualScripting;
using UnityEngine;

public class VMStatus : MonoBehaviour
{
    [SerializeField] GameObject complete;
    [SerializeField] GameObject incomplete;
    [SerializeField] PlayerController playerController;

    private bool isComplete = false;
    void Start()
    {
        complete.SetActive(false);
        incomplete.SetActive(true);
    }

    void Update()
    {
        if (!isComplete && transform.Find("InteractableShelves").GetComponent<InteractableShelves>().isFull)
        {
            isComplete = true;
            complete.SetActive(true);
            incomplete.SetActive(false);
            playerController.SetHappiness(50f);
            StartCoroutine(playerController.ShowText("Restocking boost: +5% happiness", Color.paleGreen));
        }
    }
}
