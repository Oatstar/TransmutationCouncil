using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject ItemContainerTier1;
    public GameObject ItemContainerTier2;
    public GameObject ItemContainerTier3;

    public List<GameObject> itemSlotsTier1 = new List<GameObject> { };
    public List<GameObject> itemSlotsTier2 = new List<GameObject> { };
    public List<GameObject> itemSlotsTier3 = new List<GameObject> { };

    public int[] tier1ItemCounts = new int[20];
    public int[] tier2ItemCounts = new int[10];
    public int[] tier3ItemCounts = new int[3];

    public Sprite[] tier1Images;
    public Sprite[] tier2Images;
    public Sprite[] tier3Images;

    public List<Item> tier1Items = new List<Item> { };
    public List<Item> tier2Items = new List<Item> { };
    public List<Item> tier3Items = new List<Item> { };

    public GameObject itemTextCountPrefab;
    public GameObject itemSpritePrefab;

    Color fullColor = new Color(1, 1f, 1f, 1.0f);
    Color emptyColor = new Color(1, 1f, 1f, 0.35f);

    public static ItemManager instance;


    private void Awake()
    {
        instance = this;

        SetupValues();
    }

    void SetupValues()
    {
        CreateItems();
        CacheItemSlots();
        CreateItemSlotData();

        SetStartValues();
        RefreshAllItemsSlots();
    }

    void CacheItemSlots()
    {
        foreach (Transform child in ItemContainerTier1.transform)
        {
            itemSlotsTier1.Add(child.gameObject);
        }

        foreach (Transform child in ItemContainerTier2.transform)
        {
            itemSlotsTier2.Add(child.gameObject);
        }

        foreach (Transform child in ItemContainerTier3.transform)
        {
            itemSlotsTier3.Add(child.gameObject);
        }
    }

    void CreateItemSlotData()
    {
        for (int i = 0; i < itemSlotsTier1.Count; i++)
        {
            GameObject spawnedItem = Instantiate(itemSpritePrefab, transform.position, Quaternion.identity, itemSlotsTier1[i].transform);
            spawnedItem.transform.localPosition = new Vector2(0, 0);
            spawnedItem.name = "Item";
            GameObject spawnedText = Instantiate(itemTextCountPrefab, transform.position, Quaternion.identity, itemSlotsTier1[i].transform);
            spawnedText.transform.localPosition = new Vector2(0, 0);
            
            spawnedItem.GetComponent<Image>().sprite = tier1Images[i];
            spawnedItem.GetComponent<ItemController>().SetCurrentItem(tier1Items[i]);
        }

        for (int i = 0; i < itemSlotsTier2.Count; i++)
        {
            GameObject spawnedItem = Instantiate(itemSpritePrefab, transform.position, Quaternion.identity, itemSlotsTier2[i].transform);
            spawnedItem.transform.localPosition = new Vector2(0, 0);
            spawnedItem.name = "Item";
            GameObject spawnedText = Instantiate(itemTextCountPrefab, transform.position, Quaternion.identity, itemSlotsTier2[i].transform);
            spawnedText.transform.localPosition = new Vector2(0, 0);

            spawnedItem.GetComponent<Image>().sprite = tier2Images[i];
            spawnedItem.GetComponent<ItemController>().SetCurrentItem(tier2Items[i]);
        }

        for (int i = 0; i < itemSlotsTier3.Count; i++)
        {
            GameObject spawnedItem = Instantiate(itemSpritePrefab, transform.position, Quaternion.identity, itemSlotsTier3[i].transform);
            spawnedItem.transform.localPosition = new Vector2(0, 0);
            spawnedItem.name = "Item";
            GameObject spawnedText = Instantiate(itemTextCountPrefab, transform.position, Quaternion.identity, itemSlotsTier3[i].transform);
            spawnedText.transform.localPosition = new Vector2(0, 0);

            spawnedItem.GetComponent<Image>().sprite = tier3Images[i];
            spawnedItem.GetComponent<ItemController>().SetCurrentItem(tier3Items[i]);
        }
    }

    void CreateItems()
    {

        tier1Items.Add(new Item("Fire Essence", 1));
        tier1Items.Add(new Item("Water Droplet", 1));
        tier1Items.Add(new Item("Earth Shard", 1));
        tier1Items.Add(new Item("Air Whisp", 1));
        tier1Items.Add(new Item("Lightning Spark", 1));
        tier1Items.Add(new Item("Ice Crystal", 1));
        tier1Items.Add(new Item("Shadow Fragment", 1));
        tier1Items.Add(new Item("Light Orb", 1));
        tier1Items.Add(new Item("Metallic Ore", 1));
        tier1Items.Add(new Item("Plant Leaf", 1));
        tier1Items.Add(new Item("Spirit Dust", 1));
        tier1Items.Add(new Item("Phoenix Feather", 1));
        tier1Items.Add(new Item("Dragon Scale", 1));
        tier1Items.Add(new Item("Moonstone", 1));
        tier1Items.Add(new Item("Sunflower Petal", 1));
        tier1Items.Add(new Item("Mystic Herb", 1));
        tier1Items.Add(new Item("Alchemist’s Stone", 1));
        tier1Items.Add(new Item("Ectoplasm", 1));
        tier1Items.Add(new Item("Celestial Dust", 1));
        tier1Items.Add(new Item("Vampire Fang", 1));

        tier2Items.Add(new Item("Molten Glass", 2));
        tier2Items.Add(new Item("Thunderstone", 2));
        tier2Items.Add(new Item("Frostfire Gem", 2));
        tier2Items.Add(new Item("Shadowsteel", 2));
        tier2Items.Add(new Item("Solar Leaf", 2));
        tier2Items.Add(new Item("Spirit Vine", 2));
        tier2Items.Add(new Item("Phoenix Armor", 2));
        tier2Items.Add(new Item("Lunar Amulet", 2));
        tier2Items.Add(new Item("Mystic Elixir", 2));
        tier2Items.Add(new Item("Ethereal Blade", 2));

        tier3Items.Add(new Item("Veil of Shadows", 3));
        tier3Items.Add(new Item("Wraithblade", 3));
        tier3Items.Add(new Item("Shadowbrew", 3));

        SetTier2Needs();
        SetTier3Needs();
        
        for (int i = 0; i < tier1Items.Count; i++)
            tier1Items[i].itemId = i;

        for (int i = 0; i < tier2Items.Count; i++)
            tier2Items[i].itemId = i;

        for (int i = 0; i < tier3Items.Count; i++)
            tier3Items[i].itemId = i;

    }

    void SetTier2Needs()
    {
        tier2Items[0].neededItems.Add(0);
        tier2Items[0].neededItems.Add(1);

        tier2Items[1].neededItems.Add(4);
        tier2Items[1].neededItems.Add(2);

        tier2Items[2].neededItems.Add(5);
        tier2Items[2].neededItems.Add(0);

        tier2Items[3].neededItems.Add(6);
        tier2Items[3].neededItems.Add(8);

        tier2Items[4].neededItems.Add(14);
        tier2Items[4].neededItems.Add(7);

        tier2Items[5].neededItems.Add(9);
        tier2Items[5].neededItems.Add(10);

        tier2Items[6].neededItems.Add(11);
        tier2Items[6].neededItems.Add(12);

        tier2Items[7].neededItems.Add(13);
        tier2Items[7].neededItems.Add(18);

        tier2Items[8].neededItems.Add(15);
        tier2Items[8].neededItems.Add(16);

        tier2Items[9].neededItems.Add(17);
        tier2Items[9].neededItems.Add(19);
    }

    void SetTier3Needs()
    {
        tier3Items[0].neededItems.Add(6);
        tier3Items[0].neededItems.Add(7);

        tier3Items[1].neededItems.Add(9);
        tier3Items[1].neededItems.Add(3);
        
        tier3Items[2].neededItems.Add(8);
        tier3Items[2].neededItems.Add(5);

    }

    public Sprite GetItemSprite(Item targetItem)
    {
        if (targetItem.itemTier == 1)
            return tier1Images[targetItem.itemId];
        else if (targetItem.itemTier == 2)
            return tier2Images[targetItem.itemId];
        else if (targetItem.itemTier == 3)
            return tier3Images[targetItem.itemId];
        else
            return null;

    }

    void SetStartValues()
    {
        for (int i = 0; i < tier1ItemCounts.Length; i++)
        {
            tier1ItemCounts[i] = 1;
        }

        for (int i = 0; i < tier2ItemCounts.Length; i++)
        {
            tier2ItemCounts[i] = 0;
        }

        for (int i = 0; i < tier3ItemCounts.Length; i++)
        {
            tier3ItemCounts[i] = 0;
        }

    }

    void RefreshAllItemsSlots()
    {
        for (int i = 0; i < itemSlotsTier1.Count; i++)
        {
            RefreshItemSlot(1, i);
        }
        for (int i = 0; i < itemSlotsTier2.Count; i++)
        {
            RefreshItemSlot(2, i);
        }
        for (int i = 0; i < itemSlotsTier3.Count; i++)
        {
            RefreshItemSlot(3, i);
        }
    }

    void RefreshItemSlot(int tier, int slotId)
    {
        if(tier == 1)
        {
            Image slotImage = itemSlotsTier1[slotId].transform.Find("Item").GetComponent<Image>();
            Debug.Log("tier " + tier + "  slot: " + slotId + " - count: "+ tier1ItemCounts[slotId]);
            if (tier1ItemCounts[slotId] <= 0)
                slotImage.color = emptyColor;
            else
                slotImage.color = fullColor;
        }
        else if (tier == 2)
        {
            Image slotImage = itemSlotsTier2[slotId].transform.Find("Item").GetComponent<Image>();

            Debug.Log("tier " + tier + "  slot: " + slotId + " - count: " + tier2ItemCounts[slotId]);
            if (tier2ItemCounts[slotId] <= 0)
                slotImage.color = emptyColor;
            else
                slotImage.color = fullColor;
        }
        else if (tier == 3)
        {
            Image slotImage = itemSlotsTier3[slotId].transform.Find("Item").GetComponent<Image>();

            Debug.Log("tier " + tier + "  slot: " + slotId + " - count: " + tier3ItemCounts[slotId]);
            if (tier3ItemCounts[slotId] <= 0)
                slotImage.color = emptyColor;
            else
                slotImage.color = fullColor;
        }
    }
}


[System.Serializable]
public class Item
{
    public string itemName;
    public List<int> neededItems = new List<int> {};
    public int itemTier;
    public string infoText;
    public int itemId;

    // Default constructor
    public Item()
    {
        itemName = "";
        itemId = -1;
        neededItems = new List<int> {};
        itemTier = 1;
        infoText = "";
    }

    public Item(string itemName, int itemTier)
    {
        this.itemName = itemName;
        this.itemTier = itemTier;
    }
}