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
    public float shearRadius;
    public bool openGate;
    public bool interacted;
    public bool whistle;

    public GameObject[] sur_agents;

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
    public Transform eastPastureIn;
    public Transform eastPastureOut;
    public Transform shearPosition;
    public Transform barnOut;
    #endregion

    [Header("Audio")]
    public AudioClip[] shearSounds;
    public AudioClip[] hmmSounds;

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

    public void PlayHmmSound()
    {
        farmer.GetComponent<AudioSource>().clip = hmmSounds[Random.Range(0, hmmSounds.Length)];
        farmer.GetComponent<AudioSource>().Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(farmer.transform.position, sheepChaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(farmer.transform.position, shearRadius);
    }
}
