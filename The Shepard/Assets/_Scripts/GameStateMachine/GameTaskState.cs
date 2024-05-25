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
        manager.selectedTask = Tasks.None;

        switch (manager.currentArea)
        {
            case TrackArea.NorthPasture:
                AssistanceManager.Instance.ToNorthPasture();
                break;
            case TrackArea.WestPasture:
                AssistanceManager.Instance.ToWestPasture();
                break;
            case TrackArea.EastPasture:
                AssistanceManager.Instance.ToEastPasture();
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
