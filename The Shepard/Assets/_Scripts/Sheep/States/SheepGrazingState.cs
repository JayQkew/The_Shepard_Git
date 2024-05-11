using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepGrazingState : SheepBaseState
{
    public override void EnterState(SheepBehaviour manager)
    {
        manager.state = SheepState.Grazing;
        manager.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        manager.nextState = ChooseNextState();
    }

    public override void UpdateState(SheepBehaviour manager)
    {
        manager.currnetGrazeTime += Time.deltaTime;

        if (manager.currnetGrazeTime >= manager.grazeTime)
        {
            if (manager.nextState == SheepState.Grazing) manager.SwitchState(manager.GrazingState);
            else manager.SwitchState(manager.IdleState);
        }

        if (manager.inAura) manager.SwitchState(manager.WalkingState);
        if (manager.startled) manager.SwitchState(manager.RunningState);

        //Idle
        //Walking
        //Running
    }

    public override void ExitState(SheepBehaviour manager)
    {
        manager.currnetGrazeTime = 0;
    }

    private SheepState ChooseNextState()
    {
        int state = Random.Range(0, 4);
        if (state == 0) return SheepState.Grazing;
        else return SheepState.Idle;
    }

}
