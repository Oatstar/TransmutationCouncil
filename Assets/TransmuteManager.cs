using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmuteManager : MonoBehaviour
{
    public CircleSlotController circleSlot1;
    public CircleSlotController circleSlot2;

    public void StartTransmute()
    {
        Item item1 = circleSlot1.currentItem;
        Item item2 = circleSlot2.currentItem;

        if(item1 == null || item2 == null)
        {
            Debug.Log("Empty slots on transmutation circle. Both items must be in place");
        }
        else
        {
            CheckForTransmute(item1, item2);
        }
    }

    Item CheckForTransmute(Item item1, Item item2)
    {
        foreach (Item tier2Item in ItemManager.instance.tier2Items)
        {
            if (tier2Item.neededItems.Contains(item1.itemId) && tier2Item.neededItems.Contains(item2.itemId))
            {
                Debug.Log($"Transmute success! {item1.itemName} and {item2.itemName} create {tier2Item.itemName}.");
                return tier2Item;
            }
        }

        Debug.Log("Transmute failed. No matching Tier 2 item found.");
        return null;
    }
}
