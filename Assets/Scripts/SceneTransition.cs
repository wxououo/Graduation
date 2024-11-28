using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public Animator transitionAnim; // �L��ʵe�� Animator
    public string sceneName;        // �n�����������W��

    // �o�Ӥ�k�N�P���s�� OnClick() �j�w
    public void OnButtonPress()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end"); // ����L��ʵe
        yield return new WaitForSeconds(1.5f); // ���ݰʵe���񵲧�
        SceneManager.LoadScene(sceneName); // ��������
    }
}
