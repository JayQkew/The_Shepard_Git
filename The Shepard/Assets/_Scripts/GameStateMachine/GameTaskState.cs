using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTaskState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Task Start");
        AssistanceManager.Instance.ToPen();
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

        switch (manager.currentArea)
        {
            case TrackArea.Pasture1:
                AssistanceManager.Instance.ToPasture1();
                break;
            case TrackArea.Pasture2:
                AssistanceManager.Instance.ToPasture2();
                break;
            case TrackArea.Pasture3:
                AssistanceManager.Instance.ToPasture3();
                break;
        }
    }

}

public enum Tasks
{
    None,
    Shearing,
    Tagging
}
