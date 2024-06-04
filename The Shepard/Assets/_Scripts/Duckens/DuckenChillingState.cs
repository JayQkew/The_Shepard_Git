using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckenChillingState : DuckenBaseState
{
    public override void EnterState(DuckenManager manager)
    {
        DetermineChillTime(manager);
    }

    public override void UpdateState(DuckenManager manager)
    {
        manager.duckenChillTime -= Time.deltaTime;
        if(manager.duckenChillTime <= 0)
        {
            manager.SwitchState(manager.DuckenRoamingState);
        }
    }

    public override void ExitState(DuckenManager manager)
    {
    }

    public void DetermineChillTime(DuckenManager manager)
    {
        manager.duckenChillTime = Random.Range(manager.duckenChillTimeRange.x, manager.duckenChillTimeRange.y);
    }

}
