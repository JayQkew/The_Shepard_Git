using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        //all sheep in pen = start chasing sheep
        if (SheepTracker.Instance.AtRequiredPlace(TrackArea.Pen))
        {
            GameObject closestSheep = ClosestSheep(manager);

            if (closestSheep != null)
            {
                manager.SetFarmerTarget(closestSheep.transform);
            }
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
                sheep_distance.Add(hit[i].transform.gameObject, Vector3.Distance(manager.farmer.transform.position, hit[i].transform.position));
                closestDistance = Vector3.Distance(manager.farmer.transform.position, hit[i].transform.position);
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

        if(closestSheep != null)
        {
            Debug.Log(closestSheep.GetComponent<SheepBehaviour>().sheepStats.name);
        }

        return closestSheep;

    }
}
