using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;

    public void SelectLevel1()
    {
        SceneManager.LoadScene("Space_One");
    }
    public void SelectLevel2()
    {
        SceneManager.LoadScene("Space_Two");
    }
    public void SelectLevel3()
    {
        SceneManager.LoadScene("Space_Three");
    }
}
