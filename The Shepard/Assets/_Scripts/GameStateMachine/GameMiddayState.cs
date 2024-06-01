using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMiddayState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Midday");
    }
    public override void UpdateState(GameManager manager)
    {
        if (manager.selectedTask != Tasks.None && 
            manager.currentTime >= manager.taskTime &&
            manager.taskComplete == false)
        {
            manager.SwitchState(manager.TaskState);
        }

        if (manager.currentTime >= manager.middayEnd)
        {
            manager.SwitchState(manager.EveningState);
        }
    }

    public override void ExitState(GameManager manager)
    {
    }

}
