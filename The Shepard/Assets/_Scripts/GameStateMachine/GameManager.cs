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
    public TrackArea targetArea;

    [Header("Tasks")]
    public Tasks selectedTask;
    public bool taskComplete;
    public int tasklessDays;
    public GameObject penForceWall;

    public Vector2 longWoolRatio;
    public int shearTaskCount;
    public int longWoolCount;

    [Header("Midday")]
    public float dayLength = 5;
    public float currentTime;

    [Header("Transistion Times")]
    public float morningEnd;    // transistion into midday
    public float taskTime;
    public float bringSheepBack;
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

        if (currentTime >= bringSheepBack)
        {
            FarmerGuideBack();
        }

    }

    private void FarmerGuideBack()
    {
        switch (targetArea)
        {
            case TrackArea.NorthPasture:
                FarmerManager.Instance.farmerTarget = FarmerManager.Instance.northPastureIn;
                targetArea = TrackArea.Pen;
                FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerGuideState);
                break;
            case TrackArea.WestPasture:
                FarmerManager.Instance.farmerTarget = FarmerManager.Instance.westPastureIn;
                targetArea = TrackArea.NorthPasture;
                FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerGuideState);
                break;
            case TrackArea.EastPasture:
                FarmerManager.Instance.farmerTarget = FarmerManager.Instance.eastPastureIn;
                targetArea = TrackArea.NorthPasture;
                FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerGuideState);
                break;
        }

    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

}
