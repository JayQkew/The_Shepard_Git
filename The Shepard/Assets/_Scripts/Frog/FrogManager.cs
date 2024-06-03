using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrogManager : MonoBehaviour
{
    public NavMeshAgent frogNavAgent;
    public float searchRadius;
    public float frogChillTime;
    public Vector2 frogChillTimeRange;
    public bool found;

    #region States
    public FrogBaseState currentState;
    public FrogChillState FrogChillState = new FrogChillState();
    public FrogJumpingState FrogJumpingState = new FrogJumpingState();
    #endregion

    private void Start()
    {
        currentState = FrogChillState;
        frogNavAgent.updateRotation = false;
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(FrogBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
