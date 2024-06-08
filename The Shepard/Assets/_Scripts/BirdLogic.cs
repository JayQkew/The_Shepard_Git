using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLogic : MonoBehaviour
{
    public bool scared;
    public GameObject scareAgent;

    public Rigidbody rb;
    public float upwardForce;
    public float awayForce;

    public Animator anim;

    private void Start()
    {
        SetStats();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        anim.SetBool("Scared", scared);

        if (!scared) AreaScan();

        if (transform.position.y > 30f) gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (scared && scareAgent != null) ScareBird(scareAgent);
    }

    public void ScareBird(GameObject agent)
    {
        Vector3 away = transform.position - agent.transform.position;
        Vector3 direction = away.normalized * awayForce;
        Vector3 flyForce = new Vector3(direction.x, upwardForce, direction.z);
        rb.AddForce(flyForce, ForceMode.Force);
    }

    public void AreaScan()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, BirdManager.Instance.areaRadius, Vector3.up, 0, BirdManager.Instance.scareAgents);

        if (hit.Length > 0 )
        {
            scared = true;
            scareAgent = hit[0].transform.gameObject;
            if(scareAgent.tag == "Player")
            {
                MissionManager.Instance.BarkAtBird();
            }
        }
    }

    private void SetStats()
    {
        awayForce = Random.Range(BirdManager.Instance.flyAwayRange.x, BirdManager.Instance.flyAwayRange.y);
        upwardForce = Random.Range(BirdManager.Instance.flyUpRanger.x, BirdManager.Instance.flyUpRanger.y);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, BirdManager.Instance.areaRadius);
    //}
}
