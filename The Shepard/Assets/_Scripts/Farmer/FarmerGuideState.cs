using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerGuideState : FarmerBaseState
{
    public override void EnterState(FarmerManager manager)
    {
        Debug.Log("Guiding to" + manager.farmerTarget.name);

        manager.openGate = true;
        manager.SetFarmerTarget(manager.farmerTarget);
    }

    public override void UpgradeState(FarmerManager manager)
    {
        if(GameManager.Instance.currentTime >= GameManager.Instance.bringSheepBack)
        {
            if (manager.farmerNavAgent.remainingDistance <= 1f)
            {
                if (!manager.whistle)
                {
                    manager.farmer.GetComponentInChildren<AudioLogic>().Play();
                    manager.whistle = true;
                }
                if (!GameManager.Instance.eveningMusicStarted)
                {
                    MusicManager.Instance.FadeMusicIn();
                    GameManager.Instance.eveningMusicStarted = true;
                }
            }
        }


        if (SheepTracker.Instance.AtRequiredPlace(GameManager.Instance.targetArea))
        {
            manager.openGate = false;
            manager.SwitchState(manager.FarmerChillState);
        }
    }

    public override void ExitState(FarmerManager manager)
    {
    }

    public override void FixedUpdateState(FarmerManager manager)
    {
    }
}
