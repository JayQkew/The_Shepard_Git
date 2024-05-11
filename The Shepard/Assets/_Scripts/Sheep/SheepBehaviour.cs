using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public float speedLimit;
    public SheepState state;
    public bool inAura;
    public bool startled;   //barked at
    public SheepState nextState;

    #region State Machine
    public SheepBaseState currentState;

    public SheepIdleState IdleState = new SheepIdleState();
    public SheepGrazingState GrazingState = new SheepGrazingState();
    public SheepRoamingState RoamingState = new SheepRoamingState();
    public SheepRunningState RunningState = new SheepRunningState();
    public SheepWalkingState WalkingState = new SheepWalkingState();
    #endregion

    [Header("Idle")]
    public float waitTime;
    public float currentWaitTime;

    [Header("Grazing")]
    public float grazeTime;
    public float currnetGrazeTime;

    [Header("Roaming")]
    public float roamTime;
    public float currentRoamTime;
    public Vector3 roamDirection;

    [Header("Walking")]
    public float walkCoolDown;
    public float walkCurrentTime;

    [Header("Running")]
    public float runCoolDown;
    public float runCurrentTime;
    
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

    private void FixedUpdate()
    {
        //if (state == SheepState.Roaming)
        //{
        //    rb.AddForce(roamDirection, ForceMode.Force);
        //}
    }

    public void SwitchState(SheepBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}

public enum SheepState
{
    Idle,
    Grazing,
    Roaming,
    Walking,
    Running
}
