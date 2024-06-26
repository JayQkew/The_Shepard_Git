using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEveningState : GameBaseState
{
    public override void EnterState(GameManager manager)
    {
        Debug.Log("Evening");
        AssistanceManager.Instance.BackToBarn();
    }

    public override void UpdateState(GameManager manager)
    {
        if (manager.currentTime >= manager.eveningEnd - 5)
        {
            manager.fadePanel.GetComponent<Animator>().SetBool("fadeIn", false);
        }
        
        if (manager.currentTime >= manager.eveningEnd - 12 && !manager.eveningMusicEnded)
        {
            MusicManager.Instance.FadeMusicOut();
            manager.eveningMusicEnded = true;
        }

        if (manager.currentTime >= manager.eveningEnd)
        {
            manager.SwitchState(manager.MorningState);
        }
    }

    public override void ExitState(GameManager manager)
    {
        AssistanceManager.Instance.CloseAllGates();
        FarmerManager.Instance.farmer.transform.position = new Vector3(FarmerManager.Instance.farmHouse.position.x, FarmerManager.Instance.farmer.transform.position.y, FarmerManager.Instance.farmHouse.position.z);
        if (SheepTracker.Instance.AtRequiredPlace(TrackArea.Barn) && manager.atTargetArea)
        {
            MissionManager.Instance.SheepHerded();
            manager.dayComplete = true;
            manager.day++;
        }
        else
        {
            manager.dayComplete = false;
        }
        PlayerController.Instance.followingDuckens.Clear();
        PlayerController.Instance.gameObject.transform.position = PlayerController.Instance.startPos;
        FarmerManager.Instance.whistle = false;
        manager.taskComplete = false;
        manager.currentTime = 0;
        manager.atTargetArea = false;

        manager.n_herdIn = false;
        manager.n_herdOut = false;
        manager.n_shearIn = false;

        manager.eveningMusicStarted = false;
        manager.eveningMusicEnded = false;
    }
}
