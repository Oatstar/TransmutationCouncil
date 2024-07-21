using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public List<Item> currentEquipments = new List<Item> { };
    public int currentBiome = 0;
    public int attackIntervalDefault = 10;
    public int itemDropChanceDefault = 15;
    public int knowledgeDropChanceDefault = 15;

    public float attackTimer = 0;

    public int attackInterval = 10;
    public int itemDropChance = 15;
    public int knowledgeDropChance = 15;

    public EquipmentSlotsController[] equipmentSlotControllers;

    public TMP_Text biomeText;
    public TMP_Text attackIntervalText;
    public TMP_Text itemDropChanceText;
    public TMP_Text knowledgeDropChanceText;

    public static CombatManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeBiome(0);
        RefreshTextValues();
    }

    public void RefreshEquipments()
    {
        attackInterval = attackIntervalDefault;
        itemDropChance = itemDropChanceDefault;
        knowledgeDropChance = knowledgeDropChanceDefault;

        for (int i = 0; i < equipmentSlotControllers.Length; i++)
        {
            if(equipmentSlotControllers[i].currentItem != null)
                currentEquipments.Add(equipmentSlotControllers[i].currentItem);
        }

        for (int i = 0; i < currentEquipments.Count; i++)
        {
            if(currentEquipments[i].buffBiome == currentBiome)
            {
                if(currentEquipments[i].buffType == 0) //item drop chance
                {
                    itemDropChance += currentEquipments[i].buffValue;
                }
                else if (currentEquipments[i].buffType == 1) //knowledge drop chance
                {
                    knowledgeDropChance += currentEquipments[i].buffValue;
                }
                else if (currentEquipments[i].buffType == 2) // attack speed
                {
                    attackInterval -= currentEquipments[i].buffValue;
                }
            }
        }
    }

    public void ChangeBiome(int newBiome)
    {
        currentBiome = newBiome;
        biomeText.text = GetBiomeName(currentBiome);
    }

    void RefreshTextValues()
    {
        attackIntervalText.text = "ATK: "+ attackInterval.ToString() + "s";
        itemDropChanceText.text = "Extra item chance: "+itemDropChance.ToString() + "%";
        knowledgeDropChanceText.text = "Knowledge chance: " + knowledgeDropChance.ToString() + "%";
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackInterval)
        {
            attackTimer = 0;
            DoAttack();
        }
    }

    void DoAttack()
    {
        GainLoot();
    }

    void GainLoot()
    {
        List<Item> possibleItems = ItemManager.instance.GetItemsOfBiome(currentBiome);

        List<Item> itemLoot = new List<Item> { };
        for (int i = 0; i < 3; i++)
        {
            itemLoot.Add(possibleItems[UnityEngine.Random.Range(0, possibleItems.Count)]);
        }

        int itemDropRandom = UnityEngine.Random.Range(0, 100);
        if(itemDropRandom < itemDropChance)
            itemLoot.Add(possibleItems[UnityEngine.Random.Range(0, possibleItems.Count)]);

        int knowledgeDropRandom = UnityEngine.Random.Range(0, 100);
        if (knowledgeDropRandom < knowledgeDropChance)
            ItemManager.instance.GainRandomKnowledgeByBiome(currentBiome);

        for (int i = 0; i < itemLoot.Count; i++)
        {
            LogTextManager.instance.AddSpecificHintToLog("LOOT: "+itemLoot[i].itemName);
            ItemManager.instance.AddItemToCount(itemLoot[0]);
        }


    }

    public bool CanModifyCombat()
    {
        if (attackTimer > 3)
            return false;
        else
            return true;
    }

    public string GetBiomeName(int id)
    {

        if (id == 0)
            return "Infernal Wastes";
        else if (id == 1)
            return "Crystal Caverns";
        else if (id == 2)
            return "Enchanted Forest";
        else if (id == 3)
            return "Frozen Tundra";
        else
            return "";
    }
}
