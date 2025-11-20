using Unity.VisualScripting;
using UnityEngine;

public class ShipSelector : MonoBehaviour
{
    public GameObject[] ships; // Array to hold different ship prefabs
    private int index = 0; // Current index of the selected ship


    void Start()
    {
        UpdateShip();
    }

    public void NextShip()
    {
        index ++;
        if (index >= ships.Length)
        {
            index = 0;  
        }

        UpdateShip();
    }

    public void PreviousShip()
    {
        index --;
        if (index < 0)
        {
            index = ships.Length - 1;  
        }
            
        UpdateShip();
    }

    void UpdateShip()
    {
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].SetActive(i == index);
        }
    }
}
