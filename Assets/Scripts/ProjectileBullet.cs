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
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
