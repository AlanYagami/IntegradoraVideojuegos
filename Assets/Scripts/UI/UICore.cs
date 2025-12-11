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
        if (GameManager.Instance != null) GameManager.Instance.ReturnToMenu();
        else SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        if (GameManager.Instance != null) GameManager.Instance.RestartLevel();
    }

    public void NextLevel()
    {
        if (GameManager.Instance != null) GameManager.Instance.LoadNextLevel();
    }

    public void ResumeGame()
    {
        if (GameManager.Instance != null) GameManager.Instance.ResumeGame();
        HideAllPanels(); // Ensure panels close visually immediately
    }
}
