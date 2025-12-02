using UnityEngine;

public class PowerUpSystemTest : MonoBehaviour
{
    // Este script es solo para verificar la integración
    // Puedes ejecutarlo en la escena o eliminarlo después
    
    void Start()
    {
        Debug.Log("=== POWER UP SYSTEM TEST ===");
        
        // Test 1: PlayerHealth
        var playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("✓ PlayerHealth encontrado");
            Debug.Log($"  - maxHits: {playerHealth.MaxHits}");
            Debug.Log($"  - hitsTaken: {playerHealth.HitsTaken}");
        }
        else
        {
            Debug.LogWarning("✗ PlayerHealth NO encontrado");
        }

        // Test 2: PlayerController
        var playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            Debug.Log("✓ PlayerController encontrado");
            Debug.Log($"  - bulletPrefab: {(playerController.bulletPrefab != null ? "Asignado" : "NO ASIGNADO")}");
            Debug.Log($"  - firePoint: {(playerController.firePoint != null ? "Asignado" : "NO ASIGNADO")}");
        }
        else
        {
            Debug.LogWarning("✗ PlayerController NO encontrado");
        }

        // Test 3: FireModeManager
        var fireMode = FindObjectOfType<FireModeManager>();
        if (fireMode != null)
        {
            Debug.Log("✓ FireModeManager encontrado");
            Debug.Log($"  - Modo actual: {fireMode.GetCurrentFireMode()}");
        }
        else
        {
            Debug.LogWarning("✗ FireModeManager NO encontrado (se creará en Start de PlayerController)");
        }

        // Test 4: CustomizablePowerUp
        var powerUps = FindObjectsOfType<CustomizablePowerUp>();
        Debug.Log($"✓ Power ups encontrados: {powerUps.Length}");
        foreach (var pu in powerUps)
        {
            Debug.Log($"  - {pu.powerUpName}: Curación={pu.isHealing}, Disparo={pu.isFireModePowerUp}");
        }

        Debug.Log("=== FIN TEST ===");
    }
}
