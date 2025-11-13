using UnityEngine;

public class MeteoriteM1 : MonoBehaviour
{
    public float speed = 1.5f;

    public Vector3 direction = Vector3.back;

    public float rotationSpeed = 15f;

    public int damage = 10;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true; // fuerzas físicas
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
            Debug.Log($"[MeteoriteM1] Colisión con el jugador. Daño simulado: {damage}");
            // En el futuro:
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
