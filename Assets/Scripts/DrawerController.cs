using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerController : MonoBehaviour
{
    
    private bool isOpened = false;
    void OnMouseDown()
    {
        ToggleDrawer(); // 當抽屜被點擊時，切換抽屜狀態
    }
    void ToggleDrawer()
    {
        isOpened = !isOpened; // 切換抽屜狀態

        // 根據抽屜狀態，設置抽屜的位置
        if (isOpened)
        {
            transform.Translate(-200f, 0f, 0f); // 向前移動一定距離
        }
        else
        {
            transform.Translate(200f, 0f, 0f); // 向後移動一定距離
        }
    }
}
