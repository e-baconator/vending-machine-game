using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    [SerializeField] TMPro.TextMeshProUGUI interactionText;
    [SerializeField] float interactionDistance = 5f;
    IInteractable currentTargetedInteractable;

    public void Update()
    {
        UpdateCurrentInteractable();
        UpdateInteractionText();
        CheckForInteractionInput();
    }

    void UpdateCurrentInteractable()
    {
        var ray = playerCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
        Physics.Raycast(ray, out var hit, interactionDistance);

        currentTargetedInteractable = hit.collider?.GetComponent<IInteractable>();
    }

    void UpdateInteractionText()
    {
        if (currentTargetedInteractable == null)
        {
            interactionText.text = string.Empty;
            return;
        }
        interactionText.text = currentTargetedInteractable.InteractMessage;
    }

    void CheckForInteractionInput()
    {
        if (UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame && currentTargetedInteractable != null)
        {
            currentTargetedInteractable.Interact();
        }
    }
}
