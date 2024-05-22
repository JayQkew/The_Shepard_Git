using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CosmeticManager : MonoBehaviour
{
    public static CosmeticManager Instance { get; private set; }

    public GameObject cosmetic;
    public GameObject selectedCosmetic;

    public GameObject[] slots;

    private void Awake()
    {
        Instance = this;
        slots = GameObject.FindGameObjectsWithTag("slots");
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
