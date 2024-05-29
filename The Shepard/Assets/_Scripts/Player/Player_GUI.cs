using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GUI : MonoBehaviour
{
    public GameObject playerGUI;
    public Animator mainAnim;
    public Animator guiAnim;

    public void FlipRight() => mainAnim.SetBool("FaceRight", true);

    public void FlipLeft() => mainAnim.SetBool("FaceRight", false);
}
