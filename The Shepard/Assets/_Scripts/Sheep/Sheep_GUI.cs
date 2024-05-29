using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep_GUI : MonoBehaviour
{
    public Animator mainAnim;
    public Animator guiAnim;

    public void FlipRight() => mainAnim.SetBool("FaceRight", true);
    public void FlipLeft() => mainAnim.SetBool("FaceRight", false);
}
