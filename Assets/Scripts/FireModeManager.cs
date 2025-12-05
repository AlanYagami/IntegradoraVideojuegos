using UnityEngine;

public class FireModeManager : MonoBehaviour
{
    public enum FireMode
    {
        Normal,      // Disparo simple
        DoubleShot,  // Disparo doble (dos balas simultáneamente)
        ChargedShot  // Disparo cargado (bala más fuerte)
    }

    private FireMode currentFireMode = FireMode.Normal;
    private float fireModeDuration = 0f;
    private float currentFireModeTime = 0f;

    public void SetFireMode(FireMode newMode, float duration)
    {
        currentFireMode = newMode;
        fireModeDuration = duration;
        currentFireModeTime = 0f;
        Debug.Log($"Fuego cambiado a: {newMode} por {duration} segundos");
    }

    public void ResetFireMode()
    {
        currentFireMode = FireMode.Normal;
        fireModeDuration = 0f;
        currentFireModeTime = 0f;
    }

    public FireMode GetCurrentFireMode()
    {
        return currentFireMode;
    }

    void Update()
    {
        if (fireModeDuration > 0)
        {
            currentFireModeTime += Time.deltaTime;
            if (currentFireModeTime >= fireModeDuration)
            {
                ResetFireMode();
            }
        }
    }

    // Retorna cuánto daño hace el disparo según el modo
    public int GetDamage()
    {
        return currentFireMode switch
        {
            FireMode.Normal => 1,
            FireMode.DoubleShot => 1,  // Cada bala hace 1 daño, pero son 2
            FireMode.ChargedShot => 2,  // Bala más fuerte
            _ => 1
        };
    }

    public bool IsActiveFireMode()
    {
        return currentFireMode != FireMode.Normal;
    }

    public float GetTimeRemaining()
    {
        return Mathf.Max(0, fireModeDuration - currentFireModeTime);
    }
}
