using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerSoundController soundController;
    public float speed = 5f;
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public Transform firePointLeft;   // Punto de disparo izquierdo para disparo doble
    public Transform firePointRight;  // Punto de disparo derecho para disparo doble

    private float horizontalInput;
    private float verticalInput;
    private FireModeManager fireModeManager;

    void Start()
    {
        // Obtener o crear FireModeManager
        fireModeManager = GetComponent<FireModeManager>();
        if (fireModeManager == null)
        {
            fireModeManager = gameObject.AddComponent<FireModeManager>();
        }
    }

    void Update()
    {
        // Movimiento original simple (ejes Horizontal / Vertical)
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(movement * (speed * Time.deltaTime));

        // Disparo (sin modificar)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            soundController.playDisparo();
            Shoot();
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Shoot()
    {
        if (bulletPrefab && firePoint)
        {
            FireModeManager.FireMode currentMode = fireModeManager.GetCurrentFireMode();
            Debug.Log($"PlayerController: Shoot() modo actual = {currentMode}");

            switch (currentMode)
            {
                case FireModeManager.FireMode.Normal:
                    ShootNormal();
                    break;
                case FireModeManager.FireMode.DoubleShot:
                    ShootDouble();
                    break;
                case FireModeManager.FireMode.ChargedShot:
                    ShootCharged();
                    break;
                default:
                    ShootNormal();
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Falta asignar el BulletPrefab o el FirePoint en el inspector!");
        }
    }

    void ShootNormal()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("PlayerController: Disparo normal instanciado");
        SetBulletDamage(bullet, 1);
    }

    void ShootDouble()
    {
        // Disparo izquierdo
        if (firePointLeft != null)
        {
            GameObject bulletLeft = Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation);
            Debug.Log("PlayerController: Disparo doble - izquierda instanciado");
            SetBulletDamage(bulletLeft, 1);
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            SetBulletDamage(bullet, 1);
        }

        // Disparo derecho
        if (firePointRight != null)
        {
            GameObject bulletRight = Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation);
            Debug.Log("PlayerController: Disparo doble - derecha instanciado");
            SetBulletDamage(bulletRight, 1);
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            SetBulletDamage(bullet, 1);
        }
    }

    void ShootCharged()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("PlayerController: Disparo cargado instanciado");
        SetBulletDamage(bullet, 2);
    }

    void SetBulletDamage(GameObject bullet, int damage)
    {
        // Buscar si la bala tiene un script que se pueda configurar
        ProjectileBullet projectile = bullet.GetComponent<ProjectileBullet>();
        if (projectile != null)
        {
            projectile.SetDamage(damage);
        }
    }

    // Acceso público para que PowerUps cambien el modo de fuego
    public void SetFireMode(FireModeManager.FireMode mode, float duration)
    {
        if (fireModeManager != null)
        {
            Debug.Log($"PlayerController: SetFireMode invoked -> {mode} for {duration}s");
            fireModeManager.SetFireMode(mode, duration);
        }
    }
}

