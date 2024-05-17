using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepManager : MonoBehaviour
{
    public static SheepManager Instance { get; private set; }

    [Header("State Time Range"), Tooltip("x = min, y = max")]
    public Vector2 idleRange;
    public Vector2 grazeRange;
    public Vector2 roamRange;
    public Vector2 walkRange;
    public Vector2 runRange;

    [Header("Debug Colours")]
    public bool debugColour;
    public Color idleColour;
    public Color grazeColour;
    public Color roamColour;
    public Color walkColour;
    public Color runColour;

    private void Awake()
    {
        Instance = this;
    }
}
