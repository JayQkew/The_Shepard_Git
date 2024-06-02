using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CosmeticManager : MonoBehaviour
{
    public static CosmeticManager Instance { get; private set; }

    public Dictionary<CosmeticName, bool> allCosmetics = new Dictionary<CosmeticName, bool>
    {
        {CosmeticName.Eyelashes,    true },
        {CosmeticName.Glasses,      false },
        {CosmeticName.Anime,        false },
        {CosmeticName.Scarf,        false },
        {CosmeticName.Bandana,      false },
        {CosmeticName.Bowtie,       false },
        {CosmeticName.Frog,         false },
        {CosmeticName.Ducken,       false },
        {CosmeticName.Noot,         false }
    };

    public Dictionary<CosmeticType, CosmeticName> equiptCosmetics = new Dictionary<CosmeticType, CosmeticName>
    {
        {CosmeticType.Head, CosmeticName.None },
        {CosmeticType.Body, CosmeticName.None },
        {CosmeticType.Back, CosmeticName.None }
    };

    [Header("Player Cosmetics")]
    public GameObject playerHeadCosmetic;
    public GameObject playerBodyCosmetic;
    public GameObject playerBackCosmetic;

    [Header("Player Display")]
    public GameObject headDisplay;
    public GameObject bodyDisplay;
    public GameObject backDisplay;

    [Header("Cosmetic Description")]
    public TextMeshProUGUI cosmeticName;
    public TextMeshProUGUI cosmeticDescription;

    [Space(15)]
    public GameObject[] slots;

    public bool Debug;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetSlots();

        if (Debug)
        {
            allCosmetics = new Dictionary<CosmeticName, bool>
            {
                {CosmeticName.Eyelashes,    true },
                {CosmeticName.Glasses,      true },
                {CosmeticName.Anime,        true },
                {CosmeticName.Scarf,        true },
                {CosmeticName.Bandana,      true },
                {CosmeticName.Bowtie,       true },
                {CosmeticName.Frog,         true },
                {CosmeticName.Ducken,       true },
                {CosmeticName.Noot,         true }
            };
        }
    }

    public void GetSlots()
    {
        slots = GameObject.FindGameObjectsWithTag("slots");
        foreach (GameObject slot in slots)
        {
            CosmeticSlot cosmeticSlot = slot.GetComponent<CosmeticSlot>();
            CosmeticName cosmetic = cosmeticSlot.cosmeticName;
            bool unlocked = cosmeticSlot.unlocked;

            if (!allCosmetics.ContainsKey(cosmeticSlot.cosmeticName))
            {
                allCosmetics.Add(cosmetic, unlocked);
            }
        }
    }

    public void SelectedSlot(GameObject slot)
    {
        CosmeticType cosType = slot.GetComponent<CosmeticSlot>().cosmeticType;
        foreach (GameObject s in slots)
        {
            if (s != slot && s.GetComponent<CosmeticSlot>().cosmeticType == cosType)
            {
                s.GetComponent<CosmeticSlot>().selected = false;
                s.GetComponent <CosmeticSlot>().slotIcon.GetComponent<Image>().sprite = s.GetComponent<CosmeticSlot>().unpressedButton;
                s.GetComponent <CosmeticSlot>().slotIcon.GetComponent<Image>().color = Color.white;
            }
            else if (s.GetComponent<CosmeticSlot>().cosmeticType == cosType)
            {
                s.GetComponent<CosmeticSlot>().selected = true;
                s.GetComponent<CosmeticSlot>().slotIcon.GetComponent<Image>().color = s.GetComponent<CosmeticSlot>().selectedColor;
            }
        }

        switch (cosType)
        {
            case CosmeticType.Head:
                if (equiptCosmetics[cosType] == slot.GetComponent<CosmeticSlot>().cosmeticName) UnEquiptItem(playerHeadCosmetic, headDisplay, slot);
                else EquiptItem(playerHeadCosmetic, headDisplay, slot);
                break;
            case CosmeticType.Body:
                if (equiptCosmetics[cosType] == slot.GetComponent<CosmeticSlot>().cosmeticName) UnEquiptItem(playerBodyCosmetic, bodyDisplay, slot);
                else EquiptItem(playerBodyCosmetic, bodyDisplay, slot);
                break;
            case CosmeticType.Back:
                if (equiptCosmetics[cosType] == slot.GetComponent<CosmeticSlot>().cosmeticName) UnEquiptItem(playerBackCosmetic, backDisplay, slot);
                else EquiptItem(playerBackCosmetic, backDisplay, slot);
                break;
        }
    }

    public void EquiptItem(GameObject cosmeticType, GameObject displayType, GameObject slot)
    {
        cosmeticType.GetComponent<SpriteRenderer>().sprite = slot.GetComponent<CosmeticSlot>().cosmeticSprite;
        displayType.GetComponent<Image>().color = Color.white;
        displayType.GetComponent<Image>().sprite = slot.GetComponent<CosmeticSlot>().cosmeticSprite;

        equiptCosmetics[slot.GetComponent<CosmeticSlot>().cosmeticType] = slot.GetComponent<CosmeticSlot>().cosmeticName;
    }

    public void UnEquiptItem(GameObject cosmeticType, GameObject displayType, GameObject slot)
    {
        cosmeticType.GetComponent<SpriteRenderer>().sprite = null;
        displayType.GetComponent<Image>().color = Color.clear;
        slot.GetComponent<CosmeticSlot>().slotIcon.GetComponent<Image>().sprite = slot.GetComponent<CosmeticSlot>().unpressedButton;
        slot.GetComponent<CosmeticSlot>().selected = false;

        equiptCosmetics[slot.GetComponent<CosmeticSlot>().cosmeticType] = CosmeticName.None;
    }

    public void SetCosmeticInfo(GameObject slot)
    {
        CosmeticName cosmetic = slot.GetComponent<CosmeticSlot>().cosmeticName;

        if (allCosmetics[cosmetic])
        {
            cosmeticName.text = slot.GetComponent<CosmeticSlot>().cosmeticName.ToString();
            cosmeticDescription.text = slot.GetComponent<CosmeticSlot>().cosmeticDescription;
        }
        else
        {
            cosmeticName.text = "...";
            cosmeticDescription.text = slot.GetComponent<CosmeticSlot>().missionDescription;
        }

    }

    public void ShowOnDisplay(GameObject slot)
    {
        CosmeticSlot cosmeticSlot = slot.GetComponent<CosmeticSlot>();

        if (allCosmetics[cosmeticSlot.cosmeticName])
        {
            switch (slot.GetComponent<CosmeticSlot>().cosmeticType)
            {
                case CosmeticType.Head:
                    headDisplay.GetComponent<Image>().sprite = cosmeticSlot.cosmeticSprite;
                    headDisplay.GetComponent<Image>().color = Color.white;
                    break;
                case CosmeticType.Body:
                    bodyDisplay.GetComponent<Image>().sprite = cosmeticSlot.cosmeticSprite;
                    bodyDisplay.GetComponent<Image>().color = Color.white;
                    break;
                case CosmeticType.Back:
                    backDisplay.GetComponent<Image>().sprite = cosmeticSlot.cosmeticSprite;
                    backDisplay.GetComponent<Image>().color = Color.white;
                    break;
            }
        }
    }

    public void ReturnToOriginalDisplay(GameObject slot)
    {
        CosmeticSlot cosmetic = slot.GetComponent<CosmeticSlot>();

        switch (cosmetic.cosmeticType)
        {
            case CosmeticType.Head:
                headDisplay.GetComponent<Image>().sprite = playerHeadCosmetic.GetComponent<SpriteRenderer>().sprite;
                if (equiptCosmetics[cosmetic.cosmeticType] == CosmeticName.None)
                {
                    headDisplay.GetComponent<Image>().color = Color.clear;
                }
                break;
            case CosmeticType.Body:
                bodyDisplay.GetComponent<Image>().sprite = playerBodyCosmetic.GetComponent<SpriteRenderer>().sprite;
                if (equiptCosmetics[cosmetic.cosmeticType] == CosmeticName.None)
                {
                    bodyDisplay.GetComponent<Image>().color = Color.clear;
                }
                break;
            case CosmeticType.Back:
                backDisplay.GetComponent<Image>().sprite = playerBackCosmetic.GetComponent<SpriteRenderer>().sprite;
                if (equiptCosmetics[cosmetic.cosmeticType] == CosmeticName.None)
                {
                    backDisplay.GetComponent<Image>().color = Color.clear;
                }
                break;
        }
    }
}

public enum CosmeticName
{
    None,
    Eyelashes,
    Glasses,
    Anime,
    Scarf,
    Bandana,
    Bowtie,
    Frog,
    Ducken,
    Noot
}

public enum CosmeticType
{
    Head,
    Body,
    Back
}