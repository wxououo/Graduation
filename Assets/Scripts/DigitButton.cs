using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DigitButton : MonoBehaviour
{
    public int currentDigit = 0; // 初始數字
    public int maxDigit = 9; // 最大數字
    public int buttonIndex; // 按鈕在密碼中的索引
    private TextMeshProUGUI digitText;
    public BoxController boxController;

    void Start()
    {
        // 獲取按鈕上的文本元件
        digitText = GetComponentInChildren<TextMeshProUGUI>();

        // 如果找不到TextMeshProUGUI元件，則打印錯誤日誌
        if (digitText == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the button! Parent Button: " + gameObject.name);
            
        }
        else
        {
            UpdateDigitText();
        }
    }

    // 當按鈕被點擊時調用此方法
    public void OnButtonClick()
    {
        // 變換數字
        currentDigit = (currentDigit + 1) % (maxDigit + 1);
        UpdateDigitText();

        // 告訴 BoxController 按鈕的當前數字
        if (boxController != null)
        {
            boxController.SetDigit(buttonIndex, currentDigit);
        }
        else
        {
            Debug.LogError("BoxController 未正確分配。");
        }
    }

    // 更新按鈕上的文本
    void UpdateDigitText()
    {
        if (digitText != null)
        {
            digitText.text = currentDigit.ToString();
        }
    }

}
