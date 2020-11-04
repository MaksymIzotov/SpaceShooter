using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab, bulletSpawnPoint;
    [SerializeField] float shootingRate;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine("Shooting");

    }

    public IEnumerator Shooting()
    {
        while (Input.GetKey(KeyCode.Space))
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(shootingRate);
        }
    }
}
