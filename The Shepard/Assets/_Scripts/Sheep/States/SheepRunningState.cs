using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRunningState : SheepBaseState
{
    public override void EnterState(SheepBehaviour manager)
    {
        manager.state = SheepState.Running;
        manager.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }

    public override void UpdateState(SheepBehaviour manager)
    {
        manager.walkCurrentTime += Time.deltaTime;
        if (manager.walkCurrentTime > manager.walkCoolDown)
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
        manager.runCurrentTime = 0;
    }

}
