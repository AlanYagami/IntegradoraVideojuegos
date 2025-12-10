using UnityEngine;
using UnityEngine.SceneManagement;

public class UICore : MonoBehaviour
{
    public static UICore Instance;

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;

    private void Awake()
    {
        // Singleton Implementation
        if (Instance == null)
        {
            Instance = this;
            // We do NOT want DontDestroyOnLoad if we are loading this additively strictly for the level.
            // HOWEVER, the user asked for "Exista únicamente en escenas de nivel" and "Se destruya automáticamente si aparece por error...".
            // Typically, if loaded additively, it lives as long as the scene. 
            // If the user wants it to persist across levels, we'd use DontDestroyOnLoad.
            // But if it is inside "OptionsScene" which is loaded additively, it will be destroyed when OptionsScene is unloaded or when the main scene changes single-mode.
            // Let's assume OptionsScene is loaded additively and we want it to handle itself.
            // If the user said "UI se queda persistente", we want to avoid DontDestroyOnLoad unless carefully managed.
            // Given the requirement "NO debe existir NINGÚN addititve load fuera de los niveles", 
            // let's play it safe: DO NOT use DontDestroyOnLoad if it belongs to the additive scene.
            // The additive scene will be unloaded when the main scene changes.
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Safety Check: If this script somehow ends up in MainMenu, kill it.
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "MainMenu" || currentScene == "SelectCharacter" || currentScene == "SelectLevels")
        {
            Destroy(gameObject);
            return;
        }

        // Auto-hide all panels on start
        HideAllPanels();
    }

    // ================= PUBLIC METHODS =================

    public void ShowPausePanel()
    {
        HideAllPanels();
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void ShowVictoryPanel()
    {
        HideAllPanels();
        if (victoryPanel != null) victoryPanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        HideAllPanels();
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void HideAllPanels()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }

    public void ReturnToMenu()
    {
        // Proxy to GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ReturnToMenu();
        }
        else
        {
            // Fallback
            SceneManager.LoadScene("MainMenu");
        }
    }
}
