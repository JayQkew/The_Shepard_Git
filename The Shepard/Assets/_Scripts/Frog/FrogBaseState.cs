using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FrogBaseState
{
    public abstract void EnterState(FrogManager manager);
    public abstract void ExitState(FrogManager manager);
    public abstract void UpdateState(FrogManager manager);
}
