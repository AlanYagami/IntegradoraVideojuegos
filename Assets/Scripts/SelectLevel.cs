using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public void SelectLevel1()
    {
        // IMPORTANTE: Esto cargará el nivel Y la UI de forma aditiva
        GameManager.Instance.LoadLevel("Space_One");
    }

    public void SelectLevel2()
    {
        GameManager.Instance.LoadLevel("Space_Two");
    }

    public void SelectLevel3()
    {
        GameManager.Instance.LoadLevel("Space_Three");
    }

    public void BackToCharacterSelection()
    {
        GameManager.Instance.LoadScene("SelectCharacter");
    }
}
