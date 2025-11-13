using UnityEngine;

public class HomingMissileAdvanced : MonoBehaviour
{
    public float speed = 20f;
    public float rotateSpeed = 200f;
    public float trackingTime = 2.5f;
    public float lifeTime = 8f;
    public int damage = 25;

    private Transform target;
    private Rigidbody rb;
    private bool tracking = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeTime);
        Invoke(nameof(StopTracking), trackingTime);
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    void FixedUpdate()
    {
        if (tracking && target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);
            rb.angularVelocity = -rotateAmount * rotateSpeed * Mathf.Deg2Rad;
        }

        rb.linearVelocity = transform.forward * speed;
    }

    void StopTracking()
    {
        tracking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[Missile] Impactó al jugador!");
            Destroy(gameObject);
        }
    }
}
