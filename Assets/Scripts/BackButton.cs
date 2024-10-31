using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public MouseLook MouseLookScript;  // 連結到 MouseLook 元件
    public Button bButton;  // 連結到 Button 元件

    void Start()
    {
        // 確保 Button 一開始是隱藏的
        if (bButton != null)
        {
            bButton.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (MouseLookScript == null || bButton == null)
        {
            Debug.LogError("MouseLookScript or bButton is not assigned.");
            return;
        }

        Debug.Log("HasAdjustedCamera: " + MouseLookScript.HasAdjustedCamera);  // 顯示 HasAdjustedCamera 狀態

        if (MouseLookScript.HasAdjustedCamera)
        {
            bButton.gameObject.SetActive(true); // 顯示按鈕
        }
        else
        {
            bButton.gameObject.SetActive(false); // 隱藏按鈕
        }
    }
}