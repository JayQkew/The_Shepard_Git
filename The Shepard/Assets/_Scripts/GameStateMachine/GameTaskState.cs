using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTaskState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Task Start");
    }

    public override void UpdateState(GameManager manager)
    {
        if (manager.taskComplete == true)
        {
            manager.taskComplete = false;
            manager.SwitchState(manager.MiddayState);
        }
    }
    public override void ExitState(GameManager manager)
    {
        manager.pauseTime = false;
        manager.selectedTask = Tasks.None;
    }

}

public enum Tasks
{
    None,
    Shearing,
    Tagging
}
