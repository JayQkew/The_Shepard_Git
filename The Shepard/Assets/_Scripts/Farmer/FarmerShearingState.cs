using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FarmerShearingState : FarmerBaseState
{
    public override void EnterState(FarmerManager manager)
    {
        manager.farmerTarget = manager.northPastureOut;
        manager.SetFarmerTarget(manager.farmerTarget);
        Debug.Log("Farmer in Shearing State");
    }
    public override void UpgradeState(FarmerManager manager)
    {
        GameObject closestSheep = ClosestSheep(manager);

        //all sheep in pen = start chasing sheep
        if (SheepTracker.Instance.AtRequiredPlace(TrackArea.Pen))
        {
            if (closestSheep != null)
            {
                if (closestSheep.GetComponent<SheepBehaviour>().sheepStats.woolLength == WoolLength.Long)
                {
                    manager.SetFarmerTarget(closestSheep.transform);
                }

            }
            ShearSheep(manager);
        }
    }
    public override void FixedUpdateState(FarmerManager manager)
    {
        AuraEffect(manager);
    }

    public override void ExitState(FarmerManager manager)
    {
    }

    public GameObject ClosestSheep(FarmerManager manager)
    {
        RaycastHit[] hit = Physics.SphereCastAll(manager.farmer.transform.position, manager.sheepChaseRadius, Vector3.up, 0, manager.sheepLayer);
        Dictionary<GameObject, float> sheep_distance = new Dictionary<GameObject, float>();
        GameObject closestSheep = null;
        float closestDistance = 0;

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != manager.farmer)
            {
                if (hit[i].transform.GetComponent<SheepBehaviour>().sheepStats.woolLength == WoolLength.Long)
                {
                    sheep_distance.Add(hit[i].transform.gameObject, Vector3.Distance(manager.farmer.transform.position, hit[i].transform.position));
                    closestDistance = Vector3.Distance(manager.farmer.transform.position, hit[i].transform.position);

                }
            }
        }



        foreach(KeyValuePair<GameObject, float> pair in sheep_distance)
        {
            if(pair.Value <= closestDistance)
            {
                closestDistance = pair.Value;
                closestSheep = pair.Key;
            }
        }

        //if(closestSheep != null)
        //{
        //    Debug.Log(closestSheep.GetComponent<SheepBehaviour>().sheepStats.name);
        //}

        return closestSheep;

    }


    private void AuraEffect(FarmerManager manager)
    {
        foreach(GameObject agent in AreaOfEffect(manager))
        {
            Vector3 force = PushForce(manager, agent) * manager.farmerPushForce;
            agent.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        }
    }

    private Vector3 PushForce(FarmerManager manager, GameObject effectedAgent)
    {
        Vector3 dir = effectedAgent.transform.position - manager.farmer.transform.position;
        Vector3 forceNorm = Vector3.ClampMagnitude(dir, 1);
        return forceNorm;
    }

    public GameObject[] AreaOfEffect(FarmerManager manager)
    {
        RaycastHit[] hit = Physics.SphereCastAll(manager.farmer.transform.position, manager.farmerAuraRadius, Vector3.up, 0, manager.sheepLayer);
        List<GameObject> sur_agents = new List<GameObject>();

        for (int i  = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != manager.farmer)
            {
                sur_agents.Add(hit[i].transform.gameObject);
            }
        }

        return sur_agents.ToArray();
    }


    //Shear the sheep if collision occurs!
    public void ShearSheep(FarmerManager manager)
    {
        RaycastHit[] hit = Physics.SphereCastAll(manager.farmer.transform.position, 1, Vector3.up, 0, manager.sheepLayer);
        List<GameObject> sur_agents = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != manager.farmer && hit[i].transform.GetComponent<SheepBehaviour>().sheepStats.woolLength == WoolLength.Long)
            {
                hit[i].transform.GetComponent<SheepBehaviour>().sheepStats.woolLength = WoolLength.None;
                GameManager.Instance.longWoolCount--;
                Debug.Log($"{hit[i].transform.GetComponent<SheepBehaviour>().sheepStats.name} got sheared");
            }
        }

    }
}
