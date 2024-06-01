using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateTriggerLogic : MonoBehaviour
{
    public GameObject gate;
    public OpenDirection gateOpenDirection;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "farmer")
        {
            if(!FarmerManager.Instance.openGate) AssistanceManager.Instance.GateClose(gate);
            else
            {
                if (gateOpenDirection == OpenDirection.Out) AssistanceManager.Instance.GateOpenOut(gate);
                else if (gateOpenDirection == OpenDirection.In) AssistanceManager.Instance.GateOpenIn(gate);
            }
        }
    }
}

public enum OpenDirection
{
    In,
    Out
}
