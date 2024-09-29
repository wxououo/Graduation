using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOpener : MonoBehaviour
{
    [SerializeField] private GameObject photoFrame;
    public GameObject CameraFrame;
    public void OpenCam()
    {
        if (CameraFrame != null)
        {
            bool isActive = CameraFrame.activeSelf;
            CameraFrame.SetActive(!isActive);
            photoFrame.SetActive(false);
        }
    }
}
