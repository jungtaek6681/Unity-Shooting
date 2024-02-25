using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraController : MonoBehaviour
{
    [SerializeField] Transform cameraRoot;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] Transform lookTarget;
    [SerializeField] float lookDistance;

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

        Vector3 targetPos;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, lookDistance))
        {
            targetPos = hitInfo.point;
        }
        else
        {
            targetPos = Camera.main.transform.position + Camera.main.transform.forward * lookDistance;
        }
        lookTarget.position = Vector3.Lerp(lookTarget.position, targetPos, 0.1f);
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>();
    }
}
