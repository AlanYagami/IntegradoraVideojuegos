using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("El enemigo recibió daño. Vida restante: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Notificar al GameManager para sumar puntos (ej. 10 puntos por enemigo común)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(10);
        }

        // Aquí podrías poner animación o efecto de explosión
        Destroy(gameObject);
    }
}