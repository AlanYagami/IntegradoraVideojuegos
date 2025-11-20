using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public float lifetime = 3f;

    public Vector3 direction = Vector3.up; // Dirección por defecto

    private float timer;

    void Start()
    {
        timer = lifetime;
    }

    void Update()
    {
        // Mover bala en la dirección indicada
        transform.Translate(direction * (speed * Time.deltaTime));

        // Contador de tiempo
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}