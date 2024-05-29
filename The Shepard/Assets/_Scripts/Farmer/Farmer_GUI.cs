using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Farmer_GUI : MonoBehaviour
{
    public GameObject GUI;
    public Animator animator;
    public NavMeshAgent farmerNavAgent;

    private void Update()
    {
        if (farmerNavAgent.remainingDistance <= 1.5f)
        {
            animator.SetInteger("AnimState", 0);
        }
        else
        {
            animator.SetInteger("AnimState", 1);
        }

        if(farmerNavAgent.velocity.x < 0)
        {
            
        }

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    StartCoroutine(SpriteManager.Instance.FlipSprite(GUI, -1, 0.5f));
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    StartCoroutine(SpriteManager.Instance.FlipSprite(GUI, 1, 0.5f));
        //}
    }
}
