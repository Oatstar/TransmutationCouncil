using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BigTooltip : MonoBehaviour
{
    TMP_Text tooltipText;
    RectTransform backgroundRectTransform;
    RectTransform thisRectTransform;

    //public Image itemImage;
    public Image neededItem1Image;
    public Image neededItem2Image;
    public TMP_Text itemNameText;
    public TMP_Text itemFlavourText;
    public TMP_Text neededItem1NameText;
    public TMP_Text neededItem2NameText;
    public TMP_Text targetBiomeText;
    public TMP_Text buffInformation;

    public Image lootableBiomeImage;
    public Image targetBuffBiomeImage;

    public Sprite[] biomeIcons;

    public Canvas canvas;
    //[SerializeField] private Camera uiCamera;

    private static BigTooltip instance;

    private void Awake()
    {
        instance = this;

        // = transform.Find("TooltipBG").GetComponent<RectTransform>();
        thisRectTransform = GetComponent<RectTransform>();

        HideToolTip();
    }

    private void Start()
    {
        //ShowToolTip("");
        HideToolTip();
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 movePos;
        Vector2 offset = new Vector2(0, 0);

        // Get the width of the screen
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Get the width of the tooltip rect
        float tooltipWidth = thisRectTransform.sizeDelta.x;
        float tooltipHeight = thisRectTransform.sizeDelta.y;

        // Determine the offset based on the mouse position
        if (mousePosition.x < screenWidth / 2)
        {
            // Mouse is on the left side, offset to the right
            offset.x = tooltipWidth / 2;
        }
        else
        {
            // Mouse is on the right side, offset to the left
            offset.x = -tooltipWidth / 2;
        }
        if (mousePosition.y < screenHeight / 2)
        {
            // Mouse is on the bottom side, offset to the top
            offset.y = tooltipHeight / 2;
        }
        else
        {
            // Mouse is on the top side, offset to the bottom
            offset.y = -tooltipHeight / 2;
        }

        // Convert mouse position to local position in the canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out movePos);

        // Convert offset to world coordinates and add it to movePos
        Vector3 worldOffset = canvas.transform.TransformPoint(offset) - canvas.transform.TransformPoint(Vector3.zero);
        Vector3 worldPos = canvas.transform.TransformPoint(movePos) + worldOffset;

        // Set the position of the tooltip
        thisRectTransform.position = worldPos;

    }

    private void ShowToolTip(Item tooltipItem)
    {

        //tooltipText.text = tooltipString;
        //float padding = 4f;
        //Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + padding * 2f, tooltipText.preferredHeight + padding * 2f);
        //thisRectTransform.sizeDelta = backgroundSize;
        //itemImage.sprite = tooltipItem.itemSprite;
        itemNameText.text = tooltipItem.itemName;
        itemFlavourText.text = tooltipItem.infoText;
        targetBiomeText.text = "";

        lootableBiomeImage.color = new Color(1, 1, 1, 0);

        if (tooltipItem.itemTier == 2 || tooltipItem.itemTier == 3)
        {
            Item neededItem1 = null;
            Item neededItem2 = null;

            neededItem1 = tooltipItem.neededItemsItem[0];
            neededItem2 = tooltipItem.neededItemsItem[1];

            if (tooltipItem.itemTier != 1)
            {
                if (tooltipItem.item1Knowledge)
                {
                    neededItem1NameText.text = neededItem1.itemName;
                    neededItem1Image.sprite = neededItem1.itemSprite;
                    neededItem1Image.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    neededItem1NameText.text = "?";
                    neededItem1Image.sprite = neededItem1.itemSprite;
                    neededItem1Image.color = new Color(1, 1, 1, 0);
                }
                if (tooltipItem.item2Knowledge)
                {
                    neededItem2NameText.text = neededItem2.itemName;
                    neededItem2Image.sprite = neededItem2.itemSprite;
                    neededItem2Image.color = new Color(1, 1, 1, 1);
                }
                else
                {
                    neededItem2NameText.text = "?";
                    neededItem2Image.sprite = neededItem2.itemSprite;
                    neededItem2Image.color = new Color(1, 1, 1, 0);
                }
            }

            
        }
        else
        {
            if (tooltipItem.itemTier == 1)
            {
                targetBiomeText.text = "Found in: " + CombatManager.instance.GetBiomeName(tooltipItem.lootableBiome);
                lootableBiomeImage.sprite = biomeIcons[tooltipItem.lootableBiome];
                lootableBiomeImage.color = new Color(1, 1, 1, 1);
            }

            neededItem1NameText.text = "";
            neededItem2NameText.text = "";
            neededItem1Image.color = new Color(1, 1, 1, 0);
            neededItem2Image.color = new Color(1, 1, 1, 0);
        }

        buffInformation.text = ItemManager.instance.GetBuffText(tooltipItem);
        targetBuffBiomeImage.sprite = biomeIcons[tooltipItem.buffBiome];

        gameObject.SetActive(true);
    }

    private void HideToolTip()
    {
        gameObject.SetActive(false);
        //tooltipText.text = "  ";
        //float padding = 4f;
        //Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + padding * 2f, tooltipText.preferredHeight + padding * 2f);
        //thisRectTransform.sizeDelta = backgroundSize;
    }


    //public static void ShowTooltip_Static(string tooltipString)
    //{
    //    instance.ShowToolTip(tooltipString);
    //    instance.CancelInvoke("HideOnDelay");
    //}

    public static void ShowItemTooltip_Static(Item tooltipItem)
    {
        instance.ShowToolTip(tooltipItem);
        instance.CancelInvoke("HideOnDelay");
    }

    public static void HideTooltip_Static()
    {
        instance.Invoke("HideOnDelay", 0.05f);
    }

    void HideOnDelay()
    {
        instance.HideToolTip();
    }
}
