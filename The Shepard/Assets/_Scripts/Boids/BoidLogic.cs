using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidLogic : MonoBehaviour
{
    [Header("Boids")]
    public float influence_r;   //area to detect boids
    public LayerMask boids;     //boid layer
    public GameObject[] surroundingBoids;

    private void Update()
    {
        SurroundingBoids();
    }
    public void SurroundingBoids()
    {
        RaycastHit[] hit = Physics.SphereCastAll(transform.position, influence_r, Vector3.up, 0.1f, boids);
        List<GameObject> sur_boids = new List<GameObject>();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].transform.gameObject != gameObject) sur_boids.Add(hit[i].transform.gameObject);
        }
        surroundingBoids = sur_boids.ToArray();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, influence_r);
    }
}
