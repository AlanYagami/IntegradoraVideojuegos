using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject backgroundPanel;

    // Game Over UI
    public TextMeshProUGUI gameOverText;
    public Button menuButton;
    private bool gameOverActivo = false;

    // Pausa UI
    public TextMeshProUGUI gamePausaText;
    private bool juegoPausado = false;

    public List<Personajes> personajes;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        if (backgroundPanel != null)
            backgroundPanel.SetActive(false);

        if (menuButton != null)
            menuButton.onClick.AddListener(IrAlMenu);
    }

    void Update()
    {
        // ----- PAUSA -----
        if (!gameOverActivo && Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausa();
        }

        // ----- GAME OVER -----
        if (gameOverActivo)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.M))
            {
                IrAlMenu();
            }
        }
    }

    // ================== GAME OVER ==================

    public void GameOver()
    {
        if (gameOverActivo) return;

        gameOverActivo = true;

        if (backgroundPanel != null)
            backgroundPanel.SetActive(true);

        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER\n(ESC o M para menú)";
        }

        //Time.timeScale = 0f;
    }

    // ================== PAUSA ==================

    public void TogglePausa()
    {
        if (juegoPausado)
            Reanudar();
        else
            Pausar();
    }

    public void Pausar()
    {
        juegoPausado = true;
        Time.timeScale = 0f;

        if (backgroundPanel != null)
            backgroundPanel.SetActive(true);
        
        if (gamePausaText != null)
        {
            gamePausaText.text = "PAUSA";
        }
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;

        if (backgroundPanel != null)
            backgroundPanel.SetActive(false);
        
        if (gamePausaText != null)
        {
            gamePausaText.text = "";
        }
    }

    // ================== ESCENAS ==================

    public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
