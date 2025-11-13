using System.Collections;
using UnityEngine;

public class EnemyShipN3 : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float approachDistance = 20f;
    public float retreatDistance = 10f;

    public GameObject homingMissilePrefab;
    public Transform firePoint;
    public float fireCooldown = 5f;
    public bool useBurst = false;   // si dispara en rafaga
    public int burstCount = 3;
    public float burstDelay = 0.3f;

    public GameObject shieldVisual;
    public int shieldHits = 2;
    public float shieldRechargeTime = 8f;
    private int currentShieldHits;
    private bool shieldActive = true;

    public int health = 150;
    public int damage = 20;

    private Transform player;
    private float fireTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentShieldHits = shieldHits;
        if (shieldVisual != null)
            shieldVisual.SetActive(true);
    }

    void Update()
    {
        if (player == null) return;

        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 direction = (player.position - transform.position).normalized;

        if (distance > approachDistance)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else if (distance < retreatDistance)
        {
            transform.position -= direction * (moveSpeed * 0.5f) * Time.deltaTime;
        }

        transform.LookAt(player);
    }

    void HandleShooting()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            if (useBurst)
                StartCoroutine(BurstFire());
            else
                ShootHomingMissile();

            fireTimer = fireCooldown;
        }
    }

    IEnumerator BurstFire()
    {
        for (int i = 0; i < burstCount; i++)
        {
            ShootHomingMissile();
            yield return new WaitForSeconds(burstDelay);
        }
    }

    void ShootHomingMissile()
    {
        if (homingMissilePrefab == null || firePoint == null || player == null) return;

        GameObject missile = Instantiate(homingMissilePrefab, firePoint.position, firePoint.rotation);
        HomingMissileAdvanced hm = missile.GetComponent<HomingMissileAdvanced>();
        if (hm != null)
        {
            hm.SetTarget(player);
        }
    }

    public void TakeDamage(int dmg)
    {
        if (shieldActive)
        {
            currentShieldHits--;
            Debug.Log($"[N3] Escudo impactado. Quedan {currentShieldHits} golpes.");

            if (currentShieldHits <= 0)
            {
                shieldActive = false;
                if (shieldVisual != null)
                    shieldVisual.SetActive(false);
                StartCoroutine(RechargeShield());
            }
            return;
        }

        health -= dmg;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator RechargeShield()
    {
        yield return new WaitForSeconds(shieldRechargeTime);
        shieldActive = true;
        currentShieldHits = shieldHits;
        if (shieldVisual != null)
            shieldVisual.SetActive(true);
        Debug.Log("[N3] Escudo recargado.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, approachDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, retreatDistance);
    }
}
