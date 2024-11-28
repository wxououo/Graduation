using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 offset; // 滑鼠與物品中心的偏移
    private float zCoord;   // Z 軸上的固定位置
    void Update()
    {
        // 如果按下滑鼠右鍵，才檢測拖曳
        if (Input.GetMouseButtonDown(1)) // 1 是右鍵
        {
            OnMouseDown();
        }

        // 拖曳時，按住右鍵
        if (Input.GetMouseButton(1))
        {
            OnMouseDrag();
        }
    }
    void OnMouseDown()
    {
        // 獲取物品的 z 軸坐標，以保持物品的 z 軸位置不變
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        // 計算滑鼠點擊位置與物品位置的偏移量
        offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        // 拖曳時，根據滑鼠位置更新物品位置
        transform.position = GetMouseWorldPosition() + offset;
    }

    // 獲取滑鼠在世界座標系中的位置
    private Vector3 GetMouseWorldPosition()
    {
        // 獲取滑鼠的螢幕座標
        Vector3 mousePoint = Input.mousePosition;

        // 設定 z 軸保持不變
        mousePoint.z = zCoord;

        // 將螢幕座標轉換為世界座標
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
