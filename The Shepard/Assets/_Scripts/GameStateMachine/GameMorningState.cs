using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMorningState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Morning");
        //add more sheep here
        SheepSpawner.Instance.Init_Herd();
        SheepTrackerManager.Instance.allSheep = GameObject.FindGameObjectsWithTag("sheep");
        SelectPasture(manager);
    }

    public override void UpdateState(GameManager manager)
    {
        if (SheepTrackerManager.Instance.AtRequiredPlace(manager.currentArea)) manager.SwitchState(manager.MiddayState);
    }

    public override void ExitState(GameManager manager)
    {
    }

    public void SelectPasture(GameManager manager)
    {
        int randomNum = Random.Range(0, 3);

        if(randomNum == 0)
        {
            manager.currentArea = TrackArea.Pasture1;
            AssistanceManager.Instance.ToPasture1();
        }
        else if (randomNum == 1)
        {
            manager.currentArea = TrackArea.Pasture2;
            AssistanceManager.Instance.ToPasture2();
        }
        else
        {
            manager.currentArea = TrackArea.Pasture3;
            AssistanceManager.Instance.ToPasture3();
        }
    }
}
