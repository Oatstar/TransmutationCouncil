using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

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

    public bool[] tier2Unlocks = new bool[10];
    public bool[] tier3Unlocks = new bool[3];

    //public int[] tier2Knowledge = new int[10]; //-1 = none, 0 = item1, 1 = item2, 2 = bothItems
    //public int[] tier3Knowledge  = new int[3]; //-1 = none, 0 = item1, 1 = item2, 2 = bothItems

    public List<Item> tier1Items = new List<Item> { };
    public List<Item> tier2Items = new List<Item> { };
    public List<Item> tier3Items = new List<Item> { };

    public GameObject itemTextCountPrefab;
    public GameObject itemSpritePrefab;

    Color fullColor = new Color(1, 1f, 1f, 1.0f);
    Color blackColor = new Color(0, 0f, 0f, 0.75f);

    Color emptyColor = new Color(1, 1f, 1f, 0.35f);
    Color noColor = new Color(1, 1f, 1f, 0.0f);


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
            spawnedText.name = "ItemCount";

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
            spawnedText.name = "ItemCount";

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
            spawnedText.name = "ItemCount";

            spawnedItem.GetComponent<Image>().sprite = tier3Images[i];
            spawnedItem.GetComponent<ItemController>().SetCurrentItem(tier3Items[i]);
        }
    }

    void CreateItems()
    {

        tier1Items.Add(new Item("Fire Essence", 1, "A glowing red crystal that radiates heat"));
        tier1Items.Add(new Item("Water Droplet", 1, "A pristine droplet of pure water with magical properties"));
        tier1Items.Add(new Item("Earth Shard", 1, "A chunk of the ground infused with elemental earth energy"));
        tier1Items.Add(new Item("Air Wisp", 1, "An ethereal, swirling mist of air"));
        tier1Items.Add(new Item("Lightning Spark", 1, "A small, crackling ball of electricity"));
        tier1Items.Add(new Item("Ice Crystal", 1, "A shard of ice that never melts"));
        tier1Items.Add(new Item("Shadow Fragment", 1, "A piece of darkness given form"));
        tier1Items.Add(new Item("Light Orb", 1, "A glowing sphere of pure light"));
        tier1Items.Add(new Item("Metallic Ore", 1, "A raw, unrefined chunk of metal"));
        tier1Items.Add(new Item("Plant Leaf", 1, "A leaf from a magical plant with transmutative properties"));
        tier1Items.Add(new Item("Spirit Dust", 1, "Glimmering dust that contains spiritual energy"));
        tier1Items.Add(new Item("Phoenix Feather", 1, "A feather from a legendary firebird"));
        tier1Items.Add(new Item("Dragon Scale", 1, "A tough, shimmering scale from a dragon"));
        tier1Items.Add(new Item("Moonstone", 1, "A gem that glows with the light of the moon"));
        tier1Items.Add(new Item("Sunflower Petal", 1, "A petal from a sunflower that grows only in sunlight"));
        tier1Items.Add(new Item("Mystic Herb", 1, "A rare herb with powerful healing properties"));
        tier1Items.Add(new Item("Alchemist’s Stone", 1, "A stone said to amplify transmutation power"));
        tier1Items.Add(new Item("Ectoplasm", 1, "A semi-transparent substance left by spirits"));
        tier1Items.Add(new Item("Celestial Dust", 1, "Stardust with properties of the cosmos"));
        tier1Items.Add(new Item("Vampire Fang", 1, "A fang from a vampire, rumored to have dark powers"));

        tier2Items.Add(new Item("Molten Glass", 2, "This glass is hot to the touch and radiates a faint glow"));
        tier2Items.Add(new Item("Thunderstone", 2, "This stone crackles with electric energy"));
        tier2Items.Add(new Item("Frostfire Gem", 2, "This gem has both fiery and icy properties"));
        tier2Items.Add(new Item("Shadowsteel", 2, "This metal is extremely tough and has a dark, glossy finish"));
        tier2Items.Add(new Item("Solar Leaf", 2, "This leaf glows brightly and is often used in powerful potions"));
        tier2Items.Add(new Item("Spirit Vine", 2, "This vine is infused with spiritual energy"));
        tier2Items.Add(new Item("Phoenix Armor", 2, "This armor has regenerative properties"));
        tier2Items.Add(new Item("Lunar Amulet", 2, "This amulet grants the wearer protection from dark magic"));
        tier2Items.Add(new Item("Mystic Elixir", 2, "Can cure most ailments and grants temporary magical abilities"));
        tier2Items.Add(new Item("Ethereal Blade", 2, "This blade can harm both physical and ethereal beings"));

        tier3Items.Add(new Item("Veil of Shadows", 3, "This cloak allows wearer to pass through solid objects briefly"));
        tier3Items.Add(new Item("Wraithblade", 3, "This spectral sword glows with an eerie light"));
        tier3Items.Add(new Item("Shadowbrew", 3, "This powerful potion enhances the user's transmutation abilities, especially with dark and forbidden materials"));

        SetTier2Needs();
        SetTier3Needs();
        
        for (int i = 0; i < tier1Items.Count; i++)
        {
            tier1Items[i].itemId = i;
            tier1Items[i].itemSprite = tier1Images[i];
        }

        for (int i = 0; i < tier2Items.Count; i++)
        {
            tier2Items[i].itemId = i;
            tier2Items[i].itemSprite = tier2Images[i];
        }

        for (int i = 0; i < tier3Items.Count; i++)
        {
            tier3Items[i].itemId = i;
            tier3Items[i].itemSprite = tier3Images[i];
        }

    }

    void SetTier2Needs()
    {
        tier2Items[0].neededItems.Add(0);
        tier2Items[0].neededItems.Add(1);

        tier2Items[0].neededItemsItem.Add(tier1Items[0]);
        tier2Items[0].neededItemsItem.Add(tier1Items[1]);

        tier2Items[1].neededItems.Add(4);
        tier2Items[1].neededItems.Add(2);

        tier2Items[1].neededItemsItem.Add(tier1Items[4]);
        tier2Items[1].neededItemsItem.Add(tier1Items[2]);

        tier2Items[2].neededItems.Add(5);
        tier2Items[2].neededItems.Add(0);

        tier2Items[2].neededItemsItem.Add(tier1Items[5]);
        tier2Items[2].neededItemsItem.Add(tier1Items[0]);

        tier2Items[3].neededItems.Add(6);
        tier2Items[3].neededItems.Add(8);

        tier2Items[3].neededItemsItem.Add(tier1Items[6]);
        tier2Items[3].neededItemsItem.Add(tier1Items[8]);

        tier2Items[4].neededItems.Add(14);
        tier2Items[4].neededItems.Add(7);

        tier2Items[4].neededItemsItem.Add(tier1Items[14]);
        tier2Items[4].neededItemsItem.Add(tier1Items[7]);

        tier2Items[5].neededItems.Add(9);
        tier2Items[5].neededItems.Add(10);

        tier2Items[5].neededItemsItem.Add(tier1Items[9]);
        tier2Items[5].neededItemsItem.Add(tier1Items[10]);

        tier2Items[6].neededItems.Add(11);
        tier2Items[6].neededItems.Add(12);

        tier2Items[6].neededItemsItem.Add(tier1Items[11]);
        tier2Items[6].neededItemsItem.Add(tier1Items[12]);

        tier2Items[7].neededItems.Add(13);
        tier2Items[7].neededItems.Add(18);

        tier2Items[7].neededItemsItem.Add(tier1Items[13]);
        tier2Items[7].neededItemsItem.Add(tier1Items[18]);

        tier2Items[8].neededItems.Add(15);
        tier2Items[8].neededItems.Add(16);

        tier2Items[8].neededItemsItem.Add(tier1Items[15]);
        tier2Items[8].neededItemsItem.Add(tier1Items[16]);

        tier2Items[9].neededItems.Add(17);
        tier2Items[9].neededItems.Add(19);

        tier2Items[9].neededItemsItem.Add(tier1Items[17]);
        tier2Items[9].neededItemsItem.Add(tier1Items[19]);

    }

    void SetTier3Needs()
    {
        tier3Items[0].neededItems.Add(6);
        tier3Items[0].neededItems.Add(7);

        tier3Items[0].neededItemsItem.Add(tier2Items[6]);
        tier3Items[0].neededItemsItem.Add(tier2Items[7]);

        tier3Items[1].neededItems.Add(9);
        tier3Items[1].neededItems.Add(3);

        tier3Items[1].neededItemsItem.Add(tier2Items[9]);
        tier3Items[1].neededItemsItem.Add(tier2Items[3]);

        tier3Items[2].neededItems.Add(8);
        tier3Items[2].neededItems.Add(5);

        tier3Items[2].neededItemsItem.Add(tier2Items[8]);
        tier3Items[2].neededItemsItem.Add(tier2Items[5]);

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

    public void RefreshItemSlot(int tier, int slotId)
    {
        if(tier == 1)
        {
            Debug.Log("child: " + itemSlotsTier1[slotId].transform.GetChild(0).name);
            Debug.Log("child: " + itemSlotsTier1[slotId].transform.GetChild(1).name);
            TMP_Text countText = itemSlotsTier1[slotId].transform.Find("ItemCount").GetComponent<TMP_Text>();
            countText.text = tier1ItemCounts[slotId].ToString();
            Image slotImage = itemSlotsTier1[slotId].transform.Find("Item").GetComponent<Image>();
            //Debug.Log("tier " + tier + "  slot: " + slotId + " - count: "+ tier1ItemCounts[slotId]);
            if (tier1ItemCounts[slotId] <= 0)
                slotImage.color = blackColor;
            else
                slotImage.color = fullColor;
        }
        else if (tier == 2)
        {
            TMP_Text countText = itemSlotsTier2[slotId].transform.Find("ItemCount").GetComponent<TMP_Text>();
            countText.text = tier2ItemCounts[slotId].ToString();
            Image slotImage = itemSlotsTier2[slotId].transform.Find("Item").GetComponent<Image>();

            //Debug.Log("tier " + tier + "  slot: " + slotId + " - count: " + tier2ItemCounts[slotId]);
            if (tier2ItemCounts[slotId] <= 0)
                slotImage.color = blackColor;
            else
                slotImage.color = fullColor;
        }
        else if (tier == 3)
        {
            TMP_Text countText = itemSlotsTier3[slotId].transform.Find("ItemCount").GetComponent<TMP_Text>();
            countText.text = tier3ItemCounts[slotId].ToString();
            Image slotImage = itemSlotsTier3[slotId].transform.Find("Item").GetComponent<Image>();

            //Debug.Log("tier " + tier + "  slot: " + slotId + " - count: " + tier3ItemCounts[slotId]);
            if (tier3ItemCounts[slotId] <= 0)
                slotImage.color = blackColor;
            else
                slotImage.color = fullColor;
        }
    }

    public void RefreshUnlock(Item newItem)
    {
        if (newItem.itemTier == 2)
        {
            int itemId = newItem.itemId;
            tier2Unlocks[itemId] = true;
            itemSlotsTier2[itemId].transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().color = fullColor;

            for (int i = 0; i < newItem.neededItems.Count; i++)
            {
                int id = newItem.neededItems[i];
                itemSlotsTier1[id].transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().color = fullColor;
            }
        }
        else if (newItem.itemTier == 3)
        {
            int itemId = newItem.itemId;
            tier3Unlocks[itemId] = true;
            itemSlotsTier3[itemId].transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().color = fullColor;

            for (int i = 0; i < newItem.neededItems.Count; i++)
            {
                int id = newItem.neededItems[i];
                itemSlotsTier2[id].transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Image>().color = fullColor;
            }
        }

    }

    public void AddItemToCount(int tier, int id, Item newItem)
    {
        newItem.item1Knowledge = true;
        newItem.item2Knowledge = true;

        if (tier == 2)
        {
            tier2ItemCounts[id] = tier2ItemCounts[id] + 1;
            ItemManager.instance.RefreshItemSlot(2, id);

        }
        if (tier == 3)
        {
            tier3ItemCounts[id] = tier3ItemCounts[id] + 1;
            ItemManager.instance.RefreshItemSlot(3, id);
        }
    }

    public Item GainRandomKnowledge(int tier)
    {
        List<Item> itemsToSearch;

        if (tier == 2)
        {
            itemsToSearch = tier2Items;
        }
        else if (tier == 3)
        {
            itemsToSearch = tier3Items;
        }
        else
        {
            return null; // Handle other tiers if necessary
        }

        // Filter items where either item1Knowledge or item2Knowledge is false
        var filteredItems = itemsToSearch.Where(item => !item.item1Knowledge || !item.item2Knowledge).ToList();

        if (filteredItems.Count == 0)
        {
            // No items found, return null or handle as needed
            return null;
        }

        // Randomly select an item from the filtered list
        var random = new System.Random();
        int index = random.Next(filteredItems.Count);
        var selectedItem = filteredItems[index];
        int selectedKnowledge = -1;

        // Update item1Knowledge or item2Knowledge to true
        if (!selectedItem.item1Knowledge)
        {
            Debug.Log("Set knowledge to true for: " + selectedItem.itemName + "- need 1");
            selectedItem.item1Knowledge = true;
            selectedKnowledge = 0;
        }
        else if (!selectedItem.item2Knowledge)
        {
            Debug.Log("Set knowledge to true for: " + selectedItem.itemName + "- need 2");
            selectedItem.item2Knowledge = true;
            selectedKnowledge = 1;
        }

        // Return the modified item
        LogTextManager.instance.ReceiveKnowledgeData(selectedItem, selectedKnowledge);
        return selectedItem;
    }

}


[System.Serializable]
public class Item
{
    public string itemName;
    public List<int> neededItems = new List<int> {};
    public List<Item> neededItemsItem = new List<Item> { };

    public bool item1Knowledge = false;
    public bool item2Knowledge = false;

    public int itemTier;
    public string infoText = "Placeholder text for flavourful story and information about this item about what does it do and so on. Doesn't really give anything insightful gameplay wise.";
    public int itemId;
    public Sprite itemSprite;

    // Default constructor
    public Item()
    {
        itemName = "";
        itemId = -1;
        neededItems = new List<int> {};
        itemTier = 1;
        infoText = "";
    }

    public Item(string itemName, int itemTier, string flavourText)
    {
        this.itemName = itemName;
        this.itemTier = itemTier;
        this.infoText = flavourText;
    }
}