using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string optionalText = "";

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(optionalText == "")
        {
            if (this.tag == "Item")
            {
                string text = GetComponent<ItemController>().GetItemName();
                Tooltip.ShowTooltip_Static(text);
            }
            else
            {
                Tooltip.HideTooltip_Static();
            }

            
        }
        else
        {
            Tooltip.ShowTooltip_Static(optionalText);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.HideTooltip_Static();
    }

}
