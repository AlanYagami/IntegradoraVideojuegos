using UnityEngine;
using System.Collections;

public class EnemyOctopusP2 : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 2f;

    public float teleportRadius = 10f;
    public float teleportInterval = 3f;

    public float electricFieldRadius = 4f;
    public float slowMultiplier = 0.5f;
    public float slowDuration = 2.5f;
    public float fieldDuration = 1.5f;

    private float teleportTimer;
    private Vector3 startPos;
    private Transform playerTransform;
    private PlayerStateMachine player;

    private bool isFieldActive = false;

    void Start()
    {
        startPos = transform.position;
        teleportTimer = teleportInterval;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
            player = playerObj.GetComponent<PlayerStateMachine>();
        }
    }

    void Update()
    {
        FloatMovement();

        teleportTimer -= Time.deltaTime;
        if (teleportTimer <= 0f)
        {
            Teleport();
            teleportTimer = teleportInterval;
        }
    }

    private void FloatMovement()
    {
        float offsetY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position += new Vector3(0, offsetY * Time.deltaTime, 0);
    }

    private void Teleport()
    {
        Vector3 randomOffset = Random.insideUnitSphere * teleportRadius;
        randomOffset.y = 0;

        Vector3 newPos = startPos + randomOffset;

        Debug.Log("Pulpo P2: Teletransporte a " + newPos);
        transform.position = newPos;
        transform.LookAt(playerTransform);

        StartCoroutine(ActivateElectricField());
    }

    private IEnumerator ActivateElectricField()
    {
        if (isFieldActive) yield break;
        isFieldActive = true;

        Debug.Log("Pulpo P2: Campo eléctrico ACTIVADO");

        GameObject field = new GameObject("ElectricField");
        field.transform.position = transform.position;
        SphereCollider fieldCollider = field.AddComponent<SphereCollider>();
        fieldCollider.isTrigger = true;
        fieldCollider.radius = electricFieldRadius;


        ElectricField ef = field.AddComponent<ElectricField>();
        ef.Initialize(player, slowMultiplier, slowDuration);

        yield return new WaitForSeconds(fieldDuration);

        Debug.Log("Pulpo P2: Campo eléctrico DESACTIVADO");
        Destroy(field);
        isFieldActive = false;
    }
}