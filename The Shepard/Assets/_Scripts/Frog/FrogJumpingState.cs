using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJumpingState : FrogBaseState
{
    public override void EnterState(FrogManager manager)
    {
        FindLocation(manager);
    }

    public override void UpdateState(FrogManager manager)
    {
        if(manager.frogNavAgent.remainingDistance <= 0.2f)
        {
            manager.SwitchState(manager.FrogChillState);
        }
    }

    public override void ExitState(FrogManager manager)
    {
    }

    public void FindLocation(FrogManager manager)
    {
        Vector2 rng = Random.insideUnitCircle * manager.searchRadius;
        Vector3 dir = new Vector3(rng.x, 0, rng.y);
        Vector3 destination = manager.transform.position - dir;
        manager.frogNavAgent.SetDestination(destination);
    }
}
