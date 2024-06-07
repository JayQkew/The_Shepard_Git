using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public static SheepSpawner Instance { get; private set; }

    [Header("Spawn Matrix")]
    public bool destroyAfterUse;
    public GameObject point;
    public Vector2 matrixMetrics;
    public Vector2 matrixDistance = new Vector2(1, 1);
    public List<Vector3> sheepSpawnPoints = new List<Vector3>();
    public Transform[] points;

    [Header("Sheep")]
    public GameObject p_sheep;
    public Transform sheepParent;
    public GameObject[] activeSheep;
    public bool[] spawnedSheep; // same length as the sheep spawns


    private void Awake()
    {
        Instance = this;
        activeSheep = GameObject.FindGameObjectsWithTag("sheep");
    }

    public void Init_Herd()
    {
        BoidsManager.Instance.AddToBoids();
        if (destroyAfterUse) Gen_SpawnMatrix();
        SpawnSheepHerd();
        if (destroyAfterUse) DestroyMatrix();
    }

    public void Init_Sheep(int num)
    {
        BoidsManager.Instance.AddToBoids();
        if (destroyAfterUse) Gen_SpawnMatrix();
        AddSheep(num);
        if (destroyAfterUse) DestroyMatrix();
    }

    #region Matrix

    public void Gen_SpawnMatrix()
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
        List<Transform> spawners = new List<Transform>();
        foreach (Vector3 spawnPoint in sheepSpawnPoints)
        {
            GameObject initSpawn = Instantiate(point, Vector3.zero, Quaternion.identity, transform);
            initSpawn.transform.localPosition = spawnPoint;
            spawners.Add(initSpawn.transform);
        }
        points = spawners.ToArray();
    }

    private void DestroyMatrix()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Destroy(points[i].gameObject);
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

        // SpawnSheepHerd();

    }
    public void SpawnSheepHerd()
    {

        spawnedSheep = new bool[sheepSpawnPoints.Count];

        foreach (GameObject sheep in activeSheep)
        {
            FindPos(sheep);
        }

    }
    private bool FindPos(GameObject sheep)
    {
        int x = Random.Range(0, points.Length -1);
        if (!spawnedSheep[x])
        {
            sheep.transform.position = points[x].position;
            spawnedSheep[x] = true;
            return true;
        }
        else
        {
            return FindPos(sheep);
        }
    }

    #endregion
}
