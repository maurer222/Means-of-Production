using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float edgeSize = 10f;
    public float zoomSpeed = 20f;
    public float minZoom = 20f;
    public float maxZoom = 100f;
    public float rotationSpeed = 200f;

    private Transform cameraTransform;
    private Transform cameraParentTransform;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        cameraParentTransform = new GameObject("CameraParent").transform;
        cameraParentTransform.position = cameraTransform.position;
        cameraTransform.parent = cameraParentTransform;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
        HandleRotation();
    }

    void HandleMovement()
    {
        Vector3 direction = Vector3.zero;

        if (Input.mousePosition.x >= Screen.width - edgeSize)
        {
            direction += cameraParentTransform.right;
        }
        if (Input.mousePosition.x <= edgeSize)
        {
            direction -= cameraParentTransform.right;
        }
        if (Input.mousePosition.y >= Screen.height - edgeSize)
        {
            direction += cameraParentTransform.forward;
        }
        if (Input.mousePosition.y <= edgeSize)
        {
            direction -= cameraParentTransform.forward;
        }

        // Prevent movement along the Y axis
        direction.y = 0;

        cameraParentTransform.position += direction * moveSpeed * Time.deltaTime;
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 direction = cameraTransform.forward * scroll * zoomSpeed * Time.deltaTime;

        // Calculate new position
        Vector3 newPosition = cameraTransform.position + direction;

        // Clamp the new position to prevent going below or above certain zoom levels
        float distance = Vector3.Distance(cameraParentTransform.position, newPosition);
        if (distance > minZoom && distance < maxZoom)
        {
            cameraTransform.position = newPosition;
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(2)) // Middle mouse button
        {
            float horizontal = Input.GetAxis("Mouse X");

            // Rotate around the Y axis for horizontal mouse movement
            cameraParentTransform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
