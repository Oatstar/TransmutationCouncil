using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    string itemName = "PlaceHolder";
    public Item currentItem;

    public void SetCurrentItem(Item newItem)
    {
        currentItem = newItem;
    }

    public string GetItemName()
    {
        return itemName;
    }

    public Item GetItem()
    {
        return currentItem;
    }

    public string GetItemData()
    {
        return ("Name: " + currentItem.itemName + " --- Tier: " + currentItem.itemTier);
    }
}
