using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragObject : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    //public Item currentlyDragging;
    //public Image spriteImage;
    //public GameObject dragContainer;


    void Awake()
    {
        //dragContainer = GameObject.Find("DragItemContainer");
        //spriteImage = dragContainer.transform.Find("Item").GetComponent<Image>();
    }


    public void SetCurrentlyDraggingValues()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragManager.instance.SetDragData(GetComponent<ItemController>().GetItem());
        //spriteImage.gameObject.SetActive(true);
        Debug.Log("drag: " + this.GetComponent<ItemController>().GetItemData());
        Debug.Log("OnBeginDrag");
        //originalParent = this.transform.parent.gameObject;

        //CheckWorkStation();

        //this.transform.SetParent(dragItemContainer.transform);
        //SetInSlot(false);
        //originalParent.transform.GetComponent<ItemSlot>().CheckSlotState();

        //canvasGroup.alpha = .6f;
        //canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        DragManager.instance.RefreshPosition();

        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragManager.instance.CloseDragWindow();
        //Debug.Log("OnEndDrag");
        //canvasGroup.alpha = 1f;
        //canvasGroup.blocksRaycasts = true;

        //if (!inSlot)
        //{
        //    Debug.Log("Item not in slot. Returning to original slot.");
        //    ReturnToOriginalSlot();
        //}
        //else
        //{
        //    transform.parent.GetComponent<ItemSlot>().CheckSlotState();
        //}
        //SoundManager.instance.PlayBasicClick();
    }

    public void DropCurrentlyHolding()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }



}
