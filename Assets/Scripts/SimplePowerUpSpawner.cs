using UnityEngine;

public class SimplePowerUpSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] powerUpPrefabs; // Asigna tus 3 prefabs aquí

    [Header("Settings")]
    public float spawnIntervalMin = 15f;
    public float spawnIntervalMax = 20f;
    public Vector2 spawnAreaMin = new Vector2(-10, -10);
    public Vector2 spawnAreaMax = new Vector2(10, 10);
    public float spawnHeight = 0f;

    private float timer;
    private float currentInterval;
    private GameObject currentPowerUp;

    void Start()
    {
        SetNextInterval();
    }

    void Update()
    {
        // Si ya hay un power up en escena, no spawneamos otro
        if (currentPowerUp != null) return;

        timer += Time.deltaTime;
        if (timer >= currentInterval)
        {
            SpawnPowerUp();
            timer = 0f;
            SetNextInterval();
        }
    }

    void SetNextInterval()
    {
        currentInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    void SpawnPowerUp()
    {
        if (powerUpPrefabs == null || powerUpPrefabs.Length == 0) return;

        // Elegir prefab aleatorio
        int index = Random.Range(0, powerUpPrefabs.Length);
        GameObject prefab = powerUpPrefabs[index];

        // Elegir posición aleatoria
        Vector3 pos = new Vector3(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            spawnHeight,
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Instanciar
        if (prefab == null)
        {
            Debug.LogWarning($"SimplePowerUpSpawner: El prefab en el índice {index} es nulo o fue destruido. Asegúrate de asignar PREFABS desde la carpeta 'Project', no objetos de la 'Hierarchy'.");
            return;
        }

        currentPowerUp = Instantiate(prefab, pos, Quaternion.identity);
        Debug.Log($"SimplePowerUpSpawner: Spawneado {currentPowerUp.name} en {pos}");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Vector3 center = new Vector3(
            (spawnAreaMin.x + spawnAreaMax.x) / 2,
            spawnHeight,
            (spawnAreaMin.y + spawnAreaMax.y) / 2
        );
        Vector3 size = new Vector3(
            spawnAreaMax.x - spawnAreaMin.x,
            1f,
            spawnAreaMax.y - spawnAreaMin.y
        );
        Gizmos.DrawCube(center, size);
    }
}
