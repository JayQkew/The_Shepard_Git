using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorialState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        manager.fadePanel.GetComponent<Animator>().SetBool("fadeIn", true);
        BirdManager.Instance.FindSpawnArea();
        SheepSpawner.Instance.Init_Herd();
        SheepTracker.Instance.allSheep = GameObject.FindGameObjectsWithTag("sheep");
    }

    public override void UpdateState(GameManager manager)
    {
        switch (manager.currentTutorial)
        {
            case TutorialState.Move:
                MoveState(manager);
                break;
            case TutorialState.Jump:
                JumpState(manager);
                break;
            case TutorialState.Sprint:
                SprintState(manager);
                break;
            case TutorialState.Scroll:
                ScrollState(manager);
                break;
            case TutorialState.HerdSheep:
                HerdState(manager);
                break;
            case TutorialState.Interact:
                InteractState(manager);
                break;
            case TutorialState.Bark:
                BarkState(manager);
                break;
        }
    }

    public override void ExitState(GameManager manager)
    {
        AssistanceManager.Instance.CloseAllGates();
        FarmerManager.Instance.farmer.transform.position = new Vector3(FarmerManager.Instance.farmHouse.position.x, FarmerManager.Instance.farmer.transform.position.y, FarmerManager.Instance.farmHouse.position.z);
        PlayerController.Instance.followingDuckens.Clear();
        PlayerController.Instance.gameObject.transform.position = PlayerController.Instance.startPos;
    }

    private void MoveState(GameManager manager)
    {
        //move notif
        if (!manager.pressed_W &&
            !manager.pressed_A &&
            !manager.pressed_S &&
            !manager.pressed_D)
        {
            NotificationManager.Instance.TutorialNotification(NotificationManager.Instance.tutorialNotifications[0]);
        }

        if (Input.GetKey(KeyCode.W)) manager.pressed_W = true;
        if (Input.GetKey(KeyCode.A)) manager.pressed_A = true;
        if (Input.GetKey(KeyCode.S)) manager.pressed_S = true;
        if (Input.GetKey(KeyCode.D)) manager.pressed_D = true;

        if (manager.pressed_W &&
            manager.pressed_A &&
            manager.pressed_S &&
            manager.pressed_D)
        {
            manager.currentTutorial = TutorialState.Jump;
        }

    }

    private void JumpState(GameManager manager)
    {
        // jump notif
        NotificationManager.Instance.TutorialNotification(NotificationManager.Instance.tutorialNotifications[1]);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.jumped = true;
            manager.currentTutorial = TutorialState.Sprint;
        }
    }

    private void SprintState(GameManager manager)
    {
        // sprint notif
        NotificationManager.Instance.TutorialNotification(NotificationManager.Instance.tutorialNotifications[2]);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            manager.sprinted = true;
            manager.currentTutorial = TutorialState.Scroll;
        }
    }

    private void ScrollState(GameManager manager)
    {
        //scroll notif
        NotificationManager.Instance.TutorialNotification(NotificationManager.Instance.tutorialNotifications[3]);

        if (Input.mouseScrollDelta.y > 0 ||
            Input.mouseScrollDelta.y < 0)
        {
            manager.scrolled = true;
            manager.currentTutorial = TutorialState.HerdSheep;
        }
    }

    private void HerdState(GameManager manager)
    {
        NotificationManager.Instance.TutorialNotification(NotificationManager.Instance.tutorialNotifications[4]);
        FarmerManager.Instance.farmerTarget = FarmerManager.Instance.shearPosition;
        FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerGuideState);
        GameManager.Instance.targetArea = TrackArea.Pen;
        AssistanceManager.Instance.ToPen();


        if (SheepTracker.Instance.AtRequiredPlace(TrackArea.Pen))
        {
            manager.herded = true;
            manager.currentTutorial = TutorialState.Interact;
        }
    }

    private void InteractState(GameManager manager)
    {
        NotificationManager.Instance.TutorialNotification(NotificationManager.Instance.tutorialNotifications[5]);

        if (manager.interacted)
        {
            manager.currentTutorial = TutorialState.Bark;
        }
    }

    private void BarkState(GameManager manager)
    {
        NotificationManager.Instance.TutorialNotification(NotificationManager.Instance.tutorialNotifications[6]);

        if (manager.barkTimes >= manager.bark_r) //make more specific
        {
            NotificationManager.Instance.RetractNotification();
            manager.FadeToMorning();
            manager.currentTutorial = TutorialState.Done;
            return;
        }
    }
}

public enum TutorialState
{
    Move,
    Sprint,
    Jump,
    Scroll,
    HerdSheep,
    Interact,
    Bark,
    Done
}
