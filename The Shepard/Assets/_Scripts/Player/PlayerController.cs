using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    public PlayerMovement PlayerMovement;
    public PlayerActions PlayerActions;
    public Player_GUI PlayerGUI;

    private bool forward;
    private bool back;
    private bool left;
    private bool right;
    private bool jump;

    public Rigidbody rb;

    public TrackArea playerArea;

    private void Awake()
    {
        Instance = this;
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerActions = GetComponent<PlayerActions>();
        PlayerGUI = GetComponent<Player_GUI>();
    }

    private void Update()
    {
        forward = Input.GetKey(KeyCode.W);
        back = Input.GetKey(KeyCode.S);
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        jump = Input.GetKey(KeyCode.Space);

        #region Player Sprite Animations
        if (!PlayerMovement.Grounded()) PlayerGUI.JumpAnim();
        else if (forward || back || left || right)
        {
            if (left && rb.velocity.x > 0 && PlayerMovement.Grounded() && Input.GetKey(KeyCode.LeftShift)) PlayerGUI.ZoomiesAnim();
            else if (right && rb.velocity.x < 0 && PlayerMovement.Grounded() && Input.GetKey(KeyCode.LeftShift)) PlayerGUI.ZoomiesAnim();
            else PlayerGUI.RunAnim();
        }
        else PlayerGUI.IdleAnim();
        #endregion

        #region Flip Animation
        if (right) PlayerGUI.FlipRight();
        if (left) PlayerGUI.FlipLeft();
        #endregion

        #region Inertia Damping
        if (!forward && !back) PlayerMovement.InertiaDamp_Z(rb);
        if (!left && !right) PlayerMovement.InertiaDamp_X(rb);
        #endregion

        if (Input.GetMouseButtonDown(0))
        {
            PlayerActions.Bark();
            PlayerGUI.BarkAnim();
        }
        if (Input.GetMouseButtonDown(1)) PlayerActions.Interact();

        PlayerMovement.MoveStateCheck(Input.GetKey(KeyCode.LeftShift), Input.GetKey(KeyCode.V));

        CameraLogic.Instance.CameraZoomControl(Input.mouseScrollDelta.y);



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
