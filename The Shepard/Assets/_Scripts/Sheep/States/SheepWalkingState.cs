using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheepWalkingState : SheepBaseState
{
    public override void EnterState(SheepBehaviour manager)
    {
        manager.state = SheepState.Walking;
        manager.SetWalkTime();
        manager.DebugWalk();
    }

    public override void UpdateState(SheepBehaviour manager)
    {
        manager.currentTime += Time.deltaTime;
        if(manager.currentTime >= manager.walkTime)
        {
            manager.inAura = false;
            manager.SwitchState(manager.IdleState);
        }
        else if (manager.startled)
        {
            manager.inAura = false;
            manager.SwitchState(manager.RunningState);
        }
        //limit velocity

        //Idle
        //Running
    }

    public override void ExitState(SheepBehaviour manager)
    {
        manager.currentTime = 0;
    }

}
