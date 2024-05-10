using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMorningState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Morning");
        SheepSpawner.Instance.Gen_SpawnMatrix();
        SheepSpawner.Instance.SpawnSheepHerd();
    }

    public override void UpdateState(GameManager manager)
    {
        if (manager.allSheepHerded) manager.SwitchState(manager.MiddayState);
    }

    public override void ExitState(GameManager manager)
    {
        manager.allSheepHerded = false;
    }
}
