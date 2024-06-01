using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FarmerShearingState : FarmerBaseState
{
    public override void EnterState(FarmerManager manager)
    {
        manager.farmerTarget = SheepPastureArea(manager);
        manager.openGate = true;
        manager.SetFarmerTarget(manager.farmerTarget); //where the sheep are currently
    }
    public override void UpgradeState(FarmerManager manager)
    {
        AreaOfEffect(manager);
        GameObject closestSheep = ClosestSheep(manager);

        if(manager.farmerNavAgent.remainingDistance <= 0.25f && SheepTracker.Instance.AtRequiredPlace(TrackArea.Pen))
        {
            manager.openGate = false;
            manager.SetFarmerTarget(manager.shearPosition);

            AssistanceManager.Instance.CloseAllGates();
        }

        //all sheep in pen = start chasing sheep
        if (SheepTracker.Instance.AtRequiredPlace(TrackArea.Pen) && GameManager.Instance.longWoolCount > 0)
        {
            //AuraEffect(manager);
            if (closestSheep != null)
            {
                if (closestSheep.GetComponent<SheepBehaviour>().sheepStats.woolLength == WoolLength.Long) manager.SetFarmerTarget(closestSheep.transform);
            }
            ShearSheep(manager);
        }


        if(GameManager.Instance.longWoolCount <= 0)
        {
            manager.farmerTarget = manager.barnOut;
            GameManager.Instance.targetArea = TrackArea.Barn;
            AssistanceManager.Instance.BackToBarn();
            manager.SwitchState(manager.FarmerGuideState);
        }
    }
    public override void FixedUpdateState(FarmerManager manager)
    {
        if (manager.farmerNavAgent.remainingDistance <= 1f && SheepTracker.Instance.AtRequiredPlace(TrackArea.Pen))
        {
            AuraEffect(manager);
        }
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
        foreach(GameObject agent in manager.sur_agents)
        {
            Vector3 force = PushForce(manager, agent) * manager.farmerPushForce;
            agent.GetComponent<Rigidbody>().AddForce(force * Time.deltaTime, ForceMode.Force);
        }
    }
    private Vector3 PushForce(FarmerManager manager, GameObject effectedAgent)
    {
        Vector3 dir = effectedAgent.transform.position - manager.farmer.transform.position;
        Vector3 forceNorm = Vector3.ClampMagnitude(dir, 1);
        return forceNorm;
    }
    public void AreaOfEffect(FarmerManager manager)
    {
        RaycastHit[] hit = Physics.SphereCastAll(manager.farmer.transform.position, manager.farmerAuraRadius, Vector3.up, 0, manager.sheepLayer);
        List<GameObject> sur_agents = new List<GameObject>();

        for (int i  = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != manager.farmer && !sur_agents.Contains(hit[i].transform.gameObject))
            {
                sur_agents.Add(hit[i].transform.gameObject);
            }
        }

        manager.sur_agents = sur_agents.ToArray();
    }
    public void ShearSheep(FarmerManager manager)
    {
        RaycastHit[] hit = Physics.SphereCastAll(manager.farmer.transform.position, 1, Vector3.up, 0, manager.sheepLayer);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != manager.farmer && hit[i].transform.GetComponent<SheepBehaviour>().sheepStats.woolLength == WoolLength.Long)
            {
                hit[i].transform.GetComponent<SheepBehaviour>().sheepStats.woolLength = WoolLength.None;
                hit[i].transform.GetComponent<SheepBehaviour>().CheckWool();
                hit[i].transform.GetComponent<SheepBehaviour>().shearedWool.Play();
                GameManager.Instance.longWoolCount--;

                Debug.Log($"{hit[i].transform.GetComponent<SheepBehaviour>().sheepStats.name} got sheared");
            }
        }
    }
    private Transform SheepPastureArea(FarmerManager manager)
    {
        int allSheepLength = SheepTracker.Instance.allSheep.Length;

        if (SheepTracker.Instance.northPasture.Count == allSheepLength) return manager.northPastureOut;
        else if (SheepTracker.Instance.eastPasture.Count == allSheepLength) return manager.eastPastureOut;
        else if (SheepTracker.Instance.barn.Count == allSheepLength) return manager.barnOut;
        else if (SheepTracker.Instance.pen.Count == allSheepLength) return manager.shearPosition;
        else return manager.shearPosition;
    }
}
