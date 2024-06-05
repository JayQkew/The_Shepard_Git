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
    public TextMeshProUGUI cosmeticProgress;

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
        CosmeticSlot cosmeticSlot = slot.GetComponent<CosmeticSlot>();
        CosmeticType cosType = cosmeticSlot.cosmeticType;

        foreach (GameObject s in slots)
        {
            CosmeticSlot s_cosmeticSlot = s.GetComponent<CosmeticSlot>();

            if (allCosmetics[s_cosmeticSlot.cosmeticName] && s_cosmeticSlot.cosmeticType == cosType)  //unlocked
            {
                if (s != slot) //not selected Slot
                {
                    s_cosmeticSlot.selected = false;
                    s_cosmeticSlot.slotIcon.GetComponent<Image>().sprite = s_cosmeticSlot.unpressedButton;
                    s_cosmeticSlot.slotIcon.GetComponent<Image>().color = Color.white;
                }
                else
                {
                    s_cosmeticSlot.selected = true;
                    s_cosmeticSlot.slotIcon.GetComponent<Image>().color = s_cosmeticSlot.selectedColor;
                }

            }
        }

        switch (cosType)
        {
            case CosmeticType.Head:
                if (equiptCosmetics[cosType] == cosmeticSlot.cosmeticName) UnEquiptItem(playerHeadCosmetic, headDisplay, slot);
                else EquiptItem(playerHeadCosmetic, headDisplay, slot);
                break;
            case CosmeticType.Body:
                if (equiptCosmetics[cosType] == cosmeticSlot.cosmeticName) UnEquiptItem(playerBodyCosmetic, bodyDisplay, slot);
                else EquiptItem(playerBodyCosmetic, bodyDisplay, slot);
                break;
            case CosmeticType.Back:
                if (equiptCosmetics[cosType] == cosmeticSlot.cosmeticName) UnEquiptItem(playerBackCosmetic, backDisplay, slot);
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
            cosmeticProgress.text = MissionDescription(slot);
        }

    }

    private string MissionDescription(GameObject slot)
    {
        int completed = 0;
        string inbetween = " of ";
        int needed = 0;

        switch (slot.GetComponent<CosmeticSlot>().cosmeticName)
        {
            case CosmeticName.Glasses:
                completed = MissionManager.Instance.sheepNamed;
                needed = MissionManager.Instance.glasses_r;
                break;
            case CosmeticName.Anime:
                completed = MissionManager.Instance.daysHerded;
                needed = MissionManager.Instance.anime_r;
                break;
            case CosmeticName.Scarf:
                completed = MissionManager.Instance.shearedSheep;
                needed = MissionManager.Instance.scarf_r;
                break;
            case CosmeticName.Bandana:
                return "";
            case CosmeticName.Bowtie:
                completed = MissionManager.Instance.daysHerded;
                needed = MissionManager.Instance.bowtie_r;
                break;
            case CosmeticName.Frog:
                completed = MissionManager.Instance.frogsFound;
                needed = MissionManager.Instance.frog_r;
                break;
            case CosmeticName.Ducken:
                completed = PlayerController.Instance.followingDuckens.Count;
                needed = MissionManager.Instance.followingDuckens;
                break;
            case CosmeticName.Noot:
                completed = MissionManager.Instance.birdsBarkedAt;
                needed = MissionManager.Instance.noot_r;
                break;
        }

        return $"{completed}{inbetween}{needed}";
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