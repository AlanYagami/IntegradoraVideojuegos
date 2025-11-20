using UnityEngine;

public class MeteoriteM2 : MonoBehaviour
{
    public float speed = 15f;
    public Vector3 direction;
    public float randomAngleRange = 25f;
    public float rotationSpeed = 90f;
    public int damage = 15;
    private Rigidbody rb;
    private Camera mainCamera;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        mainCamera = Camera.main;
    }

    void Start()
    {
        if (direction == Vector3.zero)
        {
            direction = RandomDiagonalDirection();
        }
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private Vector3 RandomDiagonalDirection()
    {
        Vector3 baseDir = Vector3.back + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.3f, 0.3f), 0);
        baseDir.Normalize();

        Quaternion randomRot = Quaternion.Euler(Random.Range(-randomAngleRange, randomAngleRange),
                                                 Random.Range(-randomAngleRange, randomAngleRange),
                                                 0f);
        return randomRot * baseDir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[MeteoriteM2] Colisión con el jugador. Daño simulado: {damage}");
            // En el futuro: other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
