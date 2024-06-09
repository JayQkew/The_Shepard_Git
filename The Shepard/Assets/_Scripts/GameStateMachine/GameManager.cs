using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameBaseState currentState;
    public GameTutorialState TutorialState = new GameTutorialState();
    public GameMorningState MorningState = new GameMorningState();
    public GameMiddayState MiddayState = new GameMiddayState();
    public GameTaskState TaskState = new GameTaskState();
    public GameEveningState EveningState = new GameEveningState();

    public bool dayComplete;
    public int day;
    public List<int> sheepCountProgression = new List<int>();
    public int sheepCap;
    public GameObject fadePanel;

    [Header("Tutorial")]
    public TutorialState currentTutorial;
    [Space(5)]
    public bool pressed_W;
    public bool pressed_A;
    public bool pressed_S;
    public bool pressed_D;
    [Space(5)]
    public bool jumped;
    [Space(5)]
    public bool sprinted;
    [Space(5)]
    public bool scrolled;
    [Space(5)]
    public bool herded;
    [Space(5)]
    public bool interacted;
    [Space(5)]
    public int barkTimes;
    public int bark_r;

    [Header("Herding")]
    public bool morningMusicStopped;
    public TrackArea targetArea;
    public bool atTargetArea;

    [Header("Tasks")]
    public Tasks selectedTask;
    public bool taskComplete;
    public GameObject penForceWall;

    public Vector2 longWoolRatio;
    public int shearTaskCount;
    public int longWoolCount;

    [Header("Notification Times")]
    public bool n_herdOut;
    public float herdOutNotification;
    public bool n_herdIn;
    public float herdInNortification;
    public bool n_shearIn;
    public float shearNotification;


    [Header("Transistion Times")]
    public float currentTime;
    [Space(5)]
    public float morningEnd;    // transistion into midday
    public float taskTime;
    public float bringSheepBack;
    public float middayEnd;     // transistion into evening
    public float eveningEnd;    // day end

    private void Awake()
    {
        Instance = this;
        currentState = TutorialState;
    }
    private void Start()
    {
        currentState.EnterState(this);
    }

    private void Update()
    {
        if (currentState != TutorialState)
        {
            currentTime += Time.deltaTime;
        }

        currentState.UpdateState(this);

        if (currentTime >= bringSheepBack)
        {
            FarmerGuideBack();
        }

        if (currentTime >= herdOutNotification && !n_herdOut)
        {
            NotificationManager.Instance.Notification(NotificationManager.Instance.dayNotifications[0]);
            n_herdOut = true;
        }
        else if (currentTime >= shearNotification && !n_shearIn && selectedTask != Tasks.None)
        {
            NotificationManager.Instance.Notification(NotificationManager.Instance.dayNotifications[2]);
            n_shearIn = true;
        }
        else if (currentTime >= herdInNortification && !n_herdIn)
        {
            NotificationManager.Instance.Notification(NotificationManager.Instance.dayNotifications[1]);
            n_herdIn = true;
        }
    }

    private void FarmerGuideBack()
    {
        switch (targetArea)
        {
            case TrackArea.NorthPasture:
                FarmerManager.Instance.farmerTarget = FarmerManager.Instance.northPastureIn;
                targetArea = TrackArea.Barn;
                FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerGuideState);
                break;
            case TrackArea.EastPasture:
                FarmerManager.Instance.farmerTarget = FarmerManager.Instance.eastPastureIn;
                targetArea = TrackArea.Barn;
                FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerGuideState);
                break;
        }

        AssistanceManager.Instance.BackToBarn();
    }

    public void SwitchState(GameBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void FadeToMorning()
    {
        StartCoroutine(FadeToMorn());
    }

    private IEnumerator FadeToMorn() 
    {
        fadePanel.GetComponent<Animator>().SetBool("fadeIn", false);
        yield return new WaitForSeconds(3);
        SwitchState(MorningState);
    }
}
