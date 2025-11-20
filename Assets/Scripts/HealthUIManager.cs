using UnityEngine;
using MagicPigGames;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private ProgressBar healthBar;
    private PlayerHealth playerHealth;

    private void Start()
    {

        // Buscar el PlayerHealth en la escena
        playerHealth = FindObjectOfType<PlayerHealth>();
        
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth no encontrado en la escena");
            return;
        }

        if (healthBar == null)
        {
            Debug.LogError("ProgressBar no asignada en el Inspector");
            return;
        }

        // Suscribirse al evento de cambio de salud
        playerHealth.OnHealthChanged += UpdateHealthBar;

        // Inicializar la barra con la salud actual
        // UpdateHealthBar(playerHealth.GetComponent<PlayerHealth>().maxHits, playerHealth.GetComponent<PlayerHealth>().maxHits);
    }

    private void UpdateHealthBar(int hitsTaken, int maxHits)
    {
        // Convertir la vida restante a un valor de progreso entre 0 y 1
        // Si no ha recibido daño (hitsTaken = 0), el progreso es 1 (barra llena)
        // Si ha recibido daño máximo (hitsTaken = maxHits), el progreso es 0 (barra vacía)
        float hitsRemaining = maxHits - hitsTaken;
        float progress = hitsRemaining / maxHits;
        
        // Si invertProgress está activo en la barra, invertir el valor aquí
        // porque SetProgress() invertirá de nuevo
        // if (healthBar.invertProgress)
        // {
        //     progress = 1f - progress;
        // }
        
        Debug.Log($"Health Update - Hits: {hitsTaken}/{maxHits}, RawProgress: {hitsRemaining}/{maxHits}, FinalProgress: {progress:F2}, InvertProgress: {healthBar.invertProgress}");
        healthBar.SetProgress(progress);
    }

    private void OnDestroy()
    {
        // Desuscribirse del evento para evitar memory leaks
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthBar;
        }
    }
}
