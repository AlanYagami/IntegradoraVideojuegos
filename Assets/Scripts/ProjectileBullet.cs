using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 1;
    public float lifetime = 5f;
    private float timer = 0f;

    void Start()
    {
        timer = lifetime;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Fix: Usamos comparison directa (other.tag == "...") en lugar de CompareTag
        // porque CompareTag lanza erro si el Tag no está definido en los Project Settings.
        if (other.tag == "PlayerProjectile" || other.tag == "EnemyProjectile")
            return;

        // Intentar dañar Enemy
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Intentar dañar MeteoriteM1
        MeteoriteM1 meteor = other.GetComponent<MeteoriteM1>();
        if (meteor != null)
        {
            meteor.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Intentar dañar AsteroidMovement
        AsteroidMovement asteroid = other.GetComponent<AsteroidMovement>();
        if (asteroid != null)
        {
            asteroid.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Si es "Enemy" tag pero no tiene script, lo destruimos por seguridad (Legacy behavior)
        if (other.CompareTag("Enemy"))
        {
             Debug.LogWarning($"[PlayerBullet] Objeto {other.name} tiene tag Enemy pero no script conocido. Destruyendo igual.");
             Destroy(other.gameObject);
             Destroy(gameObject);
        }
    }


    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
