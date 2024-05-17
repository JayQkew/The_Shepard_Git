using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepTrackerManager : MonoBehaviour
{
    public static SheepTrackerManager Instance { get; private set; }

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
}

public enum TrackArea
{
    Barn,
    Pen,
    Pasture1,
    Pasture2,
    Pasture3
}
