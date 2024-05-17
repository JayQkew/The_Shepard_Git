using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    public static BoidsManager Instance { get; private set; }

    public List<GameObject> boids = new List<GameObject>();
    public float coherence_f;
    public float separation_f;
    public float minDistance;   //radius of a boids personal bubble
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        boids = GameObject.FindGameObjectsWithTag("sheep").ToList();
    }

    private void FixedUpdate()
    {
        BoidsLogic();
    }

    private void BoidsLogic()
    {
        foreach (GameObject boid in boids)
        {
            Coherence(boid);
            Separation(boid);
        }
    }

    #region COHERENCE
    private void Coherence(GameObject boid)
    {
        Vector3 force = CenterOfCrowd(boid) * coherence_f;

        if (!float.IsNaN(force.x) && !float.IsNaN(force.y) && !float.IsNaN(force.z))
        {
            boid.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        }
        else return;
    }

    private Vector3 CenterOfCrowd(GameObject boid)
    {
        Vector3 total = Vector3.zero;
        BoidLogic sheepLogic = boid.GetComponent<BoidLogic>();

        if (sheepLogic.surroundingBoids.Length > 0)
        {
            foreach (GameObject b in sheepLogic.surroundingBoids)
            {
                total += b.transform.position;
            }
        }
        else return total;

        Vector3 center = total / sheepLogic.surroundingBoids.Length;
        Vector3 center_f = center - boid.transform.position;
        Vector3 center_norm = Vector3.ClampMagnitude(center_f, 1);

        return center_norm;

    }
    #endregion

    #region SEPARATION
    private void Separation(GameObject boid)
    {
        Vector3 force = MinDistance(boid) * separation_f;

        if (!float.IsNaN(force.x) && !float.IsNaN(force.y) && !float.IsNaN(force.z))
        {
            boid.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        }
        else return;
    }

    private Vector3 MinDistance(GameObject boid)    //personal bubble around boid
    {
        Vector3 distance = Vector3.zero;
        BoidLogic sheepLogic = boid.GetComponent<BoidLogic>();

        if (sheepLogic.surroundingBoids.Length > 0)
        {
            foreach (GameObject b in sheepLogic.surroundingBoids)
            {
                Vector3 dif = b.transform.position - boid.transform.position;
                if (Vector3.Magnitude(dif) < minDistance) distance -= dif;
            }
        }
        else return distance;

        Vector3 distance_norm = Vector3.ClampMagnitude(distance, 1);
        return distance_norm;
    }

    #endregion

    public void AddToBoids()
    {
        boids = GameObject.FindGameObjectsWithTag("sheep").ToList();
    }

}
