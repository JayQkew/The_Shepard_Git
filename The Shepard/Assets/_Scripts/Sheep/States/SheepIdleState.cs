using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepIdleState : SheepBaseState
{

    public override void EnterState(SheepBehaviour manager)
    {
        manager.state = SheepState.Idle;
        manager.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        manager.nextState = ChooseNextState();
    }

    public override void UpdateState(SheepBehaviour manager)
    {
        manager.currentWaitTime += Time.deltaTime;

        if(manager.currentWaitTime >= manager.waitTime)
        {
            if (manager.nextState == SheepState.Grazing) manager.SwitchState(manager.GrazingState);
            else if (manager.nextState == SheepState.Roaming) manager.SwitchState(manager.RoamingState);
            else manager.SwitchState(manager.IdleState);
        }

        if(manager.inAura) manager.SwitchState(manager.WalkingState);
        if(manager.startled) manager.SwitchState(manager.RunningState);

        //Idle
        //Grazing
        //Roaming
        //Walking
        //Running
    }

    public override void ExitState(SheepBehaviour manager)
    {
        manager.currentWaitTime = 0;
    }

    private SheepState ChooseNextState()
    {
        int state = Random.Range(0, 4);
        if(state == 0) return SheepState.Idle;
        else if(state == 1) return SheepState.Grazing;
        else return SheepState.Roaming;
    }


}
