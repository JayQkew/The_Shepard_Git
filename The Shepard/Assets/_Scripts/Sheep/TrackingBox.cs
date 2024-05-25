using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBox : MonoBehaviour
{
    [SerializeField] private TrackArea area;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sheep")
        {
            switch (area)
            {
                case TrackArea.Barn:
                    SheepTrackerManager.Instance.barn.Add(other.gameObject);
                    break;
                case TrackArea.Pen:
                    SheepTrackerManager.Instance.pen.Add(other.gameObject);
                    break;
                case TrackArea.NorthPasture:
                    SheepTrackerManager.Instance.pasture1.Add(other.gameObject);
                    break;
                case TrackArea.WestPasture:
                    SheepTrackerManager.Instance.pasture2.Add(other.gameObject);
                    break;
                case TrackArea.EastPasture:
                    SheepTrackerManager.Instance.pasture3.Add(other.gameObject);
                    break;
            }
        }

        if (other.tag == "Player")
        {
            switch (area)
            {
                case TrackArea.Barn:
                    PlayerController.Instance.playerArea = TrackArea.Barn;
                    break;
                case TrackArea.Pen:
                    PlayerController.Instance.playerArea = TrackArea.Pen;
                    break;
                case TrackArea.NorthPasture:
                    PlayerController.Instance.playerArea = TrackArea.NorthPasture;
                    break;
                case TrackArea.WestPasture:
                    PlayerController.Instance.playerArea = TrackArea.WestPasture;
                    break;
                case TrackArea.EastPasture:
                    PlayerController.Instance.playerArea = TrackArea.EastPasture;
                    break;
                case TrackArea.OpenField:
                    PlayerController.Instance.playerArea = TrackArea.OpenField;
                    break;
            }

            CameraLogic.Instance.ChangeCameraDistance();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "sheep")
        {
            switch (area)
            {
                case TrackArea.Barn:
                    SheepTrackerManager.Instance.barn.Remove(other.gameObject);
                    break;
                case TrackArea.Pen:
                    SheepTrackerManager.Instance.pen.Remove(other.gameObject);
                    break;
                case TrackArea.NorthPasture:
                    SheepTrackerManager.Instance.pasture1.Remove(other.gameObject);
                    break;
                case TrackArea.WestPasture:
                    SheepTrackerManager.Instance.pasture2.Remove(other.gameObject);
                    break;
                case TrackArea.EastPasture:
                    SheepTrackerManager.Instance.pasture3.Remove(other.gameObject);
                    break;
            }
        }

        if (other.tag == "Player")
        {
            CameraLogic.Instance.ChangeCameraDistance();
        }
    }
}
