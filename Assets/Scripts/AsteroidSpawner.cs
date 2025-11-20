using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab; 
    public Transform player;

    [Header("Spawn Settings")]
    public float spawnRate = 1.2f;
    public float minDistance = 20f;
    public float maxDistance = 30f;

    [Header("Random Stats")]
    public float minSpeed = 1f;
    public float maxSpeed = 8f;

    public float minScale = 20f;
    public float maxScale = 30f;


    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnAsteroid();
            yield return new WaitForSeconds(spawnRate);
        }
    }

    void SpawnAsteroid()
    {
        // Posición aleatoria alrededor del jugador (solo en X/Z)
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minDistance, maxDistance);

        Vector3 spawnPos = player.position +
                           new Vector3(randomDir.x, 0, randomDir.y) * distance;

        // Crear asteroide
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);

        // Obtener script
        AsteroidMovement mov = asteroid.GetComponent<AsteroidMovement>();

        // Asignar objetivos y valores aleatorios
        mov.target = player;
        mov.speed = Random.Range(minSpeed, maxSpeed);
        mov.rotationSpeed = new Vector3(
            Random.Range(-90f, 90f),
            Random.Range(-90f, 90f),
            Random.Range(-90f, 90f)
        );

        float randomScale = Random.Range(minScale, maxScale);
        asteroid.transform.localScale = Vector3.one * randomScale;
    }
}