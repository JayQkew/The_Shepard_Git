using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepRoamingState : SheepBaseState
{
    Vector3 roamDir;
    public override void EnterState(SheepBehaviour manager)
    {
        manager.state = SheepState.Roaming;
        manager.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        manager.nextState = ChooseNextState();
        roamDir = RoamDir(manager);
    }

    public override void UpdateState(SheepBehaviour manager)
    {
        manager.currentRoamTime += Time.deltaTime;
        //manager.rb.AddForce(RoamDir(manager)*5, ForceMode.Force);
        manager.rb.velocity = roamDir;

        if (manager.currentRoamTime >= manager.roamTime)
        {
            if (manager.nextState == SheepState.Grazing) manager.SwitchState(manager.GrazingState);
            else manager.SwitchState(manager.IdleState);
        }

        if (manager.inAura) manager.SwitchState(manager.WalkingState);
        if (manager.startled) manager.SwitchState(manager.RunningState);

        //Idle
        //Walking
        //Running
    }

    public override void ExitState(SheepBehaviour manager)
    {
        manager.currentRoamTime = 0;
    }

    private SheepState ChooseNextState()
    {
        int state = Random.Range(0, 2);
        if (state == 1) return SheepState.Idle;
        else return SheepState.Grazing;
    }

    private Vector3 RoamDir(SheepBehaviour manager)
    {
        float randomX = Random.Range(-3.5f, 3.5f) + manager.transform.position.x;
        float randomZ = Random.Range(-3.5f, 3.5f) + manager.transform.position.z;
        Vector3 dir = manager.transform.position - new Vector3(randomX, manager.transform.position.y, randomZ) ;
        //return Vector3.ClampMagnitude(dir, 1);
        return dir;
    }

}
