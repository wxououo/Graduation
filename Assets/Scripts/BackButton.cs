using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public MouseLook MouseLookScript;  // �s���� MouseLook ����
    public Button bButton;  // �s���� Button ����

    void Start()
    {
        // �T�O Button �@�}�l�O���ê�
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

        Debug.Log("HasAdjustedCamera: " + MouseLookScript.HasAdjustedCamera);  // ��� HasAdjustedCamera ���A

        if (MouseLookScript.HasAdjustedCamera)
        {
            bButton.gameObject.SetActive(true); // ��ܫ��s
        }
        else
        {
            bButton.gameObject.SetActive(false); // ���ë��s
        }
    }
}