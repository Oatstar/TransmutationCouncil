using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    TMP_Text tooltipText;
    RectTransform backgroundRectTransform;
    RectTransform thisRectTransform;
    Canvas canvas;

    [SerializeField] private Camera uiCamera;

    private static Tooltip instance;

    private void Awake()
    {
        instance = this;

        canvas = GetComponentInParent<Canvas>();
        backgroundRectTransform = transform.Find("TooltipBG").GetComponent<RectTransform>();
        tooltipText = transform.Find("TooltipText").GetComponent<TMP_Text>();
        thisRectTransform = GetComponent<RectTransform>();

        HideToolTip();
    }

    private void Start()
    {
        ShowToolTip("");
        HideToolTip();
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector2 offset = new Vector2(0, 0);
        // Get the width of the screen
        float screenWidth = Screen.width;
        float screenheight = Screen.height;

        // Get the width of the tooltip rect
        float tooltipWidth = thisRectTransform.sizeDelta.x;
        float tooltipHeight = thisRectTransform.sizeDelta.y;

        if (mousePosition.x < screenWidth / 2)
        {
            // Mouse is on the left side, offset to the right
            offset.x = tooltipWidth/2;
        }
        else
        {
            // Mouse is on the right side, offset to the left
            offset.x = -tooltipWidth/2;
        }
        if(mousePosition.y < screenWidth /2)
        {
            offset.y = tooltipHeight / 2;
        }
        else
        {
            offset.y = -tooltipHeight / 2;
        }
        thisRectTransform.anchoredPosition = mousePosition+offset;

    }

    private void ShowToolTip(string tooltipString)
    {
        tooltipText.text = tooltipString;
        float padding = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + padding * 2f, tooltipText.preferredHeight + padding * 2f);
        thisRectTransform.sizeDelta = backgroundSize;

        gameObject.SetActive(true);
    }

    private void HideToolTip()
    {
        gameObject.SetActive(false);
        tooltipText.text = "  ";
        float padding = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + padding * 2f, tooltipText.preferredHeight + padding * 2f);
        thisRectTransform.sizeDelta = backgroundSize;
    }


    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowToolTip(tooltipString);
        instance.CancelInvoke("HideOnDelay");
    }

    public static void HideTooltip_Static()
    {
        instance.Invoke("HideOnDelay", 0.05f);
    }

    void HideOnDelay()
    {
        instance.HideToolTip();
    }
}
