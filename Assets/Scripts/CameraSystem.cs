using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    public Transform spaceship;
    public Transform cameraObject;
    public Vector3 offset;
    void Update()
    {
        Vector3 direction = (spaceship.position - cameraObject.position).normalized;

        cameraObject.rotation = Quaternion.LookRotation(direction);

        transform.Rotate(Vector3.up, Input.mousePositionDelta.x * 0.5f);
        //transform.rotation = Quaternion.LookRotation(direction);
        //transform.position = spaceship.position;

        transform.position = Vector3.Lerp(transform.position, spaceship.position, Time.deltaTime * 5);

        cameraObject.localPosition = offset;
    }
}
