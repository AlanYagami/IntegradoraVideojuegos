using UnityEngine;

[ExecuteAlways]
public class AutoAssignHelper : MonoBehaviour
{
    void Start()
    {
        var player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogWarning("AutoAssignHelper: No PlayerController found in scene.");
            return;
        }

        if (player.bulletPrefab == null)
        {
            Debug.LogWarning("PlayerController.bulletPrefab is null. Attempting to load 'Bullet' from Resources folder.");
            GameObject res = Resources.Load<GameObject>("Bullet");
            if (res != null)
            {
                player.bulletPrefab = res;
                Debug.Log("AutoAssignHelper: Assigned 'Bullet' prefab from Resources to PlayerController.bulletPrefab.");
            }
            else
            {
                Debug.LogWarning("AutoAssignHelper: No 'Bullet' prefab found in Resources. Open the editor utility (Tools/PowerUp Setup/Configure Player) and assign a bullet prefab.");
            }
        }
    }
}
