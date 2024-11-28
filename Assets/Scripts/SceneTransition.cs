using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Animator transitionAnim; // 過渡動畫的 Animator
    public string sceneName;        // 要切換的場景名稱

    // 這個方法將與按鈕的 OnClick() 綁定
    public void OnButtonPress()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end"); // 播放過渡動畫
        yield return new WaitForSeconds(1.5f); // 等待動畫播放結束
        SceneManager.LoadScene(sceneName); // 切換場景
    }
}
