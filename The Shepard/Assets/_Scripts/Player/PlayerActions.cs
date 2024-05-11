using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    [Header("Bark 1")]
    public GameObject[] bark1_affectedAgents;
    [SerializeField]
    private float bark1_r;
    [SerializeField]
    private float bark1_strength;

    [Header("Bark 2")]
    public GameObject[] bark2_affectedAgents;
    [SerializeField]
    private float bark2_strength;
    [SerializeField]
    private GameObject boxPivot;
    [SerializeField]
    private Transform box;

    [Space(10)]
    [SerializeField]
    private LayerMask effectedAgents;
    [SerializeField]
    private LayerMask ground;

    private void Update()
    {
        Bark2_Box();

        Bark1_AoE();
        Bark2_AoE();
    }

    #region Bark 1
    public void Bark1()
    {
        foreach (GameObject agent in bark1_affectedAgents)
        {
            Vector3 force = Bark1_Force(agent.transform.position) * bark1_strength;
            agent.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            agent.GetComponent<SheepBehaviour>().inAura = true;
            agent.GetComponent<SheepBehaviour>().startled = true;
        }

        BoidsManager.Instance.AddToBoids(bark1_affectedAgents);
    }
    private Vector3 Bark1_Force(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        Vector3 forceNorm = Vector3.ClampMagnitude(dir, 1);
        return forceNorm;
    }
    private void Bark1_AoE()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, bark1_r, Vector3.up, 0, effectedAgents);
        List<GameObject> sur_agents = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject) sur_agents.Add(hit[i].transform.gameObject);
        }

        bark1_affectedAgents = sur_agents.ToArray();
    }

    #endregion

    #region Bark 2
    public void Bark2()
    {
        foreach (GameObject agent in bark2_affectedAgents)
        {
            agent.GetComponent<Rigidbody>().AddForce(Bark2_Force() * bark2_strength, ForceMode.Impulse);
            agent.GetComponent<SheepBehaviour>().inAura = true;
            agent.GetComponent<SheepBehaviour>().startled = true;
        }

        BoidsManager.Instance.AddToBoids(bark2_affectedAgents);
    }

    private Vector3 Bark2_Force()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePos - playerPos;
        Vector3 worldDir = new Vector3(direction.x, 0, direction.y);
        
        return Vector3.ClampMagnitude(worldDir, 1);
    }

    private void Bark2_AoE()
    {
        RaycastHit[] hit = Physics.BoxCastAll(box.position, box.lossyScale/2, Vector3.up, Quaternion.identity, 0, effectedAgents);
        List<GameObject> sur_agents = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject && bark1_affectedAgents.Contains(hit[i].transform.gameObject))
            {
                sur_agents.Add(hit[i].transform.gameObject);
            }
        }

        bark2_affectedAgents = sur_agents.ToArray();

    }

    private void Bark2_Box()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = mousePosition - playerPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        boxPivot.transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bark1_r);
    }
}
