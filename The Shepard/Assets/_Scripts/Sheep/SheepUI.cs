using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class SheepUI : MonoBehaviour
{
    public GameObject go_canvas;
    public Canvas sheepCanvas;
    public TMP_InputField inputField;

    private void Awake()
    {
        sheepCanvas = go_canvas.GetComponent<Canvas>();
        sheepCanvas.worldCamera = Camera.main;      
        inputField = sheepCanvas.GetComponentInChildren<TMP_InputField>();
    }

    private void Start()
    {
        inputField.text = GetComponent<SheepBehaviour>().sheepStats.name;
    }

    private void Update()
    {

        if (go_canvas.activeSelf)
        {
            if (inputField.isFocused)
            {
                PlayerController.Instance.PlayerMovement.canMove = false;
            }
            else
            {
                PlayerController.Instance.PlayerMovement.canMove = true;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    public void EnterName()
    {
        GetComponent<SheepBehaviour>().sheepStats.name = inputField.text;
        GetComponent<SheepBehaviour>().sheepStats.tagged = true;

        int taggedSheep = 0;

        foreach (GameObject sheep in SheepTracker.Instance.allSheep)
        {
            if (sheep.GetComponent<SheepBehaviour>().sheepStats.tagged)
            {
                taggedSheep++;
            }
        }

        MissionManager.Instance.sheepNamed = taggedSheep;

        MissionManager.Instance.SheepNamed();
    }
}
