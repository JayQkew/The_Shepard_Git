using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistanceManager : MonoBehaviour
{
    public static AssistanceManager Instance { get; private set; }

    [Header("Herd Assistance Areas")]
    public GameObject barnTop;
    public GameObject penBot;
    public GameObject penTop;
    public GameObject pasture1Bot;
    public GameObject pasture1Left;
    public GameObject pasture1Right;
    public GameObject pasture2Right;
    public GameObject pasture3Left;

    private void Awake()
    {
        Instance = this;
    }

    public void ToNorthPasture()
    {
        barnTop.SetActive(true);
        penTop.SetActive(true);
        pasture2Right.SetActive(true);
        pasture3Left.SetActive(true);

        penBot.SetActive(false); 
        pasture1Bot.SetActive(false);
        pasture1Left.SetActive(false);
        pasture1Right.SetActive(false);
    }

    public void ToWestPasture()
    {
        barnTop.SetActive(true);
        penTop.SetActive(true);
        pasture1Left.SetActive(true);

        penBot.SetActive(false);
        pasture1Bot.SetActive(false);
        pasture1Right.SetActive(false);
        pasture2Right.SetActive(false);
        pasture3Left.SetActive(false);
    }

    public void ToEastPasture()
    {
        barnTop.SetActive(true);
        penTop.SetActive(true);
        pasture1Right.SetActive(true);
        pasture2Right.SetActive(true);

        penBot.SetActive(false);
        pasture1Bot.SetActive(false);
        pasture1Left.SetActive(false);
        pasture3Left.SetActive(false);
    }

    public void ToPen()
    {
        barnTop.SetActive(true);
        pasture1Bot.SetActive(true);
        pasture2Right.SetActive(true);
        pasture3Left.SetActive(true);

        penTop.SetActive(false);
        penBot.SetActive(false);
        pasture1Left.SetActive(false);
        pasture1Right.SetActive(false);
    }

    public void BackToBarn()
    {
        pasture1Bot.SetActive(true);
        pasture3Left.SetActive(true);
        pasture2Right.SetActive(true);
        penBot.SetActive(true);

        barnTop.SetActive(false);
        penTop.SetActive(false);
        pasture1Left.SetActive(false);
        pasture1Right.SetActive(false);
    }

}

public enum Pastures
{
    Pasture1,
    Pasture2,
    Pasture3
}
