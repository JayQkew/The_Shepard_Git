using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepBehaviour : MonoBehaviour
{
    public Sheep sheepStats;

    public Rigidbody rb;
    public float currentLimit;
    public float walkLimit;
    public float runLimit;
    public SheepState state;
    public bool inAura;
    public bool startled;   //barked at
    public SheepState nextState;
    public float currentTime;
    public ParticleSystem shearedWool;

    #region State Machine
    public SheepBaseState currentState;

    public SheepIdleState IdleState = new SheepIdleState();
    public SheepGrazingState GrazingState = new SheepGrazingState();
    public SheepRoamingState RoamingState = new SheepRoamingState();
    public SheepRunningState RunningState = new SheepRunningState();
    public SheepWalkingState WalkingState = new SheepWalkingState();
    #endregion

    #region Wool
    public float woolCurrrentTime;
    public float woolGrow;
    public float woolGrowMin;
    public float woolGrowMax;
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
        SetGrowSpeed();
    }

    private void Update()
    {
        CheckMovement();
        GrowWool();
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, currentLimit);
        currentState.UpdateState(this);
    }

    public void SwitchState(SheepBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void GrowWool()
    {
        if (sheepStats.woolLength != WoolLength.Long)
        {
            woolCurrrentTime += Time.deltaTime;

            if (woolCurrrentTime >= woolGrow)
            {
                woolCurrrentTime = 0;
                sheepStats.woolLength++;
                CheckWool();
                if(sheepStats.woolLength == WoolLength.Long) GameManager.Instance.longWoolCount++;
            }
        }
    }

    public void CheckWool()
    {
        switch (sheepStats.woolLength)
        {
            case WoolLength.None:
                GetComponent<Sheep_GUI>().NoneWool();
                break;
            case WoolLength.Short:
                GetComponent<Sheep_GUI>().ShortWool();
                break;
            case WoolLength.Medium:
                GetComponent<Sheep_GUI>().MediumWool();
                break;
            case WoolLength.Long:
                GetComponent<Sheep_GUI>().LongWool();
                break;
        }
    }

    private void SetGrowSpeed() => woolGrow = Random.Range(woolGrowMin, woolGrowMax);

    private void CheckMovement()
    {
        if (rb.velocity.x < -0.25f)
        {
            GetComponent<Sheep_GUI>().FlipLeft();

            if(rb.velocity.x < -0.26f) GetComponent<Sheep_GUI>().RunAnim();
        }
        else if (rb.velocity.x > 0.25f)
        {
            GetComponent<Sheep_GUI>().FlipRight();
            if(rb.velocity.x > 0.26f) GetComponent<Sheep_GUI>().RunAnim();
        }
        else
        {
            GetComponent<Sheep_GUI>().IdleAnim();
        }
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
