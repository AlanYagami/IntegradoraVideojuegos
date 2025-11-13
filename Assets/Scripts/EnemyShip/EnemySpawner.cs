using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyShipPrefab;
    public int count = 5;
    public float spacing = 5f;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = transform.position + new Vector3(i * spacing - (count * spacing / 2), 0, 0);
            Instantiate(enemyShipPrefab, pos, Quaternion.identity);
        }
    }
}
