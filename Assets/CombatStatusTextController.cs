using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CombatStatusTextController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string optionalText = "";
    public GameObject combatStatusPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        combatStatusPanel.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        combatStatusPanel.SetActive(false);
    }
}