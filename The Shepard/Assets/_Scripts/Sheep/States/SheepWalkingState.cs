using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheepWalkingState : SheepBaseState
{
    public override void EnterState(SheepBehaviour manager)
    {
        manager.state = SheepState.Walking;
        manager.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
    }

    public override void UpdateState(SheepBehaviour manager)
    {
        manager.walkCurrentTime += Time.deltaTime;
        if(manager.walkCurrentTime > manager.walkCoolDown)
        {
            BoidsManager.Instance.boids.Remove(manager.gameObject);
            manager.inAura = false;
            manager.SwitchState(manager.IdleState);
        }
        //limit velocity

        //Idle
        //Running
    }

    public override void ExitState(SheepBehaviour manager)
    {
        manager.walkCurrentTime = 0;
    }

}
