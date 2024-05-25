using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMiddayState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Midday");
        TaskDecider(manager);
    }
    public override void UpdateState(GameManager manager)
    {
        //if (manager.currentTime >= manager.taskTime && taskDay && !manager.taskComplete)
        //{
        //    TaskSelect(manager);
        //    manager.SwitchState(manager.TaskState);
        //}
        if (manager.currentTime >= manager.middayEnd)
        {
            manager.SwitchState(manager.EveningState);
        }

    }

    public override void ExitState(GameManager manager)
    {
    }

    public void TaskDecider(GameManager manager)
    {
        if (manager.tasklessDays >= 2)
        {
            manager.tasklessDays = 0;
        }
        else
        {
            int randNum = Random.Range(0, 2);

            if (randNum == 0)
            {
                manager.tasklessDays = 0;
            }
            else
            {
                manager.tasklessDays++;
            }
        }

    }

    public void TaskSelect(GameManager manager)
    {
        int randNum = Random.Range(0, 2);
        if (randNum == 0)
        {
            manager.selectedTask = Tasks.Tagging;
        }
        else
        {
            manager.selectedTask = Tasks.Shearing;
        }
    }

}
