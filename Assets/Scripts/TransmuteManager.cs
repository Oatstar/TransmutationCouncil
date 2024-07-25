using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmuteManager : MonoBehaviour
{
    public CircleSlotController circleSlot1;
    public CircleSlotController circleSlot2;

    [SerializeField] bool transmutationOnGoing = false;

    public static TransmuteManager instance;

    private void Awake()
    {
        instance = this;
    }
    
    public void StartTransmute()
    {
        AudioManager.instance.PlayBasicClick();

        if (GetTransmutationOnGoing())
        {
            InfoTextPopupManager.instance.SpawnInfoTextPopup("Wait for transmutation to finish");
            return;
        }

        transmutationOnGoing = true;
        StartCoroutine(FinishTransmute());
    }

    public bool GetTransmutationOnGoing()
    {
        return transmutationOnGoing;
    }


    IEnumerator FinishTransmute()
    {

        Item item1 = circleSlot1.currentItem;
        Item item2 = circleSlot2.currentItem;

        if(item1 == null || item2 == null)
        {
            InfoTextPopupManager.instance.SpawnInfoTextPopup("Both pedestals must have an item");

            Debug.Log("Both pedestals must have an item");
        }
        else if (item1.itemId == -1 || item2.itemId == -1)
        {
            InfoTextPopupManager.instance.SpawnInfoTextPopup("Both pedestals must have an item");

            Debug.Log("Both pedestals must have an item");
        }
        else if(item1.itemTier != item2.itemTier)
        {
            InfoTextPopupManager.instance.SpawnInfoTextPopup("Both items must be of the same tier");
        }
        else
        {
            AudioManager.instance.PlayTransmuteSound();
            CircleGraphicController.instance.StartGlow();

            yield return new WaitForSeconds(1.0f);

            // Go ahead and finish
            Item newItem = CheckForTransmute(item1, item2);
            if (newItem != null)
            {
                Debug.Log("Transmute successful");

                ItemManager.instance.AddItemToCount(newItem);

                string succesText = $"{item1.itemName} and {item2.itemName} create {newItem.itemName}.";
                LogTextManager.instance.AddSpecificHintToLog(succesText);
                InfoTextPopupManager.instance.SpawnInfoTextPopup("Transmute successful!");

                AudioManager.instance.PlayTransmuteSuccess();
                ItemManager.instance.RefreshUnlock(newItem);
            }
            else
            {
                string failText = "Transmute failed. No matching item found.";
                Debug.Log(failText);
                LogTextManager.instance.AddSpecificHintToLog(failText);

                AudioManager.instance.PlayTransmuteFailed();
            }

            circleSlot1.ExpendItem();
            circleSlot2.ExpendItem();
        }

        //InfoTextPopupManager.instance.SpawnInfoTextPopup("Transmutation finished.\n ");

        transmutationOnGoing = false;
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
                    //InfoTextPopupManager.instance.SpawnInfoTextPopup("Transmute successful!");
                    //Debug.Log(successText);
                    //LogTextManager.instance.AddSpecificHintToLog(successText);

                    //ItemManager.instance.AddItemToCount(tier2Item);

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
                    //InfoTextPopupManager.instance.SpawnInfoTextPopup("Transmute successful!");
                    //Debug.Log(succesText);

                    //LogTextManager.instance.AddSpecificHintToLog(succesText);

                    //ItemManager.instance.AddItemToCount(tier3Item);

                    return tier3Item;
                }
            }
        }

        
        return null;
    }
}
