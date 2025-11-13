using UnityEngine;

public class BubbleProjectile : MonoBehaviour
{
    public float slowDuration = 3f;
    public float slowFactor = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[BubbleProjectile] El jugador ha sido alcanzado y ralentizado.");

            //revisar y cambiar pq ya me dio sueñito
            PlayerMovement pm = other.GetComponent<PlayerMovement>();
            if (pm != null)
            {
                pm.ApplySlow(slowFactor, slowDuration);
            }

            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
