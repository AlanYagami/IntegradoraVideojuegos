using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
<<<<<<< HEAD
            Debug.Log($"[EnemyProjectile] Impactó al jugador");
            // En el futuro: other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
=======
            Debug.Log($"[EnemyProjectile] Impactó al jugador. Daño: {damage}");
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeHit();
            }

>>>>>>> 8e8aebc989d22cb83b3a17a04186725cef223389
            Destroy(gameObject);
        }
    }
}
