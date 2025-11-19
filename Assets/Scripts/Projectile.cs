using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 3f; // segundos antes de destruirse

    void Start()
    {
        // Destruye la bala automáticamente después de X segundos
        Destroy(gameObject, lifeTime);
    }
}