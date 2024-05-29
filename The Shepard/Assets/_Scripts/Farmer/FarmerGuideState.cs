using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerGuideState : FarmerBaseState
{
    public override void EnterState(FarmerManager manager)
    {
        manager.SetFarmerTarget(manager.farmerTarget);
    }

    public override void UpgradeState(FarmerManager manager)
    {
        if (SheepTracker.Instance.AtRequiredPlace(GameManager.Instance.targetArea))
        {
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
