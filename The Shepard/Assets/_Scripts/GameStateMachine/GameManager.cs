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
    public float bringSheepBack;

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

        if (SheepTrackerManager.Instance.AtRequiredPlace(currentArea))
        {
            FarmerManager.Instance.SetFarmerTarget(FarmerManager.Instance.farmHouse);
        }

        if (currentTime >= bringSheepBack)
        {
            //FarmerManager.Instance.SetFarmerTarget()
            switch (currentArea)
            {
                case TrackArea.NorthPasture:
                    FarmerManager.Instance.SetFarmerTarget(FarmerManager.Instance.northPastureIn);
                    if (SheepTrackerManager.Instance.AtRequiredPlace(TrackArea.Pen))
                    {
                        FarmerManager.Instance.SetFarmerTarget(FarmerManager.Instance.farmHouse);
                    }
                    break;
                case TrackArea.WestPasture:
                    FarmerManager.Instance.SetFarmerTarget(FarmerManager.Instance.westPastureIn);
                    if (SheepTrackerManager.Instance.AtRequiredPlace(TrackArea.NorthPasture))
                    {
                        FarmerManager.Instance.SetFarmerTarget(FarmerManager.Instance.farmHouse);
                    }
                    break;
                case TrackArea.EastPasture:
                    FarmerManager.Instance.SetFarmerTarget(FarmerManager.Instance.eastPastureIn);
                    if (SheepTrackerManager.Instance.AtRequiredPlace(TrackArea.NorthPasture))
                    {
                        FarmerManager.Instance.SetFarmerTarget(FarmerManager.Instance.farmHouse);
                    }
                    break;
            }
        }

    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
}
