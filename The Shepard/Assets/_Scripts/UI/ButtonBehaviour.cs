using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image buttonImage;

    public Sprite[] buttonSprites;
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = buttonSprites[1];
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = buttonSprites[0];
    }
}
