using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewItemFlasherController : MonoBehaviour
{
    Image thisImage;

    public static NewItemFlasherController instance;


    private void Awake()
    {
        instance = this;
        thisImage = GetComponent<Image>();
    }

    public void StartFade(Item newItem)
    {
        // Ensure the image starts fully transparent
        thisImage.color = new Color(1, 1, 1, 0);
        // Set the sprite to the new item's sprite
        Sprite newItemSprite = newItem.itemSprite;
        thisImage.sprite = newItemSprite;
        // Make the image fully opaque
        thisImage.color = new Color(1, 1, 1, 1);
        // Start the fading coroutine
        StartCoroutine(DoFading());
    }

    IEnumerator DoFading()
    {
        float duration = 1.0f;
        float elapsedTime = 0f;
        Color startColor = thisImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            thisImage.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
            yield return null;
        }
        // Ensure the final color is set to fully transparent
        thisImage.color = endColor;
    }
}