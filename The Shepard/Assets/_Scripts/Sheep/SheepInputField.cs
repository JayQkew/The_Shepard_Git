using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SheepInputField : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerActions.Instance.hoverOverUI = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayerActions.Instance.hoverOverUI = false;
    }

    
}
