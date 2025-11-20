using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public Transform spaceship;
    public Transform cameraObject;
    public Vector3 offset;

    float pitch = 0f;

    void Update()
    {
        transform.Rotate(Vector3.up, Input.mousePositionDelta.x * 0.5f);

        pitch -= Input.mousePositionDelta.y * 0.5f;
        pitch = Mathf.Clamp(pitch, -30f, 50f);

        Vector3 direction = (spaceship.position - cameraObject.position).normalized;

        Quaternion baseLook = Quaternion.LookRotation(direction);
        Quaternion verticalTilt = Quaternion.Euler(pitch, 0f, 0f);

        cameraObject.rotation = baseLook * verticalTilt;

        transform.position = Vector3.Lerp(transform.position, spaceship.position, Time.deltaTime * 5);

        cameraObject.localPosition = offset;
    }
}
