using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FarmerBaseState
{
    public abstract void EnterState(FarmerManager manager);

    public abstract void UpgradeState(FarmerManager manager);

    public abstract void ExitState(FarmerManager manager);
}
