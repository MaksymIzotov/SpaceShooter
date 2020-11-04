using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    int health = 3;

    private void Start() => HealthTextUpdate.Instance.HealthUpdate(health);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
            EnemyHit(collision.gameObject);
    }

    void EnemyHit(GameObject enemy)
    {
        Destroy(enemy);
        health--;
        HealthTextUpdate.Instance.HealthUpdate(health);

        if (health == 0)
        {
            ButtonsManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }
}
