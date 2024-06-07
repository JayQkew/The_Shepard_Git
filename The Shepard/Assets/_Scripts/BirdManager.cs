using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdManager : MonoBehaviour
{
    public static BirdManager Instance { get; private set; }



    public GameObject p_bird;
    public List<GameObject> allBirds = new List<GameObject>();
    public Transform[] spawnAreas;
    public float spawnAreaRadius;

    public Vector2 flyUpRanger;
    public Vector2 flyAwayRange;

    public float areaRadius;
    public LayerMask scareAgents;

    private void Awake()
    {
        Instance = this;
    }

    public void FindSpawnArea()
    {
        Transform spawnPos = spawnAreas[Random.Range(0, spawnAreas.Length)];
        foreach (GameObject bird in allBirds)
        {
            bird.SetActive(true);
            bird.GetComponent<BirdLogic>().scared = false;
            bird.GetComponent<BirdLogic>().scareAgent = null;
            bird.GetComponent<Rigidbody>().velocity = Vector2.zero;
            Vector2 randomArea = (Random.insideUnitCircle * spawnAreaRadius);
            Vector3 spawn = new Vector3 (spawnPos.position.x - randomArea.x, spawnPos.position.y, spawnPos.position.z - randomArea.y);
            bird.transform.position = spawn;
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Transform pos in spawnAreas)
        {
            Gizmos.DrawWireSphere(pos.position, spawnAreaRadius);
        }
    }
}
