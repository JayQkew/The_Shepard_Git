using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameBaseState currentState;
    public GameMorningState MorningState = new GameMorningState();
    public GameMiddayState MiddayState = new GameMiddayState();
    public GameEveningState EveningState = new GameEveningState();

    public bool allSheepHerded = false;
    public float dayLength = 5;
    public float currentTime;

    private void Awake()
    {
        currentState = MorningState;
    }
    private void Start()
    {
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
