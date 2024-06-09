using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTutorialState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
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
        FarmerManager.Instance.SwitchState(FarmerManager.Instance.FarmerGuideState);

        if (SheepTracker.Instance.AtRequiredPlace(TrackArea.Barn))
        {
            manager.herded = true;
            manager.currentTutorial = TutorialState.Interact;
        }
    }

    private void InteractState(GameManager manager)
    {
        NotificationManager.Instance.TutorialNotification(NotificationManager.Instance.tutorialNotifications[5]);

        if (Input.GetMouseButtonDown(1)) //make more specific
        {
            manager.currentTutorial = TutorialState.Bark;
        }
    }

    private void BarkState(GameManager manager)
    {
        NotificationManager.Instance.TutorialNotification(NotificationManager.Instance.tutorialNotifications[6]);

        if (Input.GetMouseButtonDown(0)) //make more specific
        {
            manager.SwitchState(manager.MorningState);
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
    Bark
}
