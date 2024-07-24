using UnityEngine;

public class VignetteController : MonoBehaviour
{
    private RectTransform rectTransform;
    private Canvas canvas;

    void Start()
    {
        // Get the RectTransform component attached to the same GameObject
        rectTransform = GetComponent<RectTransform>();

        // Get the parent Canvas
        canvas = GetComponentInParent<Canvas>();

        if (rectTransform == null)
        {
            Debug.LogError("No RectTransform found. This script should be attached to a UI element with a RectTransform.");
        }
        if (canvas == null)
        {
            Debug.LogError("No Canvas found. This script should be attached to a UI element inside a Canvas.");
        }
    }

    void Update()
    {
        if (rectTransform != null && canvas != null)
        {
            // Get the current mouse position
            Vector2 mousePosition = Input.mousePosition;

            // Get the screen width and height
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            // Clamp the x and y coordinates
            float clampedX = Mathf.Clamp(mousePosition.x, 0, screenWidth);
            float clampedY = Mathf.Clamp(mousePosition.y, 0, screenHeight);

            // Create a new vector with clamped coordinates
            mousePosition = new Vector2(clampedX, clampedY);

            // Convert the mouse position to the local position of the RectTransform's parent
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), mousePosition, canvas.worldCamera, out localPoint);

            // Set the anchored position to the local point adjusted by the element's size
            rectTransform.anchoredPosition = localPoint;
        }
    }

    public void ResetToCenter()
    {
        if (rectTransform != null)
        {
            // Reset the position to the center of the canvas
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            Debug.LogError("RectTransform is not assigned.");
        }
    }
}