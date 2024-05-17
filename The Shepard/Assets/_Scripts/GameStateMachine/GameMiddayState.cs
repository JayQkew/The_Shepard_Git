using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMiddayState : GameBaseState
{
    bool taskDay;
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Midday");
        TaskDecider(manager);
    }
    public override void UpdateState(GameManager manager)
    {
        if (!manager.pauseTime)
        {
            manager.currentTime += Time.deltaTime;
        }

        if (manager.currentTime >= manager.taskTime && taskDay && !manager.taskComplete)
        {
            manager.pauseTime = true;
            SelectTask(manager);
            manager.SwitchState(manager.TaskState);
        }
        else if (manager.currentTime >= manager.dayLength)
        {
            manager.SwitchState(manager.EveningState);
        }
    }

    public override void ExitState(GameManager manager)
    {
        taskDay = false;
    }

    public void TaskDecider(GameManager manager)
    {
        if (manager.tasklessDays >= 2)
        {
            taskDay = true;
            manager.tasklessDays = 0;
        }
        else
        {
            int randNum = Random.Range(0, 2);

            if (randNum == 0)
            {
                taskDay = true;
                manager.tasklessDays = 0;
            }
            else
            {
                manager.tasklessDays++;
            }
        }

    }

    public void SelectTask(GameManager manager)
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
