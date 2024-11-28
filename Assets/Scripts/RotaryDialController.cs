using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement;

public class RotaryDialController : MonoBehaviour
{
    public Transform dial; // 輪盤的Transform
    public float[] numberAngles = {-30f, 300f, 270f, 240f, 210f, 180f, 150f, 120f, 60f, 30f}; // 每個數字的角度位置
    private string currentInput = ""; // 當前輸入的數字序列
    public string correctCode = "51571"; // 密碼
    public float rotationSpeed = 400f; // 旋轉速度
    public float waitTime = 1f; // 等待時間
    public string originalSceneName; // 原始場景的名稱
    public List<Button> numberButtons;

    // 當某個數字按鈕被點擊時調用
    public void OnNumberButtonClick(int number)
    {
        if (currentInput.Length < correctCode.Length)
        {
            currentInput += number.ToString();
            DisableButtons();
            // 開始旋轉協程
            StartCoroutine(RotateDialToNumberAndBack(number));

            // 如果輸入完成，檢查密碼
            if (currentInput.Length == correctCode.Length)
            {
                CheckCode();
            }
        }
    }

    private IEnumerator RotateDialToNumberAndBack(int number)
    {
        float targetAngle = numberAngles[number];
        float startAngle = dial.localEulerAngles.z;

        // 旋轉到目標角度
        while (Mathf.Abs(Mathf.DeltaAngle(dial.localEulerAngles.z, targetAngle)) > 0.1f)
        {
            float angle = Mathf.MoveTowardsAngle(dial.localEulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            dial.localEulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }

        // 等待一段時間
        yield return new WaitForSeconds(waitTime);

        // 旋轉回初始角度
        while (Mathf.Abs(Mathf.DeltaAngle(dial.localEulerAngles.z, startAngle)) > 0.1f)
        {
            float angle = Mathf.MoveTowardsAngle(dial.localEulerAngles.z, startAngle, rotationSpeed * Time.deltaTime);
            dial.localEulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }
        EnableButtons();

    }

    private void CheckCode()
    {
        if (currentInput == correctCode)
        {
            Unlock();
        }
        else
        {
            ResetInput();
        }
    }

    private void Unlock()
    {
        Debug.Log("好像有什麼出現了！");
        PlayerPrefs.SetInt("IsUnlocked", 1); // 保存解鎖狀態
        PlayerPrefs.Save(); // 確保狀態被保存
        SceneManager.LoadScene(originalSceneName);
    }

    private void ResetInput()
    {
        currentInput = "";
    }

    private void DisableButtons()
    {
        foreach (Button button in numberButtons)
        {
            button.interactable = false;
        }
    }

    private void EnableButtons()
    {
        foreach (Button button in numberButtons)
        {
            button.interactable = true;
        }
    }
}
