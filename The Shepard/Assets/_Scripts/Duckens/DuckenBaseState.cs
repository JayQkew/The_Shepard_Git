using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DuckenBaseState
{
    public abstract void EnterState(DuckenManager manager);
    public abstract void ExitState(DuckenManager manager);
    public abstract void UpdateState(DuckenManager manager);
}
