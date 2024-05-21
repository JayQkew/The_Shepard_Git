using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private Light sceneLight;

    [SerializeField]
    private Gradient dayLight;

    private void Update()
    {
        sceneLight.color = dayLight.Evaluate(GameManager.Instance.currentTime / GameManager.Instance.eveningEnd);
    }
}
