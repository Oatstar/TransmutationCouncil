using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSlotController : MonoBehaviour
{
    public Image itemCircleImage;
    public Item currentItem;

    public void DropNewItem(Item droppedItem)
    {
        currentItem = droppedItem;
        itemCircleImage.sprite = ItemManager.instance.GetItemSprite(currentItem);
        itemCircleImage.gameObject.SetActive(true);
    }

    public void RemoveItem()
    {
        itemCircleImage.gameObject.SetActive(false);

        currentItem = null;
        itemCircleImage.sprite = null;

        GetComponent<ItemSlot>().ClearSlot();
    }
}
