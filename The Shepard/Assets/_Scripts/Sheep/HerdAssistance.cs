using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdAssistance : MonoBehaviour
{
    private Dictionary<GameObject, Vector3> sheep = new Dictionary<GameObject, Vector3>();
    public bool EastWest;
    public float forceMultiplier;
    public Vector3 leftPoint;
    public Vector3 rightPoint;
    private Vector3 centerPoint;

    private void Start()
    {
        FindPoints();
    }

    private void Update()
    {
        FindPoints();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sheep" && !sheep.ContainsKey(other.gameObject))
        {
            sheep.Add(other.gameObject, SheepAreaPos(other.gameObject));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "sheep")
        {
            sheep[other.gameObject] = SheepAreaPos(other.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "sheep" && sheep.ContainsKey(other.gameObject))
        {
            sheep.Remove(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (sheep.Count > 0)
        {
            foreach (KeyValuePair<GameObject, Vector3> _sheep in sheep)
            {
                _sheep.Key.GetComponent<Rigidbody>().AddForce(_sheep.Value * forceMultiplier);
            }
        }
    }

    private void FindPoints()
    {
        Vector3 scale = transform.lossyScale;
        centerPoint = transform.position;
        if (EastWest)
        {
            leftPoint = centerPoint - new Vector3(scale.x / 2, 0, 0);
            rightPoint = centerPoint + new Vector3(scale.x / 2, 0, 0);
        }
        else
        {
            leftPoint = centerPoint - new Vector3(0, 0, scale.z / 2);
            rightPoint = centerPoint + new Vector3(0, 0, scale.z / 2);
        }
    }

    private Vector3 SheepAreaPos(GameObject sheep)
    {
        Rigidbody rb = sheep.GetComponent<Rigidbody>();
        if (EastWest)
        {
            if (sheep.transform.position.x < centerPoint.x)
            {
                return Vector3.right;
            }
            else
            {
                return Vector3.left;
            }
            //left right split (x)
        }
        else
        {
            if (sheep.transform.position.z < centerPoint.z)
            {
                return Vector3.forward;
            }
            else
            {
                return Vector3.back;
            }
            //top down split (z)
        }
    }

    private Vector3 ApplyForce(Vector3 referencePoint)
    {
        Vector3 force = centerPoint - referencePoint;
        return Vector3.ClampMagnitude(force, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(leftPoint, 0.5f);
        Gizmos.DrawWireSphere(rightPoint, 0.5f);
    }
}
