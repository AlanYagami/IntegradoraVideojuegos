using UnityEngine;
using UnityEngine.InputSystem;

public class ShipChargeShot : MonoBehaviour
{
    public GameObject chargedProjectilePrefab;
    public Transform spawnPoint;

    public float maxScale = 3f;        
    public float chargeSpeed = 1f;     
    public float projectileSpeed = 20f;

    private bool isCharging = false;
    private GameObject previewProjectile;
    private float currentScale = 1f;

    void Update()
    {
        // Inicia carga (cuando presionas espacio)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartCharging();
        }

        // Suelta carga (cuando sueltas espacio)
        if (Keyboard.current.spaceKey.wasReleasedThisFrame)
        {
            ReleaseCharge();
        }

        // Mientras carga, crecer
        if (isCharging && previewProjectile != null)
        {
            currentScale += chargeSpeed * Time.deltaTime;
            currentScale = Mathf.Clamp(currentScale, 1f, maxScale);

            previewProjectile.transform.position = spawnPoint.position;
            previewProjectile.transform.localScale = Vector3.one * currentScale;
        }
    }

    void StartCharging()
    {
        if (previewProjectile != null) return;

        isCharging = true;
        currentScale = 1f;

        previewProjectile = Instantiate(chargedProjectilePrefab, spawnPoint.position, spawnPoint.rotation);

        var rb = previewProjectile.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        previewProjectile.transform.localScale = Vector3.one;
    }

    void ReleaseCharge()
    {
        if (!isCharging || previewProjectile == null) return;

        isCharging = false;

        var rb = previewProjectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = spawnPoint.forward * projectileSpeed;
        }

        previewProjectile = null;
    }
}