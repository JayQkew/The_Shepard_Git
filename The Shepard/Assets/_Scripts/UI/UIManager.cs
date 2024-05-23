using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject pausePanel;
    public GameObject cosmeticPanel;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActivateUI(pausePanel);
            PauseUI();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ActivateUI(cosmeticPanel);
            CosmeticManager.Instance.GetSlots();
        }
    }

    public void ActivateUI(GameObject uiPanel)
    {
        if (uiPanel.activeSelf == false)
        {
            uiPanel.SetActive(true);
        }
        else
        {
            uiPanel.SetActive(false);
        }
    }

    public void PauseUI()
    {
        if (pausePanel.activeSelf) Pause();
        else Resume();
    }

    public void Pause() => Time.timeScale = 0;
    public void Resume() => Time.timeScale = 1;
}
