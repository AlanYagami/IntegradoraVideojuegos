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
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeHit();
            }
            Destroy(gameObject);
        }
    }
}
