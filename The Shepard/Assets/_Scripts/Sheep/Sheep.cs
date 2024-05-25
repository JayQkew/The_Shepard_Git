using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sheep
{
    public string name;
    public WoolLength woolLength;
    public bool tagged;
}

public enum WoolLength
{
    None,
    Short,
    Medium,
    Long
}