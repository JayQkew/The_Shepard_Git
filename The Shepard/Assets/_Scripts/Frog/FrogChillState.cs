using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogChillState : FrogBaseState
{
    public override void EnterState(FrogManager manager)
    {
        DetermineChillTime(manager);
        PlayAudio(manager);
    }

    public override void UpdateState(FrogManager manager)
    {
        manager.frogChillTime -=Time.deltaTime;
        if(manager.frogChillTime <= 0)
        {
            manager.SwitchState(manager.FrogJumpingState);
        }
    }

    public override void ExitState(FrogManager manager)
    {
        PlayAudio(manager);
    }

    public void DetermineChillTime(FrogManager manager)
    {
        manager.frogChillTime = Random.Range(manager.frogChillTimeRange.x, manager.frogChillTimeRange.y);
    }

    private void PlayAudio(FrogManager manager)
    {
        if (Random.value > 0.8f)
        {
            manager.gameObject.GetComponent<AudioLogic>().RandomPlay();
        }
    }

}
