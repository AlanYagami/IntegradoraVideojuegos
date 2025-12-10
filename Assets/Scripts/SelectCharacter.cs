using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    [Header("Model Selection")]
    public GameObject[] shipModels; 
    private int currentShipIndex = 0;

    private void Start()
    {
        currentShipIndex = PlayerPrefs.GetInt("NaveID", 0);
        UpdateShipVisuals();
    }


    public void NextShip()
    {
        currentShipIndex++;
        if (currentShipIndex >= shipModels.Length)
        {
            currentShipIndex = 0;
        }
        UpdateShipVisuals();
    }

    public void PreviousShip()
    {
        currentShipIndex--;
        if (currentShipIndex < 0)
        {
            currentShipIndex = shipModels.Length - 1;
        }
        UpdateShipVisuals();
    }

    private void UpdateShipVisuals()
    {
        for (int i = 0; i < shipModels.Length; i++)
        {
            if (shipModels[i] != null)
            {
                shipModels[i].SetActive(i == currentShipIndex);
            }
        }
        
        PlayerPrefs.SetInt("NaveID", currentShipIndex);
        PlayerPrefs.Save();
    }


    public void ConfirmSelection()
    {
        GameManager.Instance.LoadScene("SelectLevels");
    }

    public void SelectChar1()
    {
        currentShipIndex = 0;
        UpdateShipVisuals();
    }

    public void SelectChar2()
    {
        currentShipIndex = 1;
        UpdateShipVisuals();
    }

    public void SelectChar3()
    {
        currentShipIndex = 3;
        UpdateShipVisuals();
    }

    public void SelectChar4()
    {
        currentShipIndex = 4;
        UpdateShipVisuals();
    }

    public void SelectChar5()
    {
        currentShipIndex = 5;
        UpdateShipVisuals();
    }
    
    // public void BackToMenu()
    // {
    //     GameManager.Instance.ReturnToMenu();
    // }
}
