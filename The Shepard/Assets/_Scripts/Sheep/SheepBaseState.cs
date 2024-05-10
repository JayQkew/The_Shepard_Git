using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SheepBaseState
{
    public abstract void EnterState(SheepBehaviour manager);
    public abstract void UpdateState(SheepBehaviour manager);
    public abstract void ExitState(SheepBehaviour manager);
}
