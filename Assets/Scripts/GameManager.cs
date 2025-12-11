using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Configuration")]
    public string menuSceneName = "MainMenu";
    public string optionsSceneName = "OptionsScene";
    public List<string> levelNames = new List<string> { "Space_One", "Space_Two", "Space_Three" };

    [Header("Score System")]
    public int score = 0;
    public int targetScore = 100;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. Check if the loaded scene is a GAMEPLAY LEVEL
        if (levelNames.Contains(scene.name))
        {
            // 2. Load OptionsScene ADDITIVELY if not already present
            if (!IsSceneLoaded(optionsSceneName))
            {
                Debug.Log($"[GameManager] Loading UI ({optionsSceneName}) for level: {scene.name}");
                SceneManager.LoadSceneAsync(optionsSceneName, LoadSceneMode.Additive);
            }
            
            // Ensure time is running
            Time.timeScale = 1f;
        }
        else if (scene.name == optionsSceneName)
        {
            // OptionsScene just loaded. Do nothing special. 
            // UICore inside it will initialize itself.
        }
        else
        {
            // We are in a menu (MainMenu, SelectCharacter, etc).
            // Ensure NO OptionsScene is present (cleanup if necessary)
            // Note: Normal usage of LoadScene(Single) automatically unloads other scenes, 
            // so OptionsScene should disappear naturally.
            Time.timeScale = 1f;
        }
    }

    private bool IsSceneLoaded(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene s = SceneManager.GetSceneAt(i);
            if (s.name == sceneName) return true;
        }
        return false;
    }

    // ================= SCORE METHODS =================

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"[GameManager] Score: {score}/{targetScore}");

        if (score >= targetScore)
        {
            Victory();
        }
    }

    // ================= NAVIGATION METHODS =================

    public void LoadScene(string sceneName)
    {
        Debug.Log($"[GameManager] Loading Scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    public void LoadLevel(string levelName)
    {
        if (levelNames.Contains(levelName))
        {
            Debug.Log($"[GameManager] Starting Level: {levelName}");
            SceneManager.LoadScene(levelName); 
            // OnSceneLoaded will handle the UI
        }
        else
        {
            Debug.LogError($"[GameManager] Level {levelName} is not in the allowed level list!");
        }
    }

    public void RestartLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        LoadLevel(currentScene);
    }

    public void LoadNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int index = levelNames.IndexOf(currentScene);
        
        if (index != -1 && index < levelNames.Count - 1)
        {
            LoadLevel(levelNames[index + 1]);
        }
        else
        {
            Debug.Log("[GameManager] No more levels. Returning to menu.");
            ReturnToMenu();
        }
    }

    public void ReturnToMenu()
    {
        LoadScene(menuSceneName);
    }

    // ================= GAME LOGIC PROXIES =================
    // Called by PlayerHealth, etc.

    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;
        if (UICore.Instance != null) UICore.Instance.ShowGameOverPanel();
    }

    public void Victory()
    {
        Debug.Log("Victory!");
        Time.timeScale = 0f; // Pause game
        if (UICore.Instance != null) UICore.Instance.ShowVictoryPanel();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        if (UICore.Instance != null) UICore.Instance.ShowPausePanel();
    }

    // ================= INPUT HANDLING =================
    
    private void Update()
    {
        // Only process inputs if we are in a gameplay level
        string currentScene = SceneManager.GetActiveScene().name;
        if (!levelNames.Contains(currentScene)) return;

        // PAUSE: ESC or P
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale > 0)
                PauseGame();
            else
                ResumeGame();
        }

        // DEBUG: V for Victory
        if (Input.GetKeyDown(KeyCode.V))
        {
            Victory();
        }

        // DEBUG: G for Game Over
        if (Input.GetKeyDown(KeyCode.G))
        {
            GameOver();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        if (UICore.Instance != null) UICore.Instance.HideAllPanels();
    }
}
