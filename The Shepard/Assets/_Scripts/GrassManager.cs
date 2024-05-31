using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public Sprite[] grassSprites;
    public GameObject[] grass;

    private void Start()
    {
        foreach (GameObject _grass in grass)
        {
            _grass.GetComponent<SpriteRenderer>().sprite = grassSprites[Random.Range(0, grassSprites.Length)];
        }
    }
}
