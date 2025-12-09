using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Canvas uiCanvas;
    public TextMeshProUGUI gameOverText;
    public Button menuButton;
    public TextMeshProUGUI gamePausaText;

    private bool gameOverActivo = false;
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
        if (uiCanvas != null)
            uiCanvas.gameObject.SetActive(false);

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

        if (uiCanvas != null)
            uiCanvas.gameObject.SetActive(true);

        if (gameOverText != null)
        {
            gameOverText.text = "GAME OVER\n(ESC o M para menú)";
        }

        Time.timeScale = 0f;
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

        if (uiCanvas != null)
            uiCanvas.gameObject.SetActive(true);

        if (gamePausaText != null)
        {
            gamePausaText.text = "PAUSA";
        }
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f;

        if (uiCanvas != null)
            uiCanvas.gameObject.SetActive(false);

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
