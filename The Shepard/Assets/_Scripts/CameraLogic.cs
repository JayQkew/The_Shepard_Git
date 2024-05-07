using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Transform subject;
    [SerializeField]
    private Vector3 lockedDistance;

    private void Start()
    {
        lockedDistance = transform.position;
    }
    private void Update()
    {
        FollowSubject();
    }

    public void FollowSubject()
    {
        transform.position = subject.position + lockedDistance;
        transform.LookAt(subject.position);
    }
}
