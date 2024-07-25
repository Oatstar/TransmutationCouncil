using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoFloaterScript : MonoBehaviour
{
    TMP_Text infoFloaterText;
    RectTransform backgroundRectTransform;
    RectTransform thisRectTransform;
    Canvas canvas;

    [SerializeField] private Camera uiCamera;
    Vector2 upwardMovement = new Vector2(0, 0.2f);


    private static InfoFloaterScript instance;

    private void Awake()
    {
        instance = this;

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        backgroundRectTransform = transform.Find("InfoFloaterBG").GetComponent<RectTransform>();
        infoFloaterText = transform.Find("InfoFloaterText").GetComponent<TMP_Text>();
        thisRectTransform = GetComponent<RectTransform>();

        HideInfoText();
    }

    
    private void SetPosition()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 localPoint;

        Vector2 movePos;

        //Vector2 offset = new Vector2(0, 0);

        //// Get the screen dimensions
        //float screenWidth = Screen.width;
        //float screenHeight = Screen.height;

        //// Get the dimensions of the tooltip
        //float tooltipWidth = thisRectTransform.sizeDelta.x;
        //float tooltipHeight = thisRectTransform.sizeDelta.y;

        //// Determine the horizontal offset
        //if (mousePosition.x < screenWidth / 2)
        //{
        //    // Mouse is on the left side, offset to the right
        //    offset.x = tooltipWidth;
        //}
        //else
        //{
        //    // Mouse is on the right side, offset to the left
        //    offset.x = -tooltipWidth;
        //}

        //// Determine the vertical offset
        //if (mousePosition.y < screenHeight / 2)
        //{
        //    // Mouse is on the bottom side, offset to the top
        //    offset.y = tooltipHeight / 2;
        //}
        //else
        //{
        //    // Mouse is on the top side, offset to the bottom
        //    offset.y = -tooltipHeight / 2;
        //}

        // Convert the screen point to a local point within the canvas
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out localPoint);

        //// Apply the offset
        //thisRectTransform.anchoredPosition = localPoint;


        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out movePos);
        Vector3 worldPos = canvas.transform.TransformPoint(movePos);

        // Convert offset to world coordinates and add it to worldPos
        Vector3 worldOffset = canvas.transform.TransformPoint(Vector3.zero);
        transform.position = worldPos + worldOffset;
    }

    private void Update()
    {
        thisRectTransform.anchoredPosition += upwardMovement;
    }

    public void ShowInfoText(string infoTextString)
    {
        gameObject.SetActive(true);

        infoFloaterText.text = infoTextString;
        float padding = 0f;
        Vector2 backgroundSize = new Vector2(infoFloaterText.preferredWidth + padding * 2f, infoFloaterText.preferredHeight/2);
        thisRectTransform.sizeDelta = backgroundSize;

        SetPosition();
        Invoke("HideOnDelay", 3f);
    }

    private void HideInfoText()
    {
        gameObject.SetActive(false);
    }

    void HideOnDelay()
    {
        Destroy(this.gameObject);
    }

    //public static void ShowInfoText_Static(string infoTextString)
    //{
    //    instance.ShowInfoText(infoTextString);
    //    instance.CancelInvoke("HideOnDelay");
    //}

    //public static void HideInfoText_Static()
    //{
    //    instance.Invoke("HideOnDelay", 0.05f);
    //}

    //void HideOnDelay()
    //{
    //    instance.HideInfoText();
    //}
}
