using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;

    [Header("Rotation")]
    public Vector3 rotationSpeed;

    [Header("Lifetime")]
    public float lifeTime = 10f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        if (target == null) return;

        // Dirección hacia la nave
        Vector3 dir = (target.position - transform.position).normalized;

        // Movimiento
        transform.position += dir * speed * Time.deltaTime;

        // Rotación
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }

    // Método para recibir daño (llamado por ProjectileBullet)
    public void TakeDamage(int amount)
    {
        // Los asteroides de este tipo suelen destruirse de un golpe, 
        // pero usamos TakeDamage para estandarizar.
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(5); // 5 puntos por asteroide
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colision con: " + other.name);
        // Si toca al jugador
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);  // se destruye el asteroide (chocó, no cuenta puntos o sí? Asumimos que choque no da puntos)
            // aquí después podrás restar vida o explotar, pero por ahora se elimina
        }
    }
}