using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CosmeticSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [Header("Cosmetics")]
    public Sprite cosmeticSprite;
    public GameObject cosmeticIcon;
    public GameObject slotIcon;
    public bool selected;

    public bool unlocked;
    public CosmeticName cosmeticName;
    public CosmeticType cosmeticType;
    public string missionDescription;
    public string cosmeticDescription;

    public Color hoverColour;
    public Color selectedColor;
    public Sprite unpressedButton;
    public Sprite pressedButton;
    private void Start()
    {
        unlocked = CosmeticManager.Instance.allCosmetics[cosmeticName];
    }

    private void OnEnable()
    {
        if (unlocked)
        {
            cosmeticIcon.GetComponent<Image>().color = Color.white;
            slotIcon.GetComponent<Image>().color = Color.white;

            if (selected)
            {
                slotIcon.GetComponent<Image>().sprite = pressedButton;
                slotIcon.GetComponent<Image>().color = selectedColor;
            }
            else
            {
                slotIcon.GetComponent <Image>().sprite = unpressedButton;
                slotIcon.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            cosmeticIcon.GetComponent<Image>().color = Color.black;
            slotIcon.GetComponent<Image>().color = Color.gray;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CosmeticManager.Instance.SetCosmeticInfo(gameObject);
        CosmeticManager.Instance.ShowOnDisplay(gameObject);

        if (unlocked)
        {
            slotIcon.GetComponent<Image>().color = hoverColour;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CosmeticManager.Instance.ReturnToOriginalDisplay(gameObject);

        if (unlocked)
        {
            if (selected)
            {
                slotIcon.GetComponent<Image>().sprite = pressedButton;
                slotIcon.GetComponent<Image>().color = selectedColor;
            }
            else
            {
                slotIcon.GetComponent<Image>().sprite = unpressedButton;
                slotIcon.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (unlocked)
        {
            CosmeticManager.Instance.SelectedSlot(gameObject);
            if (selected)
            {
                slotIcon.GetComponent<Image>().sprite = pressedButton;
            }
            else
            {
                slotIcon.GetComponent<Image>().sprite = unpressedButton;
            }
        }
    }
}
