using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DuckenManager : MonoBehaviour
{
    public NavMeshAgent duckenNavAgent;
    public float searchRadius;
    public float duckenChillTime;
    public Vector2 duckenChillTimeRange;
    public GameObject followAgent;
    public Vector3 startPosition;

    #region States
    public DuckenBaseState currentState;
    public DuckenChillingState DuckenChillingState = new DuckenChillingState();
    public DuckenFollowState DuckenFollowState = new DuckenFollowState();
    public DuckenRoamingState DuckenRoamingState = new DuckenRoamingState();
    #endregion

    void Start()
    {
        startPosition = transform.position;
        currentState = DuckenChillingState;
        duckenNavAgent.updateRotation = false;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(DuckenBaseState state)
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
