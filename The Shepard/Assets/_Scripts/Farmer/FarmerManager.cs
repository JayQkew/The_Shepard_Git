using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerManager : MonoBehaviour
{
    public static FarmerManager Instance { get; private set; }

    public GameObject farmer;
    public NavMeshAgent farmerNavAgent;
    public Transform farmerTarget;
    public LayerMask sheepLayer;
    public float sheepChaseRadius;
    public float farmerAuraRadius;
    public float farmerPushForce;
    public bool arriveAtDestination;

    #region Farmer States
    public FarmerBaseState currentState;
    public FarmerGuideState FarmerGuideState = new FarmerGuideState();
    public FarmerShearingState FarmerShearingState = new FarmerShearingState();
    public FarmerChillState FarmerChillState = new FarmerChillState();
    #endregion

    #region Farmer Positions
    [Header("Farmer Posisitons")]
    public Transform farmHouse;
    public Transform northPastureIn;
    public Transform northPastureOut;
    public Transform westPastureIn;
    public Transform westPastureOut;
    public Transform eastPastureIn;
    public Transform eastPastureOut;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentState = FarmerGuideState;
        farmerNavAgent = farmer.GetComponent<NavMeshAgent>();
        farmerNavAgent.updateRotation = false;
    }

    private void Update()
    {
        currentState.UpgradeState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(FarmerBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void SetFarmerTarget(Transform target)
    {
        farmerNavAgent.SetDestination(target.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(farmer.transform.position, sheepChaseRadius);
    }
}
