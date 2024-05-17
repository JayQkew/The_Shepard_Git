using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehaviour : MonoBehaviour
{
    public Rigidbody rb;
    public float currentLimit;
    public float walkLimit;
    public float runLimit;
    public SheepState state;
    public bool inAura;
    public bool startled;   //barked at
    public SheepState nextState;
    public float currentTime;

    #region State Machine
    public SheepBaseState currentState;

    public SheepIdleState IdleState = new SheepIdleState();
    public SheepGrazingState GrazingState = new SheepGrazingState();
    public SheepRoamingState RoamingState = new SheepRoamingState();
    public SheepRunningState RunningState = new SheepRunningState();
    public SheepWalkingState WalkingState = new SheepWalkingState();
    #endregion

    [Header("State Times")]
    public float idleTime;
    public float grazeTime;
    public float roamTime;
    public float walkTime;
    public float runTime;

    [Space(10)]
    public Vector3 roamDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = IdleState;
        currentLimit = walkLimit;
        SetIdleTime();
    }

    private void Update()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, currentLimit);
        currentState.UpdateState(this);
    }

    public void SwitchState(SheepBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    #region Set Times
    public void SetIdleTime() => idleTime = Random.Range(SheepManager.Instance.idleRange.x, SheepManager.Instance.idleRange.y);
    public void SetGrazeTime() => grazeTime = Random.Range(SheepManager.Instance.grazeRange.x, SheepManager.Instance.grazeRange.y);
    public void SetRoamTime() => roamTime = Random.Range(SheepManager.Instance.roamRange.x, SheepManager.Instance.roamRange.y);
    public void SetWalkTime() => walkTime = Random.Range(SheepManager.Instance.walkRange.x, SheepManager.Instance.walkRange.y);
    public void SetRunTime() => runTime = Random.Range(SheepManager.Instance.runRange.x, SheepManager.Instance.runRange.y);
    #endregion

    #region Debug Colours
    public void DebugIdle()
    {
        if (SheepManager.Instance.debugColour)
        {
            GetComponentInChildren<SpriteRenderer>().color = SheepManager.Instance.idleColour;
        }
    }
    public void DebugGraze()
    {
        if (SheepManager.Instance.debugColour)
        {
            GetComponentInChildren<SpriteRenderer>().color = SheepManager.Instance.grazeColour;
        }
    }

    public void DebugRoam()
    {
        if (SheepManager.Instance.debugColour)
        {
            GetComponentInChildren<SpriteRenderer>().color = SheepManager.Instance.roamColour;
        }
    }
    public void DebugWalk()
    {
        if (SheepManager.Instance.debugColour)
        {
            GetComponentInChildren<SpriteRenderer>().color = SheepManager.Instance.walkColour;
        }
    }
    public void DebugRun()
    {
        if (SheepManager.Instance.debugColour)
        {
            GetComponentInChildren<SpriteRenderer>().color = SheepManager.Instance.runColour;
        }
    }
    #endregion
}

public enum SheepState
{
    Idle,
    Grazing,
    Roaming,
    Walking,
    Running
}
