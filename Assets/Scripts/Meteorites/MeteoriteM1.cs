using UnityEngine;

public class MeteoriteM1 : MonoBehaviour
{
    public float speed = 1.5f;
    public Vector3 direction = Vector3.back;
    public float rotationSpeed = 15f;
    public int damage = 10;

    private Rigidbody rb;
    private PlayerStateMachine player;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
            Debug.Log("[M1] Rigidbody inicializado correctamente. Movimiento kinemático activado.");
        }
        else
        {
            Debug.LogWarning("[M1] No se encontró un Rigidbody.");
        }
    }

    void Start()
    {
        Debug.Log($"[M1] Meteorito creado. Velocidad: {speed}, Rotación: {rotationSpeed}, Dirección: {direction}");

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerStateMachine>();
            Debug.Log("[M1] Jugador detectado correctamente.");
        }
        else
        {
            Debug.LogWarning("[M1] No se encontró un objeto con tag 'Player'.");
        }
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[M1] Colisión con el jugador");

            // Futuro sistema de daño:
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"[M1] Colisión con otro objeto: {other.gameObject.name}");
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("[M1] El meteorito salió de la cámara y será destruido.");
        Destroy(gameObject);
    }
}
