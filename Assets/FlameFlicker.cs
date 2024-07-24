using UnityEngine;
using UnityEngine.UI;

public class FlameFlicker : MonoBehaviour
{
    public Image targetImage; // The UI Image component to flicker
    public float minAlpha = 0.4f; // Minimum alpha value (40%)
    public float maxAlpha = 1.0f; // Maximum alpha value (100%)
    public float flickerSpeed = 1.0f; // Speed of the flicker effect

    public bool activeWhilePaused = false;

    private float targetAlpha;
    private float currentAlpha;
    private Color imageColor;

    void Start()
    {
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }
        if (targetImage != null)
        {
            imageColor = targetImage.color;
            currentAlpha = imageColor.a;
            SetRandomTargetAlpha();
        }
        else
        {
            Debug.LogError("No Image component found. Please attach this script to a GameObject with an Image component.");
        }
    }

    void Update()
    {
        if(activeWhilePaused)
        {
            if (targetImage != null)
            {
                currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, flickerSpeed * Time.unscaledDeltaTime);
                imageColor.a = currentAlpha;
                targetImage.color = imageColor;

                if (Mathf.Approximately(currentAlpha, targetAlpha))
                {
                    SetRandomTargetAlpha();
                }
            }
        }

        else
        {
            if (targetImage != null)
            {
                currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, flickerSpeed * Time.deltaTime);
                imageColor.a = currentAlpha;
                targetImage.color = imageColor;

                if (Mathf.Approximately(currentAlpha, targetAlpha))
                {
                    SetRandomTargetAlpha();
                }
            }
        }
     
    }

    void SetRandomTargetAlpha()
    {
        targetAlpha = Random.Range(minAlpha, maxAlpha);
    }
}