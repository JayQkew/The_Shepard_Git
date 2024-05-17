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
                case TrackArea.Pasture1:
                    SheepTrackerManager.Instance.pasture1.Add(other.gameObject);
                    break;
                case TrackArea.Pasture2:
                    SheepTrackerManager.Instance.pasture2.Add(other.gameObject);
                    break;
                case TrackArea.Pasture3:
                    SheepTrackerManager.Instance.pasture3.Add(other.gameObject);
                    break;
            }
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
                case TrackArea.Pasture1:
                    SheepTrackerManager.Instance.pasture1.Remove(other.gameObject);
                    break;
                case TrackArea.Pasture2:
                    SheepTrackerManager.Instance.pasture2.Remove(other.gameObject);
                    break;
                case TrackArea.Pasture3:
                    SheepTrackerManager.Instance.pasture3.Remove(other.gameObject);
                    break;
            }
        }
    }
}
