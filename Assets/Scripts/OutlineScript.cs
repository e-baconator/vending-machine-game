using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    private ISelectable selectableObject;

    private int layerMask;

    private float rayDistance = 5f;

    public float delayTime = 0.1f;

    void Start()
    {
        layerMask = LayerMask.GetMask("HighlightSlot");
    }

    void Update()
    {
        // Highlight
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit, rayDistance, layerMask))
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Highlightable"))
            {
                Outline outline = highlight.GetComponent<Outline>();
                if (outline == null)
                {
                    outline = highlight.gameObject.AddComponent<Outline>();
                    outline.OutlineWidth = 8.0f;
                }

                outline.OutlineColor = Color.lightSkyBlue;
                outline.enabled = true;
            }
            else
            {
                highlight = null;
            }
        }

        if (Time.frameCount % 30 == 0) // Example: Execute every 30 frames
        {
            Invoke("OnClick", delayTime);
        }
    }

    void OnClick()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = raycastHit.transform;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;

                // my code
                if (selection.gameObject.TryGetComponent<ISelectable>(out _))
                {
                    selectableObject = (ISelectable)selection.gameObject.GetComponent("ISelectable");
                    selectableObject.Use();
                }
            }
            else
            {
                if (selection)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                }
            }
        }
    }

}
