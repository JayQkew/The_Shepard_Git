using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public GameObject[] sprites;

    private void Start()
    {
        sprites = GameObject.FindGameObjectsWithTag("GUI_Sprites");
    }

    private void Update()
    {
        foreach (var sprite in sprites)
        {
            sprite.transform.SetPositionAndRotation(sprite.transform.position, Quaternion.Euler(Camera.main.transform.rotation.x, sprite.transform.rotation.y, sprite.transform.rotation.z));
        }
    }


}
