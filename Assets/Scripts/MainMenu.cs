using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Panels Optional")]
    public GameObject optionsPanel; // Si tienes un panel de opciones en el menú principal
    public GameObject mainPanel;

    private void Start()
    {
        // Asegurar estado inicial limpio
        if (mainPanel) mainPanel.SetActive(true);
        if (optionsPanel) optionsPanel.SetActive(false);
    }

    // Botón JUGAR
    public void PlayGame()
    {
        // El flujo pide ir a SelectCharacter primero
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene("SelectCharacter");
        }
        else
        {
            Debug.LogError("FATAL: No existe GameManager en la escena. Asegúrate de iniciar desde una escena con GameManager o que tenga el prefab.");
        }
    }

    // Botón SALIR
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    // Métodos para paneles locales del menú (Credits, Options del menú principal)
    public void ShowOptions()
    {
        if (mainPanel) mainPanel.SetActive(false);
        if (optionsPanel) optionsPanel.SetActive(true);
    }

    public void ShowMain()
    {
        if (mainPanel) mainPanel.SetActive(true);
        if (optionsPanel) optionsPanel.SetActive(false);
    }
}
