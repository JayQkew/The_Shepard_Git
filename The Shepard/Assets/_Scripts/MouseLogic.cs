using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLogic : MonoBehaviour
{
    public static MouseLogic Instance { get; private set; }

    public MouseState mouseState;
    public Image image;
    public bool isInteractable;
    public Sprite normalSprite;
    public Sprite interactableSprite;
    public Sprite editSprite;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        FollowMouse();
        Interactable();
    }

    public void FollowMouse()
    {
        transform.position = Input.mousePosition;
    }

    public void Interactable()
    {
        if (mouseState == MouseState.Interact) image.sprite = interactableSprite;
        else if (mouseState == MouseState.Edit) image.sprite = editSprite;
        else image.sprite = normalSprite;
    }
}

public enum MouseState
{
    None,
    Interact,
    Edit
}