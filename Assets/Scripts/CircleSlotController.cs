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
        itemCircleImage.color = new Color(1, 1, 1,1);
        //itemCircleImage.gameObject.SetActive(true);
    }

    public void RemoveItem()
    {
        if (TransmuteManager.instance.GetTransmutationOnGoing())
            return;

        //itemCircleImage.gameObject.SetActive(false);
        itemCircleImage.color = new Color(1, 1, 1, 0);

        currentItem = null;
        itemCircleImage.sprite = null;

        GetComponent<ItemSlot>().ClearSlot();
    }

    public void ExpendItem()
    {
        itemCircleImage.color = new Color(1, 1, 1, 0);

        currentItem = null;
        itemCircleImage.sprite = null;

        GetComponent<ItemSlot>().ClearSlot();
    }
}
