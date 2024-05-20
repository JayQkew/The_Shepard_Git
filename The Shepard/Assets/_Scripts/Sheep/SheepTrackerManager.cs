using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepTrackerManager : MonoBehaviour
{
    public static SheepTrackerManager Instance { get; private set; }

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
            case TrackArea.Pasture1:
                if (pasture1.Count == allSheep.Length) return true;
                else return false;
            case TrackArea.Pasture2:
                if (pasture2.Count == allSheep.Length) return true;
                else return false;
            case TrackArea.Pasture3:
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
    Pasture1,
    Pasture2,
    Pasture3,
    OpenField
}
