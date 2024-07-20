using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BigTooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string optionalText = "";

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (optionalText == "")
        {
            if (this.tag == "Item")
            {
                Item item = GetComponent<ItemController>().GetItem();
                BigTooltip.ShowItemTooltip_Static(item);
            }
            else
            {
                BigTooltip.HideTooltip_Static();
            }


        }
        else
        {
           // BigTooltip.ShowTooltip_Static(optionalText);
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BigTooltip.HideTooltip_Static();
    }

}
