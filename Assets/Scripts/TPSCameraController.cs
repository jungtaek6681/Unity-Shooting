using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraController : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] private float mouseSensitivity;

    private Vector2 lookDelta;
    private float xRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        Look();
    }

    private void Look()
    {
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * lookDelta.x * mouseSensitivity * Time.deltaTime);
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
