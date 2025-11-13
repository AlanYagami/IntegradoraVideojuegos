using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int maxHits = 3;               // 3 golpes = destrucción
    private int hitsTaken = 0;

    public event Action<int,int> OnHealthChanged; // (hitsTaken, maxHits)
    public float respawnDelay = 2f; // si quieres reiniciar en vez de destruir

    [Header("Invuln")]
    public float invulnTime = 0.8f;
    private bool isInvulnerable = false;

    void Start()
    {
        hitsTaken = 0;
        OnHealthChanged?.Invoke(hitsTaken, maxHits);
    }

    public void TakeHit()
    {
        if (isInvulnerable) return;
        hitsTaken++;
        StartCoroutine(InvulnCoroutine());
        OnHealthChanged?.Invoke(hitsTaken, maxHits);

        if (hitsTaken >= maxHits)
        {
            Die();
        }
    }

    System.Collections.IEnumerator InvulnCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnTime);
        isInvulnerable = false;
    }

    public void HealOne()
    {
        hitsTaken = Mathf.Max(0, hitsTaken - 1);
        OnHealthChanged?.Invoke(hitsTaken, maxHits);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}