using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBaseState
{
    public abstract void EnterState(GameManager manager);
    public abstract void UpdateState(GameManager manager);
    public abstract void ExitState(GameManager manager);
}
