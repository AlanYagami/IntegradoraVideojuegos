using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform cameraObject;
    public float speed = 5f;
    public float boostMultiplier = 2f;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // dirección según la cámara
        Vector3 direction = cameraObject.forward * vertical + cameraObject.right * horizontal;
        direction = new Vector3(direction.x, 0, direction.z).normalized;

        // Verificar si Shift está presionado
        bool isBoosting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Si está presionado Shift
        float currentSpeed = isBoosting ? speed * boostMultiplier : speed;

        // Movimiento
        transform.position += direction * currentSpeed * Time.deltaTime;

        // Rotación suave
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
        }
    }
}
