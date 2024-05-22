using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CosmeticSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Cosmetics")]
    public Sprite cosmetic;
    public bool selected;

    [Header("Debug")]
    public Color empty;
    public Color hover;
    public Color pressed;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<Image>().color = hover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<Image>().color = empty;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponentInChildren<Image>().color = pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponentInChildren<Image>().color = hover;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CosmeticManager.Instance.ChangeCosmetic(cosmetic);
        CosmeticManager.Instance.SelectedSlot(gameObject);
    }
}
