using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameBaseState currentState;
    public GameMorningState MorningState = new GameMorningState();
    public GameMiddayState MiddayState = new GameMiddayState();
    public GameTaskState TaskState = new GameTaskState();
    public GameEveningState EveningState = new GameEveningState();

    [Header("Herding")]
    public TrackArea currentArea;

    [Header("Tasks")]
    public Tasks selectedTask;
    public bool taskComplete;
    public int tasklessDays;

    [Header("Midday")]
    public float dayLength = 5;
    public float taskTime = 2.5f;
    public float currentTime;

    [Header("Transistion Times")]
    public float morningEnd;    // transistion into midday
    public float middayEnd;     // transistion into evening
    public float eveningEnd;    // day end

    private void Awake()
    {
        Instance = this;
        currentState = MorningState;
    }
    private void Start()
    {
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        currentState.UpdateState(this);
    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
