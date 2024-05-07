using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public GameObject[] bark1_affectedAgents;
    public GameObject[] bark2_affectedAgents;

    [SerializeField]
    private float bark1_r;
    [SerializeField]
    private float bark1_strength;

    [SerializeField]
    private GameObject boxPivot;
    [SerializeField]
    private Transform box;

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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, bark1_r);
    }
}
