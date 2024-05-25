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

    private void Update()
    {

        if (go_canvas.activeSelf)
        {
            if (inputField.isFocused)
            {
                Debug.Log("isFocused");
                PlayerController.Instance.PlayerMovement.canMove = false;
            }
            else
            {
                PlayerController.Instance.PlayerMovement.canMove = true;
            }
        }
    }
}
