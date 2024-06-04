using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckenFollowState : DuckenBaseState
{
    public override void EnterState(DuckenManager manager)
    {
    }

    public override void UpdateState(DuckenManager manager)
    {

        manager.duckenNavAgent.SetDestination(manager.followAgent.transform.position);
        if (GameManager.Instance.currentTime >= GameManager.Instance.eveningEnd)
        {
            manager.transform.position = new Vector3(manager.startPosition.x, 0, manager.startPosition.y);
            manager.SwitchState(manager.DuckenChillingState);
        }
    }

    public override void ExitState(DuckenManager manager)
    {
    }
}
