using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEveningState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Evening");
        manager.currentTime = 0;
        AssistanceManager.Instance.BackToBarn();
    }

    public override void UpdateState(GameManager manager)
    {
        if (manager.allSheepHerded)
        {
            manager.SwitchState(manager.MorningState);
        }
    }

    public override void ExitState(GameManager manager)
    {
        manager.allSheepHerded = false;
    }
}
