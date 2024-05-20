using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameBaseState currentState;
    public GameMorningState MorningState = new GameMorningState();
    public GameMiddayState MiddayState = new GameMiddayState();
    public GameTaskState TaskState = new GameTaskState();
    public GameEveningState EveningState = new GameEveningState();

    [Header("Morning & Evening")]
    public TrackArea currentArea;

    [Header("Tasks")]
    public Tasks selectedTask;
    public bool taskComplete;
    public int tasklessDays;
    public bool pauseTime;

    [Header("Midday")]
    public float dayLength = 5;
    public float taskTime = 2.5f;
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
