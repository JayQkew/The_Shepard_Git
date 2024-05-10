using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    [Header("Spawn Matrix")]
    public GameObject point;
    public Vector2 matrixMetrics;
    public Vector2 matrixDistance = new Vector2(1, 1);
    public List<Vector3> sheepSpawnPoints = new List<Vector3>();

    [Header("Sheep")]
    public GameObject p_sheep;
    public Transform sheepParent;
    public GameObject[] activeSheep;
    public GameObject[] spawnedSheep; // same length as the sheep spawns


    private void Awake()
    {
        activeSheep = GameObject.FindGameObjectsWithTag("sheep");
    }
    private void Start()
    {
        Gen_SpawnMatrix();
        SpawnSheepHerd();
        //SpawnSheep();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnSheepHerd();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            AddSheep(1);
        }
    }

    #region Matrix

    private void Gen_SpawnMatrix()
    {
        sheepSpawnPoints.Clear();
        CreateMatrix();
        CenterMatix();
        Init_Spawners();
    }

    private void CreateMatrix()
    {
        Vector3 pos = Vector3.zero;
        for (int x = 0; x < matrixMetrics.x; x++)
        {
            for (int y = 0; y < matrixMetrics.y; y++)
            {
                sheepSpawnPoints.Add(pos);
                pos.z += matrixDistance.y;
            }
            pos.z = 0;
            pos.x += matrixDistance.x;
        }
    }

    private void CenterMatix()
    {
        float halfX = sheepSpawnPoints[sheepSpawnPoints.Count - 1].x / 2;
        float halfZ = sheepSpawnPoints[sheepSpawnPoints.Count - 1].z / 2;

        for (int i = 0; i < sheepSpawnPoints.Count; i++)
        {
            sheepSpawnPoints[i] -= new Vector3(halfX, 0, halfZ);
        }
    }

    private void Init_Spawners()
    {
        foreach (Vector3 spawnPoint in sheepSpawnPoints)
        {
            GameObject initSpawn = Instantiate(point, Vector3.zero, Quaternion.identity, transform);
            initSpawn.transform.localPosition = spawnPoint;
        }
    }

    #endregion

    #region Sheep Spawning

    public void AddSheep(int num)
    {
        for (int i = 0;i < num;i++)
        {
            GameObject sheep = Instantiate(p_sheep, sheepParent);
            SpriteManager.Instance.SpriteUpdate(sheep);
            BoidsManager.Instance.boids.Add(sheep);

            List<GameObject> newSheep = activeSheep.ToList();
            newSheep.Add(sheep);
            activeSheep = newSheep.ToArray();
        }

        SpawnSheepHerd();

    }
    public void SpawnSheepHerd()
    {
        spawnedSheep = new GameObject[sheepSpawnPoints.Count];

        foreach (GameObject sheep in activeSheep)
        {
            FindPos(sheep);
        }
    }
    private bool FindPos(GameObject sheep)
    {
        int x = Random.Range(0, sheepSpawnPoints.Count -1);
        if (spawnedSheep[x] == null)
        {
            sheep.transform.position = sheepSpawnPoints[x];
            spawnedSheep[x] = sheep;
            return true;
        }
        else
        {
            return FindPos(sheep);
        }
    }

    #endregion
}
