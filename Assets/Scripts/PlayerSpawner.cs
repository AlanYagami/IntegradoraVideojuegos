using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] ships;
    public Transform spawnPoint;

    void Start()
    {
        int naveID = PlayerPrefs.GetInt("NaveID", 0);

        GameObject newShip = Instantiate(
        ships[naveID],
        spawnPoint.position,
        spawnPoint.rotation,
        transform
        );
    }
}