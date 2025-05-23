using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public int enemyHealth = 10;
    public int enemyMaxHealth = 10;

    public int playerHealth = 10;
    public int playerMaxHealth = 10;
    public int attackDamage = 5;
    public List<Item> currentEquipments = new List<Item> { };
    public int currentBiome = 0;
    public int itemDropChanceDefault = 15;
    public int knowledgeDropChanceDefault = 25;

    public float enemyAttackTimer = 0;

    public float enemyAttackInterval = 7;
    public int attackInterval = 8;
    int attackIntervalDefault = 8;

    public float attackTimer = 0;
    public bool combatActive = false;

    public int itemDropChance = 15;
    public int knowledgeDropChance = 25;

    public int enemyDamage = 4;
    public int enemyDefaultDamage = 4;

    public EquipmentSlotsController[] equipmentSlotControllers;

    public TMP_Text biomeText;
    public TMP_Text attackIntervalText;
    public TMP_Text itemDropChanceText;
    public TMP_Text knowledgeDropChanceText;
    public TMP_Text enemyDamageText;

    public TMP_Text playerHealthText;
    public TMP_Text enemyHealthText;

    public Slider attackTimerSlider;
    public Slider enemyAttackTimerSlider;
    public Slider enemyHealthSlider;
    public Slider playerHealthSlider;



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
        currentBiome = 0;
        biomeImage.sprite = biomeSprites[currentBiome];
        biomeText.text = GetBiomeName(currentBiome);

        enemyMaxHealth = 10;
        attackInterval = attackIntervalDefault;
        attackDamage = 5;

        enemyHealth = 0;
        enemyHealthSlider.value = 0;

        RefreshTextValues();

        attacking = false;
        combatImage.sprite = combatIcons[1];

        ResetPlayerHealth();

    }

    public void RefreshEquipments()
    {
        attackInterval = attackIntervalDefault;
        itemDropChance = itemDropChanceDefault;
        knowledgeDropChance = knowledgeDropChanceDefault;
        enemyDamage = enemyDefaultDamage;
        if (currentBiome > 0)
            enemyDamage += 4;

        if (currentBiome == 4)
            enemyDamage = 20;

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
                else if (currentEquipments[i].buffType == 3) // enemy damage debuff
                {
                    enemyDamage -= currentEquipments[i].buffValue;
                }
            }
        }

        if (attackInterval < 1)
            attackInterval = 1;

        RefreshTextValues();

    }

    public void ChangeBiome(int newBiome)
    {
        AudioManager.instance.PlayBasicClick();
        if (GetAttacking())
        {
            InfoTextPopupManager.instance.SpawnInfoTextPopup("Can't change biome while fighting");
            return;
        }

        currentBiome = newBiome;
        biomeImage.sprite = biomeSprites[currentBiome];
        biomeText.text = GetBiomeName(currentBiome);

        RefreshEquipments();
    }

    void RefreshTextValues()
    {
        attackIntervalText.text = "ATK: "+ attackInterval.ToString() + "s";
        enemyDamageText.text = "Enemy damage: " + enemyDamage.ToString() + "";
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
            AudioManager.instance.PlayToggleCombat();
        }
        else
        {
            enemyHealth = 0;
            enemyHealthSlider.value = enemyHealthSlider.maxValue;
            attacking = false;

            attackTimer = 0;
            attackTimerSlider.value = 0;
            enemyAttackTimer = 0;
            enemyAttackTimerSlider.value = 0;

            combatImage.sprite = combatIcons[1];
            
        }
    }

    void SetEnemy()
    {
        ResetPlayerHealth();

        enemyHealth = enemyMaxHealth;
        RefreshEnemyHealth();

        enemyAttackTimer = 0;
    }

    void RefreshEnemyHealth()
    {
        enemyHealthSlider.value = Tools.instance.NormalizeToSlider(enemyHealth, enemyMaxHealth);
        enemyHealthText.text = enemyHealth.ToString()+ "/" + enemyMaxHealth.ToString();
    }

    void RefreshPlayerHealth()
    {
        playerHealthSlider.value = Tools.instance.NormalizeToSlider(playerHealth, playerMaxHealth);
        playerHealthText.text = playerHealth.ToString() + "/" + playerMaxHealth.ToString();
    }

    void ResetPlayerHealth()
    {
        playerHealth = playerMaxHealth;
        RefreshPlayerHealth();
    }

    private void Update()
    {
        if (!attacking)
            return;

        enemyAttackTimer += Time.deltaTime;
        enemyAttackTimerSlider.value = Tools.instance.NormalizeToSlider(enemyAttackTimer, enemyAttackInterval);

        attackTimer += Time.deltaTime;
        attackTimerSlider.value = Tools.instance.NormalizeToSlider(attackTimer, attackInterval);

        if(attackTimer >= attackInterval)
        {
            attackTimer = 0;
            DoAttack();
        }

        if (enemyAttackTimer >= enemyAttackInterval)
        {
            enemyAttackTimer = 0;
            EnemyAttacks();
        }
    }

    void DoAttack()
    {
        AudioManager.instance.PlayPlayerDealsDamage();
        enemyHealth -= attackDamage;
        RefreshEnemyHealth();
        if (enemyHealth <= 0)
        {
            EnemyDestroyed();
        }
    }

    void EnemyAttacks()
    {
        AudioManager.instance.PlayPlayerTakesDamage();
        playerHealth -= enemyDamage;
        RefreshPlayerHealth();
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            RefreshPlayerHealth();
            PlayerFailed();
        }
    }

    void EnemyDestroyed()
    {
        AudioManager.instance.PlayCombatFinished();

        if(currentBiome == 4 && !GameMasterManager.instance.gameFinished)
        {
            GameMasterManager.instance.GameFinished();
        }
        else if(currentBiome < 4)
        {
            GainLoot();
            FightNewEnemy();
        }
        else
        {
            FightNewEnemy();
        }
    }

    void PlayerFailed()
    {
        InfoTextPopupManager.instance.SpawnInfoTextPopup("You had to retreat from combat");
        LogTextManager.instance.AddSpecificHintToLog("You ran from the fight");

        AudioManager.instance.PlayTransmuteFailed();
        AudioManager.instance.PlayPlayerDies();

        ToggleAttacking();
    }


    void FightNewEnemy()
    {
        SetEnemy();
    }

    void GainLoot()
    {
        if (currentBiome == 4)
            return;

        List<Item> possibleItems = ItemManager.instance.GetItemsOfBiome(currentBiome);

        bool extraLoot = false;
        List<Item> itemLoot = new List<Item> { };
        for (int i = 0; i < 3; i++)
        {
            itemLoot.Add(possibleItems[UnityEngine.Random.Range(0, possibleItems.Count)]);
        }

        int itemDropRandom = UnityEngine.Random.Range(0, 100);
        if(itemDropRandom < itemDropChance)
        {
            itemLoot.Add(possibleItems[UnityEngine.Random.Range(0, possibleItems.Count)]);
            extraLoot = true;
        }

        int knowledgeDropRandom = UnityEngine.Random.Range(0, 100);
        if (knowledgeDropRandom < knowledgeDropChance)
            ItemManager.instance.GainRandomKnowledgeByBiome(currentBiome);

        string lootText = "LOOT: ";
        int maxCount = itemLoot.Count;
        if (extraLoot)
            maxCount -= 1;

        for (int i = 0; i < itemLoot.Count; i++)
            ItemManager.instance.AddItemToCount(itemLoot[i]);

        for (int i = 0; i < maxCount; i++)
        {
            lootText = lootText + itemLoot[i].itemName;
            if (i < maxCount - 1)
                lootText += ", ";
        }

        LogTextManager.instance.AddSpecificHintToLog(lootText);

        if(extraLoot)
            LogTextManager.instance.AddSpecificHintToLog("EXTRA LOOT: "+itemLoot[itemLoot.Count-1].itemName);
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
        else if (id == 4)
            return "Shadow Realm";
        else
            return "";
    }
}
