#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class PowerUpSetupEditor : EditorWindow
{
    private GameObject playerObject;
    private GameObject bulletPrefab;
    private Vector3 leftOffset = new Vector3(-0.3f, 0f, 0f);
    private Vector3 rightOffset = new Vector3(0.3f, 0f, 0f);
    private Vector3 forwardOffset = new Vector3(0f, 0.5f, 0f);

    [MenuItem("Tools/PowerUp Setup/Configure Player")] 
    public static void ShowWindow()
    {
        GetWindow<PowerUpSetupEditor>("PowerUp Setup");
    }

    void OnGUI()
    {
        GUILayout.Label("Configure Player for Shooting & PowerUps", EditorStyles.boldLabel);

        playerObject = (GameObject)EditorGUILayout.ObjectField("Player GameObject", playerObject, typeof(GameObject), true);
        bulletPrefab = (GameObject)EditorGUILayout.ObjectField("Bullet Prefab", bulletPrefab, typeof(GameObject), false);

        GUILayout.Space(6);
        GUILayout.Label("Firepoint Offsets (local)", EditorStyles.label);
        leftOffset = EditorGUILayout.Vector3Field("Left Offset", leftOffset);
        rightOffset = EditorGUILayout.Vector3Field("Right Offset", rightOffset);
        forwardOffset = EditorGUILayout.Vector3Field("Forward Offset", forwardOffset);

        GUILayout.Space(8);
        if (GUILayout.Button("Configure Selected Player"))
        {
            ConfigurePlayer();
        }

        GUILayout.Space(6);
        if (GUILayout.Button("Help: Quick Steps"))
        {
            EditorUtility.DisplayDialog("PowerUp Setup - Quick Steps",
                "1) Assign the Player GameObject and a Bullet Prefab (preferably with ProjectileBullet script).\n" +
                "2) Click Configure to add components and create fire points.\n" +
                "3) In the Inspector for the player, check PlayerController.bulletPrefab and assign manually if still empty.\n" +
                "4) Ensure Player has tag 'Player' and an AudioSource for PlayerSoundController.", "OK");
        }
    }

    void ConfigurePlayer()
    {
        if (playerObject == null)
        {
            EditorUtility.DisplayDialog("Error", "Assign a Player GameObject first.", "OK");
            return;
        }

        // Ensure the selected object is saved in the scene
        GameObject player = playerObject;

        // Ensure tag
        if (player.tag != "Player")
        {
            Undo.RecordObject(player, "Set Player Tag");
            player.tag = "Player";
            Debug.Log("Set tag 'Player' on selected GameObject.");
        }

        // Add PlayerController
        var playerController = player.GetComponent<PlayerController>();
        if (playerController == null)
        {
            playerController = Undo.AddComponent<PlayerController>(player);
            Debug.Log("Added PlayerController component.");
        }

        // Add PlayerHealth
        var playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            playerHealth = Undo.AddComponent<PlayerHealth>(player);
            Debug.Log("Added PlayerHealth component.");
        }

        // Add FireModeManager
        var fireModeManager = player.GetComponent<FireModeManager>();
        if (fireModeManager == null)
        {
            fireModeManager = Undo.AddComponent<FireModeManager>(player);
            Debug.Log("Added FireModeManager component.");
        }

        // Add PlayerSoundController and AudioSource
        var soundController = player.GetComponent<PlayerSoundController>();
        if (soundController == null)
        {
            soundController = Undo.AddComponent<PlayerSoundController>(player);
            Debug.Log("Added PlayerSoundController component.");
        }

        var audioSource = player.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = Undo.AddComponent<AudioSource>(player);
            audioSource.playOnAwake = false;
            Debug.Log("Added AudioSource component.");
        }
        soundController.audioSource = audioSource;

        // Create firePoint if missing
        if (playerController.firePoint == null)
        {
            GameObject fp = new GameObject("FirePoint");
            Undo.RegisterCreatedObjectUndo(fp, "Create FirePoint");
            fp.transform.parent = player.transform;
            fp.transform.localPosition = forwardOffset;
            fp.transform.localRotation = Quaternion.identity;
            playerController.firePoint = fp.transform;
            Debug.Log("Created FirePoint child.");
        }

        // Create left/right firePoints if missing
        if (playerController.firePointLeft == null)
        {
            GameObject fpl = new GameObject("FirePoint_Left");
            Undo.RegisterCreatedObjectUndo(fpl, "Create FirePoint_Left");
            fpl.transform.parent = player.transform;
            fpl.transform.localPosition = leftOffset + forwardOffset;
            fpl.transform.localRotation = Quaternion.identity;
            playerController.firePointLeft = fpl.transform;
            Debug.Log("Created FirePoint_Left child.");
        }

        if (playerController.firePointRight == null)
        {
            GameObject fpr = new GameObject("FirePoint_Right");
            Undo.RegisterCreatedObjectUndo(fpr, "Create FirePoint_Right");
            fpr.transform.parent = player.transform;
            fpr.transform.localPosition = rightOffset + forwardOffset;
            fpr.transform.localRotation = Quaternion.identity;
            playerController.firePointRight = fpr.transform;
            Debug.Log("Created FirePoint_Right child.");
        }

        // Add Collider if missing (needed for trigger interactions)
        Collider existingCollider = player.GetComponent<Collider>();
        if (existingCollider == null)
        {
            var col = Undo.AddComponent<CapsuleCollider>(player);
            col.isTrigger = false;
            Debug.Log("Added CapsuleCollider to player for collision detection.");
        }

        // Add Rigidbody if missing (set kinematic so movement via Translate still works)
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = Undo.AddComponent<Rigidbody>(player);
            rb.isKinematic = true;
            rb.useGravity = false;
            Debug.Log("Added Rigidbody (isKinematic=true) to player to allow trigger callbacks.");
        }

        // Assign bullet prefab if provided
        if (bulletPrefab != null)
        {
            playerController.bulletPrefab = bulletPrefab;
            EditorUtility.SetDirty(playerController);
            Debug.Log("Assigned Bullet Prefab to PlayerController.");

            // Check if prefab has ProjectileBullet component
            var rootPath = AssetDatabase.GetAssetPath(bulletPrefab);
            GameObject loaded = (GameObject)AssetDatabase.LoadAssetAtPath(rootPath, typeof(GameObject));
            if (loaded != null)
            {
                var prefabHas = loaded.GetComponent<ProjectileBullet>();
                if (prefabHas == null)
                {
                    if (EditorUtility.DisplayDialog("Prefab missing ProjectileBullet",
                        "The selected bullet prefab does not have the 'ProjectileBullet' script. Add it to the prefab now?",
                        "Yes", "No"))
                    {
                        // Add component to the prefab
                        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(loaded);
                        instance.AddComponent<ProjectileBullet>();
                        PrefabUtility.SaveAsPrefabAsset(instance, rootPath);
                        DestroyImmediate(instance);
                        Debug.Log("Added ProjectileBullet to prefab and saved.");
                    }
                }
            }
        }

        // Mark scene dirty
        EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());

        EditorUtility.DisplayDialog("PowerUp Setup", "Configuration applied. Review PlayerController in Inspector.", "OK");
    }
}
#endif
