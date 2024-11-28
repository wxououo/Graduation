using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeController : MonoBehaviour
{
    public ChangeToOriginalScene changeToOriginalSceneScript; // 引用ChangeToOriginalScene腳本
    public Button[] InvisibleButtons; // 引用不可見的按鈕

     void Start()
    {
        foreach (Button btn in InvisibleButtons)
        {
            if (btn != null)
            {
                btn.onClick.AddListener(() => OnInvisibleButtonClick(btn));
            }
        }
    }

    private void OnInvisibleButtonClick(Button btn)
    {
        if (changeToOriginalSceneScript != null)
        {
            changeToOriginalSceneScript.SetShouldChangeScene(true);
            changeToOriginalSceneScript.ChangeSceneImmediately();
        }
    }
}
