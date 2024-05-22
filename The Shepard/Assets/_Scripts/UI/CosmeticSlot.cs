using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CosmeticSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Cosmetics")]
    public CosmeticName cosmeticName;
    public Sprite cosmetic;
    public GameObject cosmeticIcon;
    public bool selected;

    [Header("Debug")]
    public Color empty;
    public Color hover;
    public Color pressed;

    private void Start()
    {
        cosmeticIcon.GetComponent<Image>().sprite = cosmetic;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CosmeticManager.Instance.unlockedCosmetics[cosmeticName] == true)
        {
            GetComponentInChildren<Image>().color = hover;
        }
        else
        {
            //dont change colour as much
            GetComponentInChildren<Image>().color = empty;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<Image>().color = empty;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CosmeticManager.Instance.unlockedCosmetics[cosmeticName] == true)
        {
            GetComponentInChildren<Image>().color = pressed;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (CosmeticManager.Instance.unlockedCosmetics[cosmeticName] == true)
        {
            GetComponentInChildren<Image>().color = hover;
        }
        else
        {
            //dont change colour as much
            GetComponentInChildren<Image>().color = empty;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CosmeticManager.Instance.unlockedCosmetics[cosmeticName] == true)
        {
            CosmeticManager.Instance.ChangeCosmetic(cosmetic);
            CosmeticManager.Instance.SelectedSlot(gameObject);
        }
        else
        {
            //play animation (shake)
        }
    }
}
