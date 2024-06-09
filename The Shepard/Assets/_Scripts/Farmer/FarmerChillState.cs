using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerChillState : FarmerBaseState
{
    public override void EnterState(FarmerManager manager)
    {
        Debug.Log("Chilling");
        manager.SetFarmerTarget(manager.farmHouse);
    }


    public override void UpgradeState(FarmerManager manager)
    {
    }

    public override void ExitState(FarmerManager manager)
    {
    }

    public override void FixedUpdateState(FarmerManager manager)
    {
    }
}
