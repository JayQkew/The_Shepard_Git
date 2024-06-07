using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public static BirdManager Instance { get; private set; }

    public Vector2 flyUpRanger;
    public Vector2 flyAwayRange;

    public float areaRadius;
    public LayerMask scareAgents;


    private void Awake()
    {
        Instance = this;
    }
}
