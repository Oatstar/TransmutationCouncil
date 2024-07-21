using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler {

    [SerializeField] bool slotFull = false;

    void Start()
    {
        //CheckSlotState();
    }

    public void CheckSlotState()
    {
        if (this.tag == "PotionHolder")
            slotFull = true;
        else if(this.tag == "Pot")
        {
            if (this.transform.childCount >= 9)
                slotFull = true;
            else
                slotFull = false;
        }
        else
        {
            slotFull = false;
            foreach (Transform child in transform)
            {
                if (child.tag == "Ingredient" || child.tag == "Potion")
                    slotFull = true;
            }
        }
    }

    public bool GetSlotFull()
    {
        return slotFull;
    }

    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");

        if (eventData.pointerDrag != null)
        {
            if(slotFull)
            {
                //eventData.pointerDrag.transform.GetComponent<DragManager>().DropCurrentlyHolding();
                Debug.Log("Slot is full. Returning to original slot.");
                InfoTextPopupManager.instance.SpawnInfoTextPopup("Slot full");

            }
            else if(DragManager.instance.currentlyDragging != null)
            {
                DropIntoSlot(DragManager.instance.currentlyDragging);
            }
            else
            {
                // Do nothing
            }
        }
    }

    public void DropIntoSlot(Item dropItem)
    {
        if(this.gameObject.tag == "CirclePanel")
        {
            slotFull = true;
            GetComponent<CircleSlotController>().DropNewItem(dropItem);
            DragManager.instance.DroppedInSlot(true);
        }
        if (this.gameObject.tag == "EquipmentSlot")
        {
            if (!CombatManager.instance.CanModifyCombat())
            {
                InfoTextPopupManager.instance.SpawnInfoTextPopup("Combat changes only when timer is in green color.");
                return;
            }
            
            slotFull = true;
            GetComponent<EquipmentSlotsController>().DropNewItem(dropItem);
            DragManager.instance.DroppedInSlot(true);
        }
        //item.transform.SetParent(this.transform, false);
        //item.transform.localPosition = new Vector3(0, 0, 0);


    }

    public void ClearSlot()
    {
        slotFull = false;
    }

}