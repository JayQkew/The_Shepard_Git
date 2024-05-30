using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GUI : MonoBehaviour
{
    public GameObject playerGUI;
    public Animator mainAnim;
    public Animator guiAnim;

    public void FlipRight()
    {
        mainAnim.SetBool("FaceRight", true);
    }
    public void FlipLeft()
    {
        mainAnim.SetBool("FaceRight", false);
    }
    public void BarkAnim()
    {
        if (mainAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFlipLeft"))
        {
            mainAnim.Play("PlayerBarkLeft");
            Debug.Log("BarkLeft");
        }
        else if (mainAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFlipRight"))
        {
            mainAnim.Play("PlayerBarkRight");
            Debug.Log("BarkRight");
        }
    }

    public void IdleAnim() => guiAnim.SetInteger("Move", 0);
    public void RunAnim() => guiAnim.SetInteger("Move", 1);
    public void JumpAnim() => guiAnim.SetInteger("Move", 2);
    public void ZoomiesAnim() => guiAnim.SetInteger("Move", 3);
}
