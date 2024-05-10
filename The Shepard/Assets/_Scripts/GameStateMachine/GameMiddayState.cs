using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMiddayState : GameBaseState
{
    private float dayLength;
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Midday");
        dayLength = manager.dayLength;
    }
    public override void UpdateState(GameManager manager)
    {
        manager.currentTime += Time.deltaTime;
        if(manager.currentTime >= dayLength)
        {
            manager.SwitchState(manager.EveningState);
        }
    }

    public override void ExitState(GameManager manager)
    {
        manager.currentTime = 0;
    }
}
