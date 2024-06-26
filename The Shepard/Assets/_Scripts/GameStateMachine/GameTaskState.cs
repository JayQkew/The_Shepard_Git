using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTaskState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Task Start");
        AssistanceManager.Instance.ToPen();
        manager.targetArea = TrackArea.Pen;
        FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerShearingState);
    }

    public override void UpdateState(GameManager manager)
    {
        manager.currentTime += Time.deltaTime;

        if (SheepTracker.Instance.AtRequiredPlace(TrackArea.Pen))
        {
            manager.penForceWall.SetActive(true);
        }

        if (manager.longWoolCount <= 0)
        {
            manager.taskComplete = true;
            ToCurrentState(manager);
        }
        else if (manager.currentTime >= manager.middayEnd)
        {
            manager.SwitchState(manager.EveningState);
        }


    }
    public override void ExitState(GameManager manager)
    {
        manager.penForceWall.SetActive(false);

        if (manager.taskComplete == true)
        {
            manager.selectedTask = Tasks.None;

        }

        AssistanceManager.Instance.BackToBarn();
        manager.targetArea = TrackArea.Barn;
    }

    public void ToCurrentState(GameManager manager)
    {
        if (manager.currentTime <= manager.morningEnd)
        {
            manager.SwitchState(manager.MorningState);
        }
        else if (manager.currentTime <= manager.middayEnd)
        {
            manager.SwitchState(manager.MiddayState);
        }
        else if (manager.currentTime <= manager.eveningEnd)
        {
            manager.SwitchState(manager.EveningState);
        }
        
    }

}

public enum Tasks
{
    None,
    Shearing,
    Tagging
}
