using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public float speed, tumble;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = -transform.forward * speed;
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        Destroy(gameObject, 5);
    }
}
