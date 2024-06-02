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


    [Header("Debug")]
    public Color empty;
    public Color hover;
    public Color pressed;

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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CosmeticManager.Instance.ReturnToOriginalDisplay(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CosmeticManager.Instance.SelectedSlot(gameObject);
    }
}
