using UnityEngine;
using System.Collections;

public class SpaceBaseBoss : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Attack Settings")]
    public float attackRange = 100f;
    public float rotationSpeed = 5f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;   // Tiempo entre ráfagas
    public int bulletsPerBurst = 5;
    public float timeBetweenBullets = 0.1f; // Velocidad dentro de la ráfaga
    public float bulletForce = 80f; // Fuerza de las balas

    private float nextFireTime = 0f;
    private bool isShooting = false;

    void Update()
    {
        if (player == null) return;

        RotateTowardsPlayer();
        firePoint.LookAt(player);

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange && Time.time >= nextFireTime && !isShooting)
        {
            StartCoroutine(BurstFire());
            nextFireTime = Time.time + fireRate;
        }
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator BurstFire()
    {
        isShooting = true;

        for (int i = 0; i < bulletsPerBurst; i++)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenBullets);
        }

        isShooting = false;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = false;

        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
    }
}