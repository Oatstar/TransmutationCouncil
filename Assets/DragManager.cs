using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DragManager : MonoBehaviour
{
    public GameObject dragContainer;
    public Image dragIcon;
    public Item currentlyDragging;

    RectTransform dragBoxRectTransform;

    public static DragManager instance;

    private void Awake()
    {
        instance = this;
        dragBoxRectTransform = dragContainer.GetComponent<RectTransform>();
    }

    public void SetDragData(Item dragItem)
    {
        // Set values
        currentlyDragging = dragItem;
        dragIcon.sprite = ItemManager.instance.GetItemSprite(dragItem);
        
        //Move to mousepos
        Vector2 mousePosition = Input.mousePosition;
        dragContainer.transform.position = mousePosition;
        
                //Enable
        dragContainer.SetActive(true);
    }

    public void CloseDragWindow()
    {
        dragContainer.SetActive(false);
        currentlyDragging = null;

    }

    public void RefreshPosition()
    {
        Vector2 mousePosition = Input.mousePosition;

        dragBoxRectTransform.anchoredPosition = mousePosition;
    }

    private void Update()
    {
        //if(dragContainer.activeSelf)
        //{
        //    Vector2 mousePosition = Input.mousePosition;

        //    dragContainer.transform.position = mousePosition;
        //}
    }


}
