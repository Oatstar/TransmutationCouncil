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


    public static DragObject instance;

    
    void Awake()
    {
        instance = this;


        //dragContainer = GameObject.Find("DragItemContainer");
        //spriteImage = dragContainer.transform.Find("Item").GetComponent<Image>();
    }


    public void SetCurrentlyDraggingValues()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioManager.instance.PlayBasicClick();

        if (TransmuteManager.instance.GetTransmutationOnGoing())
        {
            InfoTextPopupManager.instance.SpawnInfoTextPopup("Waiting for transmutation to finish");
            return;
        }

        if (this.transform.parent.tag == "CirclePanel")
        {
            DragManager.instance.SetDragData(transform.parent.GetComponent<CircleSlotController>().currentItem, this.transform.parent.gameObject, this);
        }
        else if (this.transform.parent.tag == "EquipmentSlot")
        {
            DragManager.instance.SetDragData(transform.parent.GetComponent<EquipmentSlotsController>().currentItem, this.transform.parent.gameObject, this);
        }
        else //if InventorySlot
        {
            Item thisItem = GetComponent<ItemController>().GetItem();
            if(ItemManager.instance.GetSpecificItemCount(thisItem) > 0)
                DragManager.instance.SetDragData(thisItem, this.transform.parent.gameObject, this);
        }

        //spriteImage.gameObject.SetActive(true);
        //Debug.Log("drag: " + this.GetComponent<ItemController>().GetItemData());
        //Debug.Log("OnBeginDrag");
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

        if (TransmuteManager.instance.GetTransmutationOnGoing())
            return;
        if (DragManager.instance.currentDragObject == null)
            return;

        //Debug.Log("OnDrag");
        DragManager.instance.RefreshPosition();

        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (TransmuteManager.instance.GetTransmutationOnGoing())
            return;
        if (DragManager.instance.currentDragObject == null)
            return;

        Debug.Log("Closed dragwindow");
        if(!DragManager.instance.droppedIntoSlot)
        {
            if(transform.parent.tag == "InventorySlot")
            {
                Debug.Log("Inventory item not dropped into slot. Returning to inventory");
                DragManager.instance.ReturnObjectToInventory(DragManager.instance.currentlyDragging);
            }
            else if(transform.parent.tag == "CirclePanel")
            {
                Debug.Log("CirclePanel item not dropped into slot. Returning to inventory");
                DragManager.instance.ReturnObjectToInventory(DragManager.instance.currentlyDragging);
            }
            else if (transform.parent.tag == "EquipmentSlot")
            {
                Debug.Log("EquipmentSlot item not dropped into slot. Returning to inventory");
                DragManager.instance.ReturnObjectToInventory(DragManager.instance.currentlyDragging);
            }
        }

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
