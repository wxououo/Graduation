using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOpener : MonoBehaviour
{
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject buttonToHide; // 新增要隐藏的按钮
    public GameObject CameraFrame;

    public void OpenCam()
    {
        if (CameraFrame != null)
        {
            bool isActive = CameraFrame.activeSelf;
            CameraFrame.SetActive(!isActive);
            photoFrame.SetActive(false);

            // 根据 CameraFrame 的状态设置按钮的显示状态
            if (buttonToHide != null)
            {
                buttonToHide.SetActive(isActive);
            }
        }
    }
}
