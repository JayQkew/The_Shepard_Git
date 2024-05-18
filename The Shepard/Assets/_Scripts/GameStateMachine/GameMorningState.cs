using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMorningState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Morning");
        SheepSpawner.Instance.Init_Herd();
        SelectPasture(manager);
    }

    public override void UpdateState(GameManager manager)
    {
        if (manager.allSheepHerded) manager.SwitchState(manager.MiddayState);
    }

    public override void ExitState(GameManager manager)
    {
        manager.allSheepHerded = false;
    }

    public void SelectPasture(GameManager manager)
    {
        int randomNum = Random.Range(0, 3);

        if(randomNum == 0)
        {
            manager.currentPasture = Pastures.Pasture1;
            AssistanceManager.Instance.ToPasture1();
        }
        else if (randomNum == 1)
        {
            manager.currentPasture = Pastures.Pasture2;
            AssistanceManager.Instance.ToPasture2();
        }
        else
        {
            manager.currentPasture = Pastures.Pasture3;
            AssistanceManager.Instance.ToPasture3();
        }
    }
}
