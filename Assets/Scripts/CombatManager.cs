using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public int enemyHealth = 10;
    public int enemyMaxHealth = 10;
    public int attackDamage = 5;
    public List<Item> currentEquipments = new List<Item> { };
    public int currentBiome = 0;
    public int attackIntervalDefault = 10;
    public int itemDropChanceDefault = 15;
    public int knowledgeDropChanceDefault = 15;

    public float attackTimer = 0;
    public bool combatActive = false;

    public int attackInterval = 8;
    public int itemDropChance = 15;
    public int knowledgeDropChance = 15;

    public EquipmentSlotsController[] equipmentSlotControllers;

    public TMP_Text biomeText;
    public TMP_Text attackIntervalText;
    public TMP_Text itemDropChanceText;
    public TMP_Text knowledgeDropChanceText;

    public Slider attackTimerSlider;
    public Slider enemyHealthSlider;

    public Sprite[] combatIcons;
    public Image combatImage;


    bool attacking = false;

    public Sprite[] biomeSprites;
    public Image biomeImage;


    public static CombatManager instance;

    public bool GetAttacking()
    {
        return attacking;
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        enemyMaxHealth = 10;
        attackInterval = 9;
        attackIntervalDefault = 9;
        attackDamage = 5;

        ChangeBiome(0);
        enemyHealth = 0;
        enemyHealthSlider.value = 0;

        RefreshTextValues();

        attacking = false;
        combatImage.sprite = combatIcons[0];
    }

    public void RefreshEquipments()
    {
        attackInterval = attackIntervalDefault;
        itemDropChance = itemDropChanceDefault;
        knowledgeDropChance = knowledgeDropChanceDefault;

        currentEquipments.Clear();
        for (int i = 0; i < equipmentSlotControllers.Length; i++)
        {
            if(equipmentSlotControllers[i].currentItem != null && equipmentSlotControllers[i].currentItem.itemName != "")
            {
                currentEquipments.Add(equipmentSlotControllers[i].currentItem);
            }
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

        RefreshTextValues();

    }

    public void ChangeBiome(int newBiome)
    {
        if (CombatManager.instance.GetAttacking())
        {
            InfoTextPopupManager.instance.SpawnInfoTextPopup("Can't change biome while fighting");
            return;
        }

        currentBiome = newBiome;
        biomeImage.sprite = biomeSprites[currentBiome];
        biomeText.text = GetBiomeName(currentBiome);
    }

    void RefreshTextValues()
    {
        attackIntervalText.text = "ATK: "+ attackInterval.ToString() + "s";
        itemDropChanceText.text = "Extra item chance: "+itemDropChance.ToString() + "%";
        knowledgeDropChanceText.text = "Knowledge chance: " + knowledgeDropChance.ToString() + "%";
    }

    public void ToggleAttacking()
    {
        if(!attacking)
        {
            SetEnemy();
            attacking = true;
            combatImage.sprite = combatIcons[0];

        }
        else
        {
            enemyHealth = 0;
            attacking = false;
            combatImage.sprite = combatIcons[1];
        }
    }

    void SetEnemy()
    {
        enemyHealth = enemyMaxHealth;
        enemyHealthSlider.value = Tools.instance.NormalizeToSlider(enemyHealth, enemyMaxHealth);
    }

    private void Update()
    {
        if (!attacking)
            return;

        attackTimer += Time.deltaTime;
        attackTimerSlider.value = Tools.instance.NormalizeToSlider(attackTimer, attackInterval);

        if(attackTimer >= attackInterval)
        {
            attackTimer = 0;
            DoAttack();
        }
    }

    void DoAttack()
    {
        enemyHealth -= attackDamage;
        enemyHealthSlider.value = Tools.instance.NormalizeToSlider(enemyHealth, enemyMaxHealth);
        if(enemyHealth <= 0)
        {
            EnemyDestroyed();
        }
    }

    void EnemyDestroyed()
    {
        GainLoot();
        FightNewEnemy();
    }

    void FightNewEnemy()
    {
        SetEnemy();
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
