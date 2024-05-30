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
        SheepTracker.Instance.allSheep = GameObject.FindGameObjectsWithTag("sheep");
        SelectPasture(manager);
        ShearSheepCount(manager);
    }

    public override void UpdateState(GameManager manager)
    {
        //if (SheepTrackerManager.Instance.AtRequiredPlace(manager.currentArea)) manager.SwitchState(manager.MiddayState);
        if (manager.currentTime >= manager.morningEnd)
        {
            manager.SwitchState(manager.MiddayState);
        }
    }

    public override void ExitState(GameManager manager)
    {
    }

    public void SelectPasture(GameManager manager)
    {
        int randomNum = Random.Range(0, 2);

        if(randomNum == 0)
        {
            manager.targetArea = TrackArea.NorthPasture;
            FarmerManager.Instance.farmerTarget = FarmerManager.Instance.northPastureOut;
            AssistanceManager.Instance.ToNorthPasture();
        }
        else
        {
            manager.targetArea = TrackArea.EastPasture;
            AssistanceManager.Instance.ToEastPasture();
            FarmerManager.Instance.farmerTarget = FarmerManager.Instance.eastPastureOut;
        }

        FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerGuideState);
    }

    private void ShearSheepCount(GameManager manager)
    {
        float amount = SheepTracker.Instance.allSheep.Length * (manager.longWoolRatio.x / manager.longWoolRatio.y);
        manager.shearTaskCount = Mathf.FloorToInt(amount);

        if(manager.longWoolCount >= manager.shearTaskCount)
        {
            manager.selectedTask = Tasks.Shearing;
        }
    }
}
