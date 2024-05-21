using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEveningState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Evening");
        AssistanceManager.Instance.BackToBarn();
    }

    public override void UpdateState(GameManager manager)
    {
        //if (SheepTrackerManager.Instance.AtRequiredPlace(TrackArea.Barn))
        //{
        //    manager.SwitchState(manager.MorningState);
        //}

        if (manager.currentTime >= manager.eveningEnd)
        {
            manager.SwitchState(manager.MorningState);
        }
    }

    public override void ExitState(GameManager manager)
    {
        manager.currentTime = 0;
    }
}
