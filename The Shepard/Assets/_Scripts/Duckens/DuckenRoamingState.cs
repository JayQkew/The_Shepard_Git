using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckenRoamingState : DuckenBaseState
{
    public override void EnterState(DuckenManager manager)
    {
        FindLocation(manager);
    }

    public override void UpdateState(DuckenManager manager)
    {
        if (manager.duckenNavAgent.remainingDistance <= 0.2f)
        {
            manager.SwitchState(manager.DuckenChillingState);
        }
    }

    public override void ExitState(DuckenManager manager)
    {
    }

    public void FindLocation(DuckenManager manager)
    {
        Vector2 rng = Random.insideUnitCircle * manager.searchRadius;
        Vector3 dir = new Vector3(rng.x, 0, rng.y);
        Vector3 destination = manager.transform.position - dir;
        manager.duckenNavAgent.SetDestination(destination);
    }
}
