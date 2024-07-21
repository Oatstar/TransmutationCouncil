using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EquipmentSlotsController : MonoBehaviour
{
    public Image itemEqImage;
    public Item currentItem;

    public void DropNewItem(Item droppedItem)
    {
        currentItem = droppedItem;
        itemEqImage.sprite = ItemManager.instance.GetItemSprite(currentItem);
        itemEqImage.color = new Color(1, 1, 1, 1);

        RefreshEquipmentSlots();
    }

    public void RemoveItem()
    {
        itemEqImage.color = new Color(1, 1, 1, 0);

        currentItem = null;
        itemEqImage.sprite = null;

        GetComponent<ItemSlot>().ClearSlot();

        RefreshEquipmentSlots();
    }

    void RefreshEquipmentSlots()
    {
        CombatManager.instance.RefreshEquipments();
    }
}
