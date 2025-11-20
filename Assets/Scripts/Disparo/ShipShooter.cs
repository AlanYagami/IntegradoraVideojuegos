using UnityEngine;

public class ShipShooter : MonoBehaviour
{
    [Header("Asignar en Inspector")]
    public GameObject projectilePrefab;   // prefab del proyectil
    public Transform spawnPoint;          // spawn point (hijo del ship)

    [Header("Ajustes")]
    public float projectileSpeed = 20f;   // velocidad inicial
    public float fireRate = 0f;           // 0 = un disparo por pulsación; >0 = disparos por segundo si mantienes espacio

    private float lastFireTime = 0f;

    void Update()
    {
        // Si fireRate > 0 permitimos mantener presionado espacio para disparar en ráfaga
        if (fireRate > 0f)
        {
            if (Input.GetKey(KeyCode.Space) && Time.time - lastFireTime >= 1f / fireRate)
            {
                Fire();
            }
        }
        else
        {
            // Un proyectil por pulsación (GetKeyDown) — exactamente lo que pediste
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        lastFireTime = Time.time;

        if (projectilePrefab == null || spawnPoint == null)
        {
            Debug.LogWarning("ShipShooter: Projectile prefab o spawnPoint no asignados.");
            return;
        }

        // Instantiate con la rotación del spawnPoint (orientación)
        GameObject proj = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

        // Empujar con Rigidbody (si tiene)
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = spawnPoint.forward * projectileSpeed;
        }
        else
        {
            // Si no tiene Rigidbody, mover manualmente (menos recomendado)
            proj.transform.position += spawnPoint.forward * 0.01f;
        }
    }
}