using UnityEngine;

public class MeteoriteM3 : MonoBehaviour
{
    public float initialSpeed = 2f;
    public float maxSpeed = 15f;
    public float acceleration = 3f;
    public float rotationSpeed = 60f;
    public Transform player;
    public int damage = 20;

    private Rigidbody rb;
    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    void Start()
    {
        currentSpeed = initialSpeed;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (player == null)
        {
            Debug.LogWarning("[MeteoriteM3] No se encontró un objeto con tag 'Player'.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calcula dirección hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Aumenta la velocidad progresivamente
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);

        // Movimiento hacia el jugador
        transform.position += direction * currentSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[MeteoriteM3] Impactó al jugador. Daño simulado: {damage}");
            // En el futuro:
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
