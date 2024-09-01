using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraObject;


    private Texture2D screenCapture;
    private bool viewPhoto;

    private void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cameraObject != null && cameraObject.activeInHierarchy)
            {
                photoFrame.SetActive(false);
                if (!viewPhoto)
                {
                    StartCoroutine(CapturePhoto());
                }
                else
                {
                    RemovePhoto();
                }
            }
        }
    }
    IEnumerator CapturePhoto()
    {
        viewPhoto = true;
        cameraObject.SetActive(false);
        yield return new WaitForEndOfFrame();

        // 定义要读取的屏幕区域
        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);
        // 读取指定区域的像素并存储在 screenCapture 中
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        cameraObject.SetActive(true);
        ShowPhoto();
    }
    void ShowPhoto()
    {
        // 创建一个新的 Sprite 对象，使用捕获的 Texture2D 对象和定义的区域
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.0f, 0.0f), 100.0f);
        // 将新创建的 Sprite 对象赋值给 photoDisplayArea 的 sprite 属性
        photoDisplayArea.sprite = photoSprite;
        photoFrame.SetActive(true);
    }
    void RemovePhoto()
    {
        viewPhoto = false;
        photoFrame.SetActive(false);
        cameraObject.SetActive(true);
    }
}
