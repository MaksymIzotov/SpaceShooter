using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rb;

    [SerializeField] int damage;

    void Start()
    {
        Destroy(gameObject, 2);

        rb = GetComponent<Rigidbody>();

        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<AsteroidHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
