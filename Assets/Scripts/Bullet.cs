/*
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
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
    public Vector3 direction;
    public float speed = 20f;

    private float timer;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
        timer -= Time.deltaTime;
        if (timer >= 5f)
        {
            gameObject.SetActive(false);
            timer = 0f;
        }
    }
}
*/