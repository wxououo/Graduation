using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; // �n�����쪺�����W��
    public bool requiresZoom = false; // �O�_�ݭn���Y��j�~���������
    private MouseLook cameraController; // �Ψ��ˬd���Y�O�_�w�վ�

    void Start()
    {
        // ����D�۾��W�� MouseLook �}��
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
        // �ˬd�O�_�ݭn���Y��j
        if (requiresZoom)
        {
            if (cameraController != null && cameraController.HasAdjustedCamera)
            {
                // ���Y�w�վ�A���\��������
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                // ���Y���վ�A�L�k��������
                Debug.Log("You need to zoom in before switching scenes!");
            }
        }
        else
        {
            // ���ݭn���Y��j�A������������
            SceneManager.LoadScene(sceneName);
        }
    }
}
