using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepUI : MonoBehaviour
{
    public GameObject go_canvas;
    public Canvas sheepCanvas;

    private void Awake()
    {
        sheepCanvas = go_canvas.GetComponent<Canvas>();
        sheepCanvas.worldCamera = Camera.main;        
    }
}
