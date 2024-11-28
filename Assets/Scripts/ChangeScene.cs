using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; // �n�����쪺�����W��
    public void QuitGame() {
        Application.Quit();
    }

    public void LoadScene(string Modern) 
    {
        SceneManager.LoadScene(Modern);
    }

    void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName);
    }
}
