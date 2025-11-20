using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject bulletPrefab; 
    public Transform firePoint; 

    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(movement * (speed * Time.deltaTime));

        // Disparo con tecla Espacio o clic izquierdo
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Shoot()
    {
        if (bulletPrefab && firePoint)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("Falta asignar el BulletPrefab o el FirePoint en el inspector!");
        }
    }
}

