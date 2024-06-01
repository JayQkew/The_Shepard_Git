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
        if (manager.currentTime >= manager.eveningEnd)
        {
            manager.SwitchState(manager.MorningState);
        }
    }

    public override void ExitState(GameManager manager)
    {
        AssistanceManager.Instance.CloseAllGates();
        manager.taskComplete = false;
        manager.currentTime = 0;
    }
}
