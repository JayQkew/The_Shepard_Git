using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager Instance { get; private set; }
    public GameObject[] sprites;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        sprites = GameObject.FindGameObjectsWithTag("GUI_Sprites");
        SetSpriteRotation();
    }

    public void SetSpriteRotation()
    {
        foreach (var sprite in sprites)
        {
            Quaternion q = Quaternion.Euler(Camera.main.transform.rotation.x, sprite.transform.rotation.y, sprite.transform.rotation.z);
            sprite.transform.SetPositionAndRotation(sprite.transform.position, q);
        }
    }

    public void SpriteUpdate(GameObject subject)
    {
        List<GameObject> newSprites = sprites.ToList();
        newSprites.Add(subject.transform.GetChild(0).gameObject);
        sprites = newSprites.ToArray();
        SetSpriteRotation();
    }

}
