using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public MoveState moveState;

    [Space(10)]
    [SerializeField]
    private float walkLimit;
    [SerializeField]
    private float sprintLimit;
    [SerializeField]
    private float crawlLimit;
    [SerializeField]
    [Space(10)]

    private float speedMultiplier = 1;

    [SerializeField]
    private float jumpMultiplier;
    [SerializeField]
    private float groundCastY;
    [SerializeField]
    private LayerMask ground;
    public void Move(Vector3 dir)
    {
        Rigidbody rb = PlayerController.Instance.rb;
        Vector3 forceDir = dir * speedMultiplier;
        rb.AddForce(forceDir, ForceMode.Force);

        float xClampedVelocity = rb.velocity.x;
        float zClampedVelocity = rb.velocity.z;
        Vector3 clampedVelocity = rb.velocity;

        if (Grounded())
        {
            switch (moveState)
            {
                case MoveState.Walk:
                    rb.velocity = Vector3.ClampMagnitude(rb.velocity, walkLimit);
                    break;
                case MoveState.Sprint:
                    rb.velocity = Vector3.ClampMagnitude(rb.velocity, sprintLimit);
                    break;
                case MoveState.Crawl:
                    rb.velocity = Vector3.ClampMagnitude(rb.velocity, crawlLimit);
                    break;
            }
        }
        else
        {
            switch (moveState)
            {
                case MoveState.Walk:
                    rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -walkLimit, walkLimit), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -walkLimit, walkLimit));
                    break;
                case MoveState.Sprint:
                    rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -sprintLimit, sprintLimit), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -sprintLimit, sprintLimit));
                    break;
                case MoveState.Crawl:
                    rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -crawlLimit, crawlLimit), rb.velocity.y, Mathf.Clamp(rb.velocity.z, -crawlLimit, crawlLimit));
                    break;
            }

        }
    }

    public void MoveStateCheck(bool sprint, bool crawl)
    {
        if (sprint) moveState = MoveState.Sprint;
        else if (crawl) moveState = MoveState.Crawl;
        else if (sprint && crawl) moveState = MoveState.Walk;
        else moveState = MoveState.Walk;
    }

    public void Jump()
    {
        if (Grounded())
        {
            Rigidbody rb = PlayerController.Instance.rb;
            Vector3 jumpForce = Vector3.up * jumpMultiplier;
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    private bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCastY, ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCastY, 0));
    }

}

public enum MoveState
{
    Walk,
    Sprint,
    Crawl
}
