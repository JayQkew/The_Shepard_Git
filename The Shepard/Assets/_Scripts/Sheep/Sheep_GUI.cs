using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep_GUI : MonoBehaviour
{
    public Animator mainAnim;
    public Animator guiAnim;

    public void FlipRight() => mainAnim.SetBool("FaceRight", true);
    public void FlipLeft() => mainAnim.SetBool("FaceRight", false);

    public void NoneWool() => guiAnim.SetInteger("SheepLength", 0);
    public void ShortWool() => guiAnim.SetInteger("SheepLength", 1);
    public void MediumWool() => guiAnim.SetInteger("SheepLength", 2);
    public void LongWool() => guiAnim.SetInteger("SheepLength", 3);
}
