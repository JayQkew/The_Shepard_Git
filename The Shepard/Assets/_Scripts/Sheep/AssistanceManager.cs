using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistanceManager : MonoBehaviour
{
    public static AssistanceManager Instance { get; private set; }

    [Header("Herd Assistance Areas")]
    public GameObject barnRight;
    public GameObject penRight;
    public GameObject penTop;
    public GameObject penLeft;
    public GameObject pastureN_Bot;
    public GameObject pastureE_Left;

    private void Awake()
    {
        Instance = this;
    }

    public void ToNorthPasture()
    {
        barnRight.SetActive(true);
        penTop.SetActive(true);

        penLeft.SetActive(false);
        pastureE_Left.SetActive(false);
        penRight.SetActive(false);
        pastureN_Bot.SetActive(false);
    }
    public void ToEastPasture()
    {
        barnRight.SetActive(true);
        penRight.SetActive(true);

        penLeft.SetActive(false);
        penTop.SetActive(false);
        pastureN_Bot.SetActive(false);
        pastureE_Left.SetActive(false);
    }

    public void ToPen()
    {
        barnRight.SetActive(true);
        pastureN_Bot.SetActive(true);
        pastureE_Left.SetActive(true);

        penLeft.SetActive(false);
        penTop.SetActive(false);
        penRight.SetActive(false);
    }

    public void BackToBarn()
    {
        pastureN_Bot.SetActive(true);
        pastureE_Left.SetActive(true);
        penRight.SetActive(true);
        penLeft.SetActive(true);

        barnRight.SetActive(false);
        penTop.SetActive(false);
    }

}

public enum Pastures
{
    Pasture1,
    Pasture2,
    Pasture3
}
