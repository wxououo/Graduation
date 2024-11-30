using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; // 要切換到的場景名稱
    public bool requiresZoom = false; // 是否需要鏡頭放大才能切換場景
    private MouseLook cameraController; // 用來檢查鏡頭是否已調整

    void Start()
    {
        // 獲取主相機上的 MouseLook 腳本
        cameraController = Camera.main.GetComponent<MouseLook>();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string Modern)
    {
        SceneManager.LoadScene(Modern);
    }

    void OnMouseDown()
    {
        // 檢查是否需要鏡頭放大
        if (requiresZoom)
        {
            if (cameraController != null && cameraController.HasAdjustedCamera)
            {
                // 鏡頭已調整，允許切換場景
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                // 鏡頭未調整，無法切換場景
                Debug.Log("You need to zoom in before switching scenes!");
            }
        }
        else
        {
            // 不需要鏡頭放大，直接切換場景
            SceneManager.LoadScene(sceneName);
        }
    }
}
