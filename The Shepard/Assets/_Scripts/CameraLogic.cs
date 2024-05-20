using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    public static CameraLogic Instance { get; private set; }

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform subject;
    [SerializeField]
    private Vector3 closeDistance;
    [SerializeField]
    private Vector3 midDistance;
    [SerializeField]
    private Vector3 farDistance;
    [SerializeField]
    public CameraDistances camDistance;
    private Vector3 currentDistance;
    private Vector3 targetDistance;

    public float lerpDuration;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        closeDistance = transform.position;
        midDistance = closeDistance * 1.5f;
        farDistance = closeDistance * 2;

        currentDistance = closeDistance;
        ChangeCameraDistance();

    }
    private void Update()
    {
        FollowSubject();
    }

    public void ChangeCameraDistance()
    {
        switch (PlayerController.Instance.playerArea)
        {
            case TrackArea.Barn:
                camDistance = CameraDistances.Close;
                targetDistance = closeDistance;
                break;
            case TrackArea.Pen:
                camDistance = CameraDistances.Mid;
                targetDistance = midDistance;
                break;
            default:
                camDistance = CameraDistances.Far;
                targetDistance = farDistance;
                break;
        }
    }

    public void FollowSubject()
    {

        currentDistance = Vector3.Lerp(currentDistance, targetDistance, lerpDuration * Time.deltaTime);

        transform.position = subject.position + currentDistance;

        transform.LookAt(subject.position);
    }

    public void CameraZoomControl(float change)
    {
        if (targetDistance.magnitude <= closeDistance.magnitude)
        {
            targetDistance = closeDistance;
        }
        else if (targetDistance.magnitude >= farDistance.magnitude)
        {
            targetDistance = farDistance;
        }

        float differenceY = change * targetDistance.y / (targetDistance.y + targetDistance.z);
        float differenceZ = change * targetDistance.z / (targetDistance.y + targetDistance.z);

        targetDistance = new Vector3(0, targetDistance.y + differenceY, targetDistance.z + differenceZ);
    }
}

public enum CameraDistances
{
    Close,
    Mid,
    Far
}
