using System.Collections;
using UnityEngine;

public class EnemyOctopusP1 : MonoBehaviour
{
    [Header("Movimiento")]
    public float floatSpeed = 2f;
    public float waveAmplitude = 2f;
    public float waveFrequency = 2f;
    public float followDistance = 25f;

    [Header("Ataque")]
    public GameObject bubblePrefab;
    public Transform firePoint;
    public float bubbleSpeed = 10f;
    public float fireCooldown = 4f;

    private float fireTimer;
    private Transform player;
    private Vector3 startPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        startPos = transform.position;
    }

    void Update()
    {
        if (player == null) return;

        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float wave = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;
        Vector3 sideMovement = transform.right * wave * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 toPlayer = (player.position - transform.position).normalized;

        if (distance > followDistance)
        {
            transform.position += toPlayer * floatSpeed * Time.deltaTime;
        }

        transform.position += (transform.forward * floatSpeed * 0.5f * Time.deltaTime) + sideMovement;

        transform.LookAt(player);
    }

    void HandleShooting()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            ShootBubble();
            fireTimer = fireCooldown;
        }
    }

    void ShootBubble()
    {
        if (bubblePrefab == null || firePoint == null) return;

        GameObject bubble = Instantiate(bubblePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bubble.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = (player.position - firePoint.position).normalized;
            rb.useGravity = false;
            rb.linearVelocity = direction * bubbleSpeed;
        }

        Destroy(bubble, 6f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}
