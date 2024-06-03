using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }

    public int daysHerded;
    public int bowtie_r;
    public int anime_r;
    [Space(10)]
    public int sheepNamed;
    public int glasses_r;
    [Space(10)]
    public int shearedSheep;
    public int scarf_r;
    [Space(10)]
    public int frogsFound;
    public int frog_r;
    [Space(10)]
    public int birdsBarkedAt;    //noot
    public int noot_r;

    private void Awake()
    {
        Instance = this;
    }
    public void SheepHerded()
    {
        daysHerded++;
        if (daysHerded == bowtie_r) CosmeticManager.Instance.allCosmetics[CosmeticName.Bowtie] = true;
        else if (daysHerded == anime_r) CosmeticManager.Instance.allCosmetics[CosmeticName.Anime] = true;
    }
    public void SheepNamed()
    {
        if (sheepNamed == glasses_r) CosmeticManager.Instance.allCosmetics[CosmeticName.Glasses] = true;
    }
    public void SheepSheared()
    {
        shearedSheep++;
        if (shearedSheep == scarf_r) CosmeticManager.Instance.allCosmetics[CosmeticName.Scarf] = true;
    }
    public void FrogFound()
    {
        frogsFound++;
        if (frogsFound == frog_r) CosmeticManager.Instance.allCosmetics[CosmeticName.Frog] = true;
    }
    public void BarkAtBird()
    {
        birdsBarkedAt++;
        if (birdsBarkedAt == noot_r) CosmeticManager.Instance.allCosmetics[CosmeticName.Noot] = true;
    }
    public void FarmerInteract() => CosmeticManager.Instance.allCosmetics[CosmeticName.Bandana] = true;
    public void BarkAtDuckens() => CosmeticManager.Instance.allCosmetics[CosmeticName.Ducken] = true;
}
