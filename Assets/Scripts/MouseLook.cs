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

    public Vector3 originalPosition;  // 鏡頭的初始位置
    private Quaternion originalRotation;  // 鏡頭的初始旋轉

    private Vector3 lastAdjustedPosition;  // 上一次調整前的位置
    private Quaternion lastAdjustedRotation;  // 上一次調整前的旋轉

    private Vector3 initialMousePosition;  // 滑鼠按下的初始位置
    private float clickThreshold = 5.0f;   // 滑鼠移動的距離閾值

    // 控制道具欄是否開啟的變數
    private bool isInventoryOpen = false;

    public void SetInventoryState(bool state)
    {
        isInventoryOpen = state;
    }

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

        // 如果道具欄開啟，禁止視角移動與點擊檢測
        if (isInventoryOpen)
        {
            return;
        }

        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotate = new Vector3(x * sensitivityX, y * sensitivityY, 0);

        // 當按下滑鼠左鍵
        if (Input.GetMouseButtonDown(0))
        {
            isMousePressed = true;
            initialMousePosition = Input.mousePosition; // 記錄滑鼠按下的初始位置
        }
        // 當釋放滑鼠左鍵
        else if (Input.GetMouseButtonUp(0))
        {
            isMousePressed = false;
            isZooming = false;

            // 計算滑鼠移動距離
            float mouseDistance = Vector3.Distance(initialMousePosition, Input.mousePosition);
            if (mouseDistance <= clickThreshold) // 若滑鼠移動距離小於閾值，才執行點擊
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Transform zoomTarget = hit.transform.Find("ZoomTarget");
                    if (zoomTarget != null && !hasAdjustedCamera)
                    {
                        // 在調整鏡頭前記錄當前位置和旋轉
                        lastAdjustedPosition = Camera.main.transform.position;
                        lastAdjustedRotation = Camera.main.transform.rotation;

                        Camera.main.transform.position = zoomTarget.position;
                        Camera.main.transform.rotation = zoomTarget.rotation;
                        hasAdjustedCamera = true;
                    }
                }
            }
        }

        // 當按下滑鼠右鍵返回到上一次調整前的位置
        if (Input.GetMouseButtonDown(1) && hasAdjustedCamera)
        {
            Camera.main.transform.position = lastAdjustedPosition;
            Camera.main.transform.rotation = lastAdjustedRotation;
            hasAdjustedCamera = false;
        }

        // 拖動視角
        if (isMousePressed && !hasAdjustedCamera)
        {
            transform.eulerAngles = transform.eulerAngles - rotate;
        }
    }
}
