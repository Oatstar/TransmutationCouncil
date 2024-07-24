using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DragManager : MonoBehaviour
{
    public GameObject dragContainer;
    public Image dragIcon;
    public Item currentlyDragging;
    public DragObject currentDragObject;

    RectTransform dragBoxRectTransform;
    public bool droppedIntoSlot = false;
    public Canvas canvas;

    public static DragManager instance;

    private void Awake()
    {
        instance = this;
        dragBoxRectTransform = dragContainer.GetComponent<RectTransform>();
    }

    public void SetDragData(Item dragItem, GameObject parentObject, DragObject currentdrag)
    {
        

        DroppedInSlot(false);
        // Set values
        currentDragObject = currentdrag;
        currentlyDragging = dragItem;
        dragIcon.sprite = ItemManager.instance.GetItemSprite(dragItem);
        
        //Move to mousepos
        Vector2 mousePosition = Input.mousePosition;
        dragContainer.transform.position = mousePosition;
        
        //Enable
        dragContainer.SetActive(true);

        if (parentObject.tag == "InventorySlot")
        {
            ItemManager.instance.RemoveItemFromCount(dragItem);
        }
        else if (parentObject.tag == "CirclePanel")
        {
            parentObject.GetComponent<CircleSlotController>().RemoveItem();
        }
        else if (parentObject.tag == "EquipmentSlot")
        {
            parentObject.GetComponent<EquipmentSlotsController>().RemoveItem();
        }
    }

    public void ReturnObjectToInventory(Item item)
    {
        AudioManager.instance.PlayPlaceItem();
        ItemManager.instance.AddItemToCount(item);
    }

    public void CloseDragWindow()
    {
        dragContainer.SetActive(false);
        currentlyDragging = null;
        currentDragObject = null;

    }

    public void SetCurrentObjectInSlot()
    {

    }

    private void Update()
    {
        //RefreshPosition();
    }

    public void RefreshPosition()
    {
        //Vector2 mousePosition = Input.mousePosition;
        //Vector2 movePos;

        //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out movePos);
        //dragBoxRectTransform.anchoredPosition = movePos;

        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,Input.mousePosition, canvas.worldCamera,out movePos);

        dragBoxRectTransform.transform.position = canvas.transform.TransformPoint(movePos);

    }

    public void DroppedInSlot(bool state)
    {
        droppedIntoSlot = state;
    }


}
