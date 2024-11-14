using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float x;
    private float y;
    public float sensitivityX = -1f;
    public float sensitivityY = 0.5f;
    private Vector3 rotate;
    private bool isMousePressed = false;

    private bool isZooming = false;

    private bool hasAdjustedCamera = false;
    public bool HasAdjustedCamera
    {
        get { return hasAdjustedCamera; }
    }

    public Vector3 originalPosition;  // ���Y����l��m
    private Quaternion originalRotation;  // ���Y����l����

    private Vector3 lastAdjustedPosition;  // �W�@���վ�e����m
    private Quaternion lastAdjustedRotation;  // �W�@���վ�e������

    void Start()
    {
        Cursor.visible = true;
        originalPosition = Camera.main.transform.position;
        originalRotation = Camera.main.transform.rotation;
    }

    void Update()
    {
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found.");
            return;
        }

        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotate = new Vector3(x * sensitivityX, y * sensitivityY, 0);

        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Transform zoomTarget = hit.transform.Find("ZoomTarget");
                if (zoomTarget != null && !hasAdjustedCamera)
                {
                    // �b�վ����Y�e�O����e��m�M����
                    lastAdjustedPosition = Camera.main.transform.position;
                    lastAdjustedRotation = Camera.main.transform.rotation;

                    Camera.main.transform.position = zoomTarget.position;
                    Camera.main.transform.rotation = zoomTarget.rotation;
                    hasAdjustedCamera = true;
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
            isZooming = false;
        }

        // �k���I����^��W�@���վ�e����m
        if (Input.GetMouseButtonDown(1) && hasAdjustedCamera)
        {
            Camera.main.transform.position = lastAdjustedPosition;
            Camera.main.transform.rotation = lastAdjustedRotation;
            hasAdjustedCamera = false;
        }

        if (isMousePressed && !hasAdjustedCamera)
        {
            transform.eulerAngles = transform.eulerAngles - rotate;
        }
    }
}
