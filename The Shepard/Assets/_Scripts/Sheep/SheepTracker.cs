using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepTracker : MonoBehaviour
{
    public static SheepTracker Instance { get; private set; }

    public GameObject[] allSheep;

    [Header("Area Tracking")]
    public List<GameObject> barn;
    public List<GameObject> pen;
    public List<GameObject> pasture1;
    public List<GameObject> pasture2;
    public List<GameObject> pasture3;

    private void Awake()
    {
        Instance = this;
    }

    public bool AtRequiredPlace(TrackArea area)
    {
        switch (area)
        {
            case TrackArea.Barn:
                if (barn.Count == allSheep.Length) return true;
                else return false;
            case TrackArea.Pen:
                if (pen.Count == allSheep.Length) return true;
                else return false;
            case TrackArea.NorthPasture:
                if (pasture1.Count == allSheep.Length) return true;
                else return false;
            case TrackArea.WestPasture:
                if (pasture2.Count == allSheep.Length) return true;
                else return false;
            case TrackArea.EastPasture:
                if (pasture3.Count == allSheep.Length) return true;
                else return false;
            default: return false;
        }
    }
}

public enum TrackArea
{
    Barn,
    Pen,
    NorthPasture,
    WestPasture,
    EastPasture,
    OpenField
}
