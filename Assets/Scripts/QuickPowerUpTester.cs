using UnityEngine;

// Helper to manually trigger power-up effects for testing without collisions
public class QuickPowerUpTester : MonoBehaviour
{
    public PlayerController playerController;
    public PlayerHealth playerHealth;

    void Awake()
    {
        if (playerController == null) playerController = GetComponent<PlayerController>();
        if (playerHealth == null) playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Press K to trigger DoubleShot for 10s
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (playerController != null)
            {
                Debug.Log("QuickPowerUpTester: Simulando DoubleShot");
                playerController.SetFireMode(FireModeManager.FireMode.DoubleShot, 10f);
            }
        }

        // Press L to trigger ChargedShot for 8s
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (playerController != null)
            {
                Debug.Log("QuickPowerUpTester: Simulando ChargedShot");
                playerController.SetFireMode(FireModeManager.FireMode.ChargedShot, 8f);
            }
        }

        // Press H to heal 1
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (playerHealth != null)
            {
                Debug.Log("QuickPowerUpTester: Simulando Heal(1)");
                playerHealth.Heal(1);
            }
        }
    }
}
