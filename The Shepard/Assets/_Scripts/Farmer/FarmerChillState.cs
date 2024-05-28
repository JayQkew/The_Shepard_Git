using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerChillState : FarmerBaseState
{
    public override void EnterState(FarmerManager manager)
    {
        manager.SetFarmerTarget(manager.farmHouse);
    }


    public override void UpgradeState(FarmerManager manager)
    {
    }

    public override void ExitState(FarmerManager manager)
    {
    }

}
