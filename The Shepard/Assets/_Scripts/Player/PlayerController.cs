using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private PlayerMovement PlayerMovement;
    private PlayerActions PlayerActions;

    private bool forward;
    private bool back;
    private bool left;
    private bool right;
    private bool jump;

    public Rigidbody rb;

    private void Awake()
    {
        Instance = this;
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerActions = GetComponent<PlayerActions>();
    }

    private void Update()
    {
        forward = Input.GetKey(KeyCode.W);
        back = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        jump = Input.GetKey(KeyCode.Space);

        PlayerMovement.MoveStateCheck(Input.GetKey(KeyCode.LeftShift), Input.GetKey(KeyCode.LeftAlt));
    }

    private void FixedUpdate()
    {
        if (forward) PlayerMovement.Move(Vector3.forward);
        if (back) PlayerMovement.Move(Vector3.back);
        if (left) PlayerMovement.Move(Vector3.left);
        if (right) PlayerMovement.Move(Vector3.right);

        if (jump) PlayerMovement.Jump();
    }
}
