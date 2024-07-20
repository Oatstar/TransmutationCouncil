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
            Item newItem = CheckForTransmute(item1, item2);
            if(newItem != null)
            {
                ItemManager.instance.RefreshUnlock(newItem);
            }
        }
    }

    Item CheckForTransmute(Item item1, Item item2)
    {
        if(item1.itemTier == 1 && item2.itemTier == 1)
        {
            Debug.Log("Both items are tier 2: "+item1.itemName + " ... "+item2.itemName);
            foreach (Item tier2Item in ItemManager.instance.tier2Items)
            {
                Debug.Log("Checking item: "+tier2Item.itemName);

                if (tier2Item.neededItems.Contains(item1.itemId) && tier2Item.neededItems.Contains(item2.itemId))
                {
                    string successText = $"{item1.itemName} and {item2.itemName} create {tier2Item.itemName}.";
                    Debug.Log(successText);
                    LogTextManager.instance.AddSpecificHintToLog(successText);

                    ItemManager.instance.AddItemToCount(2, tier2Item.itemId);

                    return tier2Item;
                }
            }
        } 
        else if(item1.itemTier == 2 && item2.itemTier == 2)
        {
            foreach (Item tier3Item in ItemManager.instance.tier3Items)
            {
                if (tier3Item.neededItems.Contains(item1.itemId) && tier3Item.neededItems.Contains(item2.itemId))
                {
                    string succesText = $"{item1.itemName} and {item2.itemName} create {tier3Item.itemName}.";
                    Debug.Log(succesText);
                    LogTextManager.instance.AddSpecificHintToLog(succesText);

                    ItemManager.instance.AddItemToCount(3, tier3Item.itemId);

                    return tier3Item;
                }
            }
        }


        string failText = "Transmute failed. No matching item found.";
        Debug.Log(failText);
        LogTextManager.instance.AddSpecificHintToLog(failText);
        return null;
    }
}
