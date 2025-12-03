using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerSoundController soundController;
    public float speed = 5f;
    public GameObject bulletPrefab; 
    public Transform firePoint;
    public Transform firePointLeft;   // Punto de disparo izquierdo para disparo doble
    public Transform firePointRight;  // Punto de disparo derecho para disparo doble
    public Animator animator;         // Referencia al Animator para la animación de carga

    private float chargeTime = 0f;
    private bool isCharging = false;
    public float maxChargeTime = 2f;  // Tiempo necesario para cargar el disparo

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

        // Fix: Mover en el plano XZ (Horizontal, 0, Vertical) en lugar de XY
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        transform.Translate(movement * (speed * Time.deltaTime));

        // Lógica de disparo
        HandleShooting();
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
            ResetCharge();
            fireModeManager.SetFireMode(mode, duration);
        }
    }
    void HandleShooting()
    {
        // Si el modo es disparo cargado, usamos lógica de mantener presionado
        if (fireModeManager.GetCurrentFireMode() == FireModeManager.FireMode.ChargedShot)
        {
            if (Input.GetButton("Fire1") || Input.GetKey(KeyCode.Space))
            {
                chargeTime += Time.deltaTime;
                if (!isCharging)
                {
                    isCharging = true;
                    if (animator != null) animator.SetBool("IsCharging", true);
                    Debug.Log("Cargando disparo...");
                }
            }

            if (Input.GetButtonUp("Fire1") || Input.GetKeyUp(KeyCode.Space))
            {
                if (chargeTime >= maxChargeTime)
                {
                    ShootCharged();
                }
                else
                {
                    // Si suelta antes de cargar completo, dispara normal
                    ShootNormal();
                }
                ResetCharge();
            }
        }
        else
        {
            // Comportamiento normal para otros modos
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
            {
                soundController.playDisparo();
                Shoot();
            }
        }
    }

    void ResetCharge()
    {
        chargeTime = 0f;
        isCharging = false;
        if (animator != null) animator.SetBool("IsCharging", false);
    }
}

