using UnityEngine;

public class EnemyShipN1 : MonoBehaviour
{
    public EnemiesSoundController soundController;

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
        {
            player = playerObj.transform;
            Debug.Log("[N1] Jugador detectado correctamente");
        }
        else
        {
            Debug.LogWarning("[N1] No se encontró un objeto con tag 'Player'");
        }

        if (soundController == null)
            Debug.LogWarning("[N1] No hay SoundController asignado.");

        Debug.Log("[N1] Nave enemiga inicializada");
    }

    void Update()
    {
        MovePattern();
        RotateTowardsPlayer();
        HandleShooting();
    }

    void MovePattern()
    {
        float offset = Mathf.Sin(Time.time * moveFrequency) * moveAmplitude;
        Vector3 newPos = startPosition + transform.right * offset;

        transform.position = Vector3.MoveTowards(transform.position, newPos, moveSpeed * Time.deltaTime);
    }

    void RotateTowardsPlayer()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                180f * Time.deltaTime
            );
        }
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
        if (player == null)
            return;

        Vector3 direction = (player.position - firePoint.position).normalized;
        if (direction == Vector3.zero)
            direction = transform.forward;

        Debug.Log("[N1] Disparando hacia el jugador");

        if (soundController != null)
        {
            soundController.playDisparoEnemigo();
        }
        else
        {
            Debug.LogWarning("[N1] No hay SoundController para reproducir sonido de disparo.");
        }

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.LookRotation(direction)
        );

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
            Debug.Log($"[EnemyShipN1] Colisión con el jugador. Daño: {damage}");

            if (soundController != null)
            {
                soundController.playGolpeEnemigo();
            }
            else
            {
                Debug.LogWarning("[N1] No hay SoundController para reproducir sonido de golpe.");
            }

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeHit();
            }
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("[N1] Nave salió de la cámara");
        Destroy(gameObject);
    }
}