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

    // Start is called before the first frame update
    void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotate = new Vector3(x * sensitivityX, y * sensitivityY, 0);

        // Check if mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
        }

        // Rotate only if mouse button is pressed
        if (isMousePressed)
        {
            transform.eulerAngles = transform.eulerAngles - rotate;
        }
    }
}
