using UnityEngine;

public class ElectricField : MonoBehaviour
{
    private PlayerStateMachine player;
    private float slowMultiplier;
    private float slowDuration;
    private float radius;

    private bool hasAppliedSlow = false;

    public void Initialize(PlayerStateMachine player, float multiplier, float duration)
    {
        this.player = player;
        slowMultiplier = multiplier;
        slowDuration = duration;

        SphereCollider sc = GetComponent<SphereCollider>();
        if (sc != null)
            radius = sc.radius;
    }

    private void Update()
    {
        if (player == null || hasAppliedSlow) return;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= radius)
        {
            Debug.Log($"Campo eléctrico: jugador afectado por {slowDuration} s");
            player.ChangeState(new PlayerSlowedState(player, slowDuration, slowMultiplier));
            hasAppliedSlow = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 0.7f, 1f, 0.3f);
        SphereCollider sc = GetComponent<SphereCollider>();

        float r = sc != null ? sc.radius : radius;
        Gizmos.DrawSphere(transform.position, r);

        Gizmos.color = new Color(0f, 0.9f, 1f, 1f);
        Gizmos.DrawWireSphere(transform.position, r);
    }
}
