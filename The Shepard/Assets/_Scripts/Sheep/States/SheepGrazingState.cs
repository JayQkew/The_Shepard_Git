using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepGrazingState : SheepBaseState
{
    public override void EnterState(SheepBehaviour manager)
    {
        manager.state = SheepState.Grazing;
        manager.SetGrazeTime();
        manager.DebugGraze();
        manager.nextState = ChooseNextState();
        manager.rb.drag = 10f;
        manager.rb.angularDrag = 10f;
        PlayAudio(manager);
    }

    public override void UpdateState(SheepBehaviour manager)
    {
        manager.currentTime += Time.deltaTime;

        if (manager.currentTime >= manager.grazeTime)
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
        manager.currentTime = 0; 
        manager.rb.drag = 0;
        manager.rb.angularDrag = 0;
        PlayAudio(manager);

    }

    private SheepState ChooseNextState()
    {
        int state = Random.Range(0, 4);
        if (state == 0) return SheepState.Grazing;
        else return SheepState.Idle;
    }

    private void PlayAudio(SheepBehaviour manager)
    {
        if(Random.value > 0.7f)
        {
            manager.gameObject.GetComponent<AudioLogic>().RandomPlay();
        }
    }
}
