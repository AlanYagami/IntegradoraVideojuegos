using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotar alrededor del eje Y local del pivot (comportamiento estable)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
