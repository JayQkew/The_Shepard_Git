using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CosmeticManager : MonoBehaviour
{
    public static CosmeticManager Instance { get; private set; }

    public Dictionary<CosmeticName, bool> unlockedCosmetics = new Dictionary<CosmeticName, bool>();
    public GameObject cosmetic;
    public GameObject selectedCosmetic;

    public GameObject[] slots;

    public bool Debug;

    private void Awake()
    {
        Instance = this;
        slots = GameObject.FindGameObjectsWithTag("slots");
    }

    private void Start()
    {
        foreach(GameObject slot in slots)
        {
            unlockedCosmetics.Add(slot.GetComponent<CosmeticSlot>().cosmeticName, Debug);
        }
    }

    public void NoCosmetic() => cosmetic.SetActive(false);
    public void ChangeCosmetic(Sprite newCosmetic)
    {
        cosmetic.SetActive(true);
        cosmetic.GetComponent<SpriteRenderer>().sprite = newCosmetic;
    }

    public void SelectedSlot(GameObject slot)
    {
        foreach (GameObject s in slots)
        {
            if (s != slot) s.GetComponent<CosmeticSlot>().selected = false;
            else s.GetComponent<CosmeticSlot>().selected = true;
        }
        selectedCosmetic = slot;
    }
}

public enum CosmeticName
{
    Hat,
    Beanie,
    Collar,
    Bandanner
}