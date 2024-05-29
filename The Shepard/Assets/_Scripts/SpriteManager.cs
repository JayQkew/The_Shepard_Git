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

    public IEnumerator FlipSprite(GameObject sprite, int direction, float targetTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < targetTime)
        {
            elapsedTime += Time.deltaTime;
            sprite.transform.localScale = Vector3.MoveTowards(sprite.transform.localScale, new Vector3(sprite.transform.localScale.x * direction, sprite.transform.localScale.y, sprite.transform.localScale.z), 1000);
            yield return null;
        }
    }

}
