using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionarNave : MonoBehaviour
{
    public GameObject iniciar;

    public void SelectLevels()
    {
        SceneManager.LoadScene("SelectLevels");
    }
}
