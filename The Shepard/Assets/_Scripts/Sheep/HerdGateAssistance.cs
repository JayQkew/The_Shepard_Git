using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdGateAssistance : MonoBehaviour
{
    public Direction direction;
    public float forceMultiplier;
    public List<GameObject> sheep;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sheep")
        {
            sheep.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "sheep")
        {
            sheep.Remove(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        foreach (GameObject _sheep in sheep)
        {
            switch (direction)
            {
                case Direction.Left:
                    _sheep.GetComponent<Rigidbody>().AddForce(Vector3.left * forceMultiplier, ForceMode.Force);
                    break;
                case Direction.Right:
                    _sheep.GetComponent<Rigidbody>().AddForce(Vector3.right * forceMultiplier, ForceMode.Force);
                    break;
                case Direction.Forward:
                    _sheep.GetComponent<Rigidbody>().AddForce(Vector3.forward * forceMultiplier, ForceMode.Force);
                    break;
                case Direction.Back:
                    _sheep.GetComponent<Rigidbody>().AddForce(Vector3.down * forceMultiplier, ForceMode.Force);
                    break;
            }
        }
    }
}

public enum Direction
{
    Left,
    Right,
    Forward,
    Back
}
