using UnityEngine;
using System.Collections;

public class MeteoriteM3 : MonoBehaviour
{
    public float initialSpeed = 2f;
    public float maxSpeed = 15f;
    public float acceleration = 3f;
    public float rotationSpeed = 60f;
    public float attachDistance = 1.5f;

    private Transform playerTransform;
    private PlayerStateMachine player;
    private bool attached = false;
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

        currentSpeed = initialSpeed;
    }

    void Start()
    {
        Debug.Log($"[M3] Meteorito creado.");

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            player = playerObject.GetComponent<PlayerStateMachine>();
            Debug.Log("[M3] Jugador detectado correctamente.");
        }
        else
        {
            Debug.LogWarning("[M3] No se encontró un objeto con tag 'Player'.");
        }
    }

    void Update()
    {
        if (playerTransform == null || attached) return;

        // Aumenta la velocidad progresivamente
        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);

        // Movimiento hacia el jugador
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, currentSpeed * Time.deltaTime);
        transform.LookAt(playerTransform);

        // Detectar si está lo suficientemente cerca para "pegarse"
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= attachDistance)
        {
            AttachToPlayer();
        }

        // Rotación
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void AttachToPlayer()
    {
        if (attached) return;
        attached = true;
        StartCoroutine(AttachRoutine());
    }

    private IEnumerator AttachRoutine()
    {
        Debug.Log("Meteorito M3: Destrucción del meteorito");
        Destroy(gameObject);

        yield return new WaitForSeconds(0.1f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (attached) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log($"[M3] Impactó al jugador.");
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
