using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    // Método para el botón del personaje 1
    public void SelectChar1()
    {
        // Aquí podrías guardar la selección en GameManager si tienes esa lógica
        // Ejemplo: GameManager.Instance.SetCharacter(0);
        
        GoToLevelSelection();
    }

    // Método para el botón del personaje 2
    public void SelectChar2()
    {
        // GameManager.Instance.SetCharacter(1);
        GoToLevelSelection();
    }

    private void GoToLevelSelection()
    {
        // Usamos GameManager para cambiar de escena de forma segura
        GameManager.Instance.LoadScene("SelectLevels");
    }
    
    public void BackToMenu()
    {
        GameManager.Instance.ReturnToMenu();
    }
}
