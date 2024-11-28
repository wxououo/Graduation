using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxController : MonoBehaviour
{
    public int[] currentDigits = new int[4]; // 保存每個按鈕的當前數字
    public int[] correctCode = { 4, 7, 5, 4 }; // 預設的正確密碼
    public string originalSceneName; // 原始場景的名稱
    private const string BoxLidStateKey = "BoxLidOpen";


    // 當某個按鈕數字變更時調用此方法
    public void SetDigit(int index, int digit)
    {
        currentDigits[index] = digit;
        Debug.Log("按鈕 " + index + " 設置為 " + digit);

        // 檢查每個按鈕的數字是否都正確
        if (CheckCode())
        {
            Unlock();
        }
    }
    // 檢查所有按鈕的數字是否正確
    private bool CheckCode()
    {
        for (int i = 0; i < correctCode.Length; i++)
        {
            if (currentDigits[i] != correctCode[i])
            {
                return false;
            }
        }
        return true;
    }

    private void Unlock()
    {
        Debug.Log("箱子解鎖成功！");
        PlayerPrefs.SetInt(BoxLidStateKey, 1); // Set the box lid state to open
        PlayerPrefs.Save();

        SceneManager.LoadScene(originalSceneName);
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(BoxLidStateKey, 0) == 1)
        {
            Unlock(); // 如果箱子已解鎖，則打開蓋子並顯示物品
        }
    }
}