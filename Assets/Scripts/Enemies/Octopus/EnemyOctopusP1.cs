using UnityEngine;
using System.Collections;

public class EnemyOctopusP1 : MonoBehaviour
{
    public float speed = 3f;
    public float attachDistance = 1.5f;

    public float slowMultiplier = 0.5f;
    public float slowDuration = 3f;
    public float attachDuration = 2f;

    private Transform playerTransform;
    private PlayerStateMachine player;
    private bool attached = false;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerTransform = playerObject.transform;
            player = playerObject.GetComponent<PlayerStateMachine>();
        }
    }

    void Update()
    {
        if (playerTransform == null || attached) return;

        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        transform.LookAt(playerTransform);

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= attachDistance)
        {
            AttachToPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !attached)
        {
            Debug.Log("[EnemyOctopusP1] Colisión con el jugador por trigger");
            AttachToPlayer();
        }
    }

    private void AttachToPlayer()
    {
        if (attached) return;
        attached = true;
        StartCoroutine(AttachRoutine());
    }

    private IEnumerator AttachRoutine()
    {
        Debug.Log("Pulpo P1: Se pegó al jugador");
        
        // Causar daño al jugador
        PlayerHealth playerHealth = playerTransform.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            Debug.Log("Pulpo P1: Causando daño");
            playerHealth.TakeHit();
        }
        else
        {
            Debug.LogError("Pulpo P1: No se encontró PlayerHealth");
        }
        
        transform.SetParent(playerTransform);
        transform.localPosition = new Vector3(0, 0, -1f);

        if (player != null)
        {
            Debug.Log("Pulpo P1: Aplicando ralentización");
            player.ChangeState(new PlayerSlowedState(player, slowDuration, slowMultiplier));
        }

        yield return new WaitForSeconds(attachDuration);

        Debug.Log("Pulpo P1: Se desprende y destruye");
        transform.SetParent(null);
        Destroy(gameObject);
    }
}

