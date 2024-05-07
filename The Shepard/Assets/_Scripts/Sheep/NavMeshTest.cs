using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    public float forceMultiplier = 1;
    public Vector3 destination;
    private Vector3 destinationNorm;
    public bool moveToDest = false;
    public Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                destination = hit.point;
                Vector3 poinrTowards = destination - transform.position;
                destinationNorm = poinrTowards.normalized;
                moveToDest = true;
            }
        }

    }

    private void FixedUpdate()
    {
        if (moveToDest)
        {
            rb.AddForce(destinationNorm * forceMultiplier, ForceMode.Force);
        }
    }
}
