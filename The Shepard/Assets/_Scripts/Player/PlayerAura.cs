using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAura : MonoBehaviour
{
    public static PlayerAura Instance { get; private set; }
    private PlayerMovement PlayerMovement;

    public GameObject[] affectedAgents;

    public float aura_r;
    [Space(10)]
    [SerializeField]
    private float walkAura;
    [SerializeField]
    private float sprintAura;
    [SerializeField]
    private float crawlAura;
    [Space(10)]
    [SerializeField]
    private float aura_f;

    [SerializeField]
    private LayerMask effectedAgents;

    private void Awake()
    {
        Instance = this;
        PlayerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        UpdateRadius();
    }
    private void Update()
    {
        UpdateRadius();
        AreaOfEffect();
    }

    private void FixedUpdate()
    {
        AuraEffect();
    }

    private void UpdateRadius()
    {
        switch (PlayerMovement.moveState)
        {
            case MoveState.Walk:
                aura_r = walkAura;
                break;
            case MoveState.Sprint:
                aura_r = sprintAura;
                break;
            case MoveState.Crawl:
                aura_r = crawlAura;
                break;
        }
    }

    private void AuraEffect()
    {
        foreach (GameObject agent in affectedAgents)
        {
            Vector3 force = PushForce(agent) * aura_f;
            agent.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        }
    }

    private Vector3 PushForce(GameObject effectedAgent)
    {
        Vector3 dir = effectedAgent.transform.position - transform.position;
        Vector3 forceNorm = Vector3.ClampMagnitude(dir, 1);
        return forceNorm;
    }

    private void AreaOfEffect()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, aura_r, Vector3.up, 0, effectedAgents);
        List<GameObject> sur_agents = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject)
            {
                hit[i].transform.GetComponent<SheepBehaviour>().inAura = true;
                sur_agents.Add(hit[i].transform.gameObject);
            }
        }

        affectedAgents = sur_agents.ToArray();

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, aura_r);
    }
}
