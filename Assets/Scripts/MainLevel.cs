using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLevel : MonoBehaviour
{
    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }
}
