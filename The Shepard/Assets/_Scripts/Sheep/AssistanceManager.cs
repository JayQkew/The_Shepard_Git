using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class AssistanceManager : MonoBehaviour
{
    public static AssistanceManager Instance { get; private set; }

    public bool debugging;
    [Header("Herd Assistance Areas")]
    public GameObject barnRight;
    public GameObject penRight;
    public GameObject penTop;
    public GameObject penLeft;
    public GameObject pastureN_Bot;
    public GameObject pastureE_Left;

    [Header("Herding Gates")]
    public GameObject barnGate;
    public GameObject pastureN_gate;
    public GameObject pastureE_gate;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
    }

    public void ToNorthPasture()
    {
        barnRight.SetActive(true);
        penTop.SetActive(true);

        penLeft.SetActive(false);
        pastureE_Left.SetActive(false);
        penRight.SetActive(false);
        pastureN_Bot.SetActive(false);

        if (GameManager.Instance.currentTime >= GameManager.Instance.bringSheepBack)
        {
            barnGate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.In;
            pastureN_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.In;
        }
        else
        {
            barnGate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.Out;
            pastureN_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.Out;
        }
        pastureE_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.Out;
    }
    public void ToEastPasture()
    {
        barnRight.SetActive(true);
        penRight.SetActive(true);

        penLeft.SetActive(false);
        penTop.SetActive(false);
        pastureN_Bot.SetActive(false);
        pastureE_Left.SetActive(false);

        if (GameManager.Instance.currentTime >= GameManager.Instance.bringSheepBack)
        {
            barnGate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.In;
            pastureN_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.In;
        }
        else
        {
            barnGate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.Out;
            pastureN_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.Out;
        }
        pastureE_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.Out;
    }
    public void ToPen()
    {
        barnRight.SetActive(true);
        pastureN_Bot.SetActive(true);
        pastureE_Left.SetActive(true);

        penLeft.SetActive(false);
        penTop.SetActive(false);
        penRight.SetActive(false);

        barnGate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.Out;
        pastureN_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.In;
        pastureE_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.In;
    }
    public void BackToBarn()
    {
        pastureN_Bot.SetActive(true);
        pastureE_Left.SetActive(true);
        penRight.SetActive(false);
        penLeft.SetActive(true);

        barnRight.SetActive(false);
        penTop.SetActive(false);

        barnGate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.In;
        pastureN_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.In;
        pastureE_gate.GetComponentInChildren<GateTriggerLogic>().gateOpenDirection = OpenDirection.In;
    }

    public void CloseAllGates()
    {
        GateClose(barnGate);
        GateClose(pastureN_gate);
        GateClose(pastureE_gate);
    }

    public void GateClose(GameObject gate) => gate.GetComponent<Animator>().SetInteger("Angle", 0);
    public void GateOpenOut(GameObject gate) => gate.GetComponent<Animator>().SetInteger("Angle", 1);
    public void GateOpenIn(GameObject gate) => gate.GetComponent<Animator>().SetInteger("Angle", 2);
}

public enum Pastures
{
    Pasture1,
    Pasture2,
    Pasture3
}
