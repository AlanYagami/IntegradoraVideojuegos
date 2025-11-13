using System.Collections;
using UnityEngine;

public class EnemyShipN2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float zigzagAmplitude = 3f;
    public float zigzagFrequency = 3f;
    public float approachDistance = 15f;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 25f;
    public int burstCount = 3;
    public float burstDelay = 0.2f;
    public float fireCooldown = 3f;

    public int damage = 15;

    private Transform player;
    private float fireTimer = 0f;
    private Vector3 startPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        startPosition = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float zigzag = Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        Vector3 targetDir = (player.position - transform.position).normalized;

        Vector3 moveDir = new Vector3(zigzag, 0, targetDir.z).normalized;

        if (Vector3.Distance(transform.position, player.position) > approachDistance)
        {
            transform.position += targetDir * moveSpeed * Time.deltaTime;
        }

        transform.position += transform.right * zigzagAmplitude * Mathf.Sin(Time.time * zigzagFrequency) * Time.deltaTime;

        transform.LookAt(player);
    }

    void HandleShooting()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            StartCoroutine(BurstFire());
            fireTimer = fireCooldown;
        }
    }

    IEnumerator BurstFire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            ShootAtPlayer();
            yield return new WaitForSeconds(burstDelay);
        }
    }

    void ShootAtPlayer()
    {
        if (projectilePrefab == null || firePoint == null || player == null) return;

        Vector3 direction = (player.position - firePoint.position).normalized;
        if (direction == Vector3.zero) direction = transform.forward;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = false;
            rb.linearVelocity = direction * projectileSpeed;
        }

        Destroy(projectile, 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, approachDistance);
    }
}
