using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerLogic : MonoBehaviour
{
    public GameObject gate;
    OpenDirection openDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "farmer")
        {
            if(FarmerManager.Instance.pushDoor) AssistanceManager.Instance.GateOpenOut(gate);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "farmer")
        {
            if(!FarmerManager.Instance.pushDoor) AssistanceManager.Instance.GateClose(gate);
        }
    }
}

public enum OpenDirection
{
    In,
    Out,
    Close
}
