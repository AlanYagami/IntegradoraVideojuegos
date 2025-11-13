using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[EnemyProjectile] Impactó al jugador. Daño: {damage}");
            // En el futuro: other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
