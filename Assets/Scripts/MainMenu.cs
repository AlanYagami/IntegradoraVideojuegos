using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Panels Optional")]
    public GameObject mainPanel;

    private void Start()
    {
        // Asegurar estado inicial limpio
        if (mainPanel) mainPanel.SetActive(true);
    }

    // Botón JUGAR
    public void PlayGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene("SelectCharacter");
        }
        else
        {
            Debug.LogError("FATAL: No existe GameManager en la escena. Asegúrate de iniciar desde una escena con GameManager o que tenga el prefab.");
        }
    }

    // Botón Instrucciones
    public void Instructions()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadScene("InstruccionScene");
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

    

    public void ShowMain()
    {
        if (mainPanel) mainPanel.SetActive(true);
    }
}
