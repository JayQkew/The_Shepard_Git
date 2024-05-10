using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    public float speedLimit;

    #region State Machine
    public SheepBaseState currentState;

    public SheepIdleState IdleState = new SheepIdleState();
    public SheepGrazingState GrazingState = new SheepGrazingState();
    public SheepRoamingState RoamingState = new SheepRoamingState();
    public SheepRunningState RunningState = new SheepRunningState();
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = IdleState;
    }

    private void Update()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, speedLimit);
        currentState.UpdateState(this);
    }

    public void SwitchState(SheepBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
