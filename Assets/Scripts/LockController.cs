using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockController : MonoBehaviour
{
    public float rotationSpeed = 100f;  // 旋轉速度
    public int currentNumber = 0;       // 當前數字
    public int buttonIndex; // 按鈕在密碼中的索引
    private float currentRotation = 0f;
    public Transform wheelTransform;    // 確保已正確引用
    public BoxController boxController;


    void Start()
    {
        // 檢查引用是否正確賦值
        if (wheelTransform == null)
        {
            Debug.LogError("wheelTransform is not assigned!");
        }
    }
    void Update() 
{
    if (Input.GetMouseButtonDown(0)) 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
    }
}
    void OnMouseDrag()
    {
        if (wheelTransform == null) return;  // 防止空引用異常

        // 獲取滑鼠 Y 軸的移動量
        float mouseY = Input.GetAxis("Mouse Y");
        
        // 根據滑鼠移動量來旋轉滾輪
        float rotationAmount = mouseY * rotationSpeed * Time.deltaTime;
        wheelTransform.Rotate(Vector3.right, rotationAmount, Space.Self);  // 本地 X 軸旋轉
        currentRotation += rotationAmount;

        // 限制每次轉動 36 度（對應每個數字）
        if (Mathf.Abs(currentRotation) >= 22.5f)
        {
            // 更新當前數字
            if (currentRotation > 0)
                currentNumber = (currentNumber + 1) % 8;
            else
                currentNumber = (currentNumber - 1 + 8) % 8;

            // 重設旋轉角度
            currentRotation = 0f;

            // 顯示當前數字
            Debug.Log("Current Number: " + currentNumber);
        }

        // 旋轉物件，模擬滾輪轉動
        wheelTransform.Rotate(Vector3.right, rotationAmount);

        if (boxController != null)
        {
            boxController.SetDigit(buttonIndex, currentNumber);
        }
        else
        {
            Debug.LogError("BoxController 未正確分配。");
        }
    }
}
