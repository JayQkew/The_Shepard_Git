using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MoveState moveState;

    public bool canMove;

    [Header("Movement Limits")]
    [SerializeField]
    private float walkLimit;
    [SerializeField]
    private float sprintLimit;
    [SerializeField]
    private float crawlLimit;
    [SerializeField]

    private float speedMultiplier = 1;

    [SerializeField]
    private float jumpMultiplier;
    [SerializeField]
    private float groundCastY;
    [SerializeField]
    private LayerMask ground;
    [SerializeField]
    private float intertiaDampDelta;
    public void Move(Vector3 dir)
    {
        if (canMove)
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

    public bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCastY, ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCastY, 0));
    }

    #region Inertia Damping
    public void InertiaDamp_X(Rigidbody rb)
    {
        Vector3 xAxisDamp = new Vector3(0, rb.velocity.y, rb.velocity.z);
        rb.velocity = Vector3.MoveTowards(rb.velocity, xAxisDamp, intertiaDampDelta * Time.deltaTime);
    }

    public void InertiaDamp_Z(Rigidbody rb)
    {
        Vector3 zAxisDamp = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        rb.velocity = Vector3.MoveTowards(rb.velocity, zAxisDamp, intertiaDampDelta * Time.deltaTime);
    }
    #endregion
}

public enum MoveState
{
    Walk,
    Sprint,
    Crawl
}
