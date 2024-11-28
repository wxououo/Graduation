using UnityEngine;

public class ZoomItem : MonoBehaviour
{
    public float mouseX ;
    public float mouseY;
    private bool isSelected = false;
    private Vector3 screenCenter;
    private Camera mainCamera;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }
        else
        {
            screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            originalPosition = transform.position;
            originalRotation = transform.rotation;
        }
    }

    void Update()
    {
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSelected = false;
                transform.position = originalPosition;
                transform.rotation = originalRotation;
            }
            else if (Input.GetMouseButton(1))
            {

                float rotationSpeed = 6.0f;
                float mouseX = Input.GetAxis("Mouse X");
                float mouseY = Input.GetAxis("Mouse Y");
                transform.Rotate(Vector3.up, -mouseX* rotationSpeed, Space.World);
                transform.Rotate(Vector3.right, -mouseY* rotationSpeed, Space.World);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Check if the mouse is over this object
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
                {
                    isSelected = true;
                    transform.position = mainCamera.ScreenToWorldPoint(screenCenter + Vector3.forward * 2);
                }
            }
        }
    }
}
