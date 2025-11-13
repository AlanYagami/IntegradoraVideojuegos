using UnityEngine;

public class EnemyShipN1 : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveAmplitude = 3f;
    public float moveFrequency = 1f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float projectileSpeed = 20f;
    public int damage = 10;

    private Transform player;
    private float fireTimer;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        MovePattern();
        HandleShooting();
    }

    void MovePattern()
    {
        // Movimiento zigzag - seno
        float offset = Mathf.Sin(Time.time * moveFrequency) * moveAmplitude;
        Vector3 newPos = startPosition + transform.right * offset;
        transform.position = Vector3.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);
    }

    void HandleShooting()
    {
        if (player == null || projectilePrefab == null || firePoint == null)
            return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
    if (player == null || projectilePrefab == null || firePoint == null)
        return;

    Vector3 direction = (player.position - firePoint.position).normalized;

    if (direction == Vector3.zero)
    {
        direction = transform.forward;
    }

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[EnemyShipN1] Colisión con el jugador. Daño simulado: {damage}");
            // En el futuro: other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
