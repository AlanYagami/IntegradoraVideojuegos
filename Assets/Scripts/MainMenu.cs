using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject instructionsMenu;

    public void OpenOptionsPanel()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        instructionsMenu.SetActive(false);
    }
    public void OpenMainMenuPanel()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        instructionsMenu.SetActive(false);
    }

    public void OpenInstructionsPanel()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }

    public void SelectShip()
    {
        SceneManager.LoadScene("SelectCharacter");
    }
}
