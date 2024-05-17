using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRunningState : SheepBaseState
{
    public override void EnterState(SheepBehaviour manager)
    {
        manager.state = SheepState.Running;
        manager.currentLimit = manager.runLimit;
        manager.SetRunTime();
        manager.DebugRun();
    }

    public override void UpdateState(SheepBehaviour manager)
    {
        manager.currentTime += Time.deltaTime;
        if (manager.currentTime >= manager.walkTime)
        {
            manager.startled = false;
            manager.SwitchState(manager.WalkingState);
        }

        //limit velocity
        //if velocity falls bellow x, go back to Walking
        //Walking
    }

    public override void ExitState(SheepBehaviour manager)
    {
        manager.currentLimit = manager.walkLimit;
        manager.currentTime = 0;
    }

}
