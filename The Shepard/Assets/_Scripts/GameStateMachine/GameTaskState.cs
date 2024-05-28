using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTaskState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Task Start");
        AssistanceManager.Instance.ToPen();
        FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerShearingState);
    }

    public override void UpdateState(GameManager manager)
    {
        manager.currentTime += Time.deltaTime;
        if (manager.taskComplete == true)
        {
            //manager.SwitchState(manager.MiddayState);
            ToCurrentState(manager);
        }
        else if (manager.currentTime >= manager.middayEnd)
        {
            manager.SwitchState(manager.EveningState);
        }


    }
    public override void ExitState(GameManager manager)
    {
        if (manager.taskComplete == true)
        {
            manager.selectedTask = Tasks.None;
            manager.taskComplete = false;
        }

        //ToCurrentState(manager);

        switch (manager.targetArea)
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
            case TrackArea.Barn:
                AssistanceManager.Instance.BackToBarn();
                break;
            case TrackArea.Pen:
                AssistanceManager.Instance.ToPen();
                break;
        }
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
