using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerManager : MonoBehaviour
{
    public static FarmerManager Instance { get; private set; }

    public GameObject farmer;
    private NavMeshAgent farmerNavAgent;
    public Transform farmerTarget;

    [Header("Farmer Posisitons")]
    public Transform farmHouse;
    public Transform northPastureIn;
    public Transform northPastureOut;
    public Transform westPastureIn;
    public Transform westPastureOut;
    public Transform eastPastureIn;
    public Transform eastPastureOut;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        farmerNavAgent = farmer.GetComponent<NavMeshAgent>();
        farmerNavAgent.updateRotation = false;
    }

    private void Update()
    {
    }

    public void SetFarmerTarget(Transform target)
    {
        farmerNavAgent.SetDestination(target.position);
    }
}
