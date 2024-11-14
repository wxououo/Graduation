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
    [SerializeField] private InventoryManager inventoryManager;


    private bool viewPhoto;
    private HashSet<Transform> photographedObjects = new HashSet<Transform>(); // 存储已拍摄过的物体
    private List<Item> capturedPhotos = new List<Item>(); // 存储已保存的照片

    // 自定义照片宽高
    public int photoWidth = 800;
    public int photoHeight = 600;

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

        // 检测相机前方是否有目标物体
        RaycastHit hit;
        bool itemDetected = Physics.Raycast(cameraObject.transform.position, cameraObject.transform.forward, out hit, 5000.0f);

        // 创建一个新的 Texture2D 来存储每次拍摄的照片
        Texture2D screenCapture = new Texture2D(photoWidth, photoHeight, TextureFormat.RGB24, false);
        Rect regionToRead = new Rect((Screen.width - photoWidth) / 2, (Screen.height - photoHeight) / 2, photoWidth, photoHeight);
        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();

        cameraObject.SetActive(true);
        ShowPhoto(screenCapture);

        // 如果检测到目标物体且该物体有 PhotoTarget 组件，并且该物体尚未拍摄过照片
        if (itemDetected && hit.transform.GetComponent<PhotoTarget>() != null && !photographedObjects.Contains(hit.transform))
        {
            string photoName = hit.transform.GetComponent<PhotoTarget>().photoName; // 获取物体的名称
            SavePhotoAsItem(screenCapture, hit.transform, photoName); // 使用物体的名称来命名照片
            Debug.Log("Photo saved as item: " + photoName);
        }
    }

    void ShowPhoto(Texture2D photoTexture)
    {
        Sprite photoSprite = Sprite.Create(photoTexture, new Rect(0.0f, 0.0f, photoTexture.width, photoTexture.height), new Vector2(0.5f, 0.5f), photoHeight);
        photoDisplayArea.sprite = photoSprite;
        photoFrame.SetActive(true);
    }

    void RemovePhoto()
    {
        viewPhoto = false;
        photoFrame.SetActive(false);
        cameraObject.SetActive(true);
    }

    void SavePhotoAsItem(Texture2D photoTexture, Transform targetObject, string photoName)
    {
        // 创建一个新照片道具并设置其名称和图标
        Item photoItem = new Item();
        photoItem.name = photoName; // 使用传入的物体名称
        photoItem.icon = Sprite.Create(photoTexture, new Rect(0.0f, 0.0f, photoTexture.width, photoTexture.height), new Vector2(0.5f, 0.5f), 100.0f);

        // 設定物品名稱
        photoItem.itemName = photoName;

        // 将照片道具添加到清单
        capturedPhotos.Add(photoItem);
        inventoryManager.Add(photoItem);  // 保存到 Inventory

        // 记录已经拍摄过该物体，避免再次拍照
        photographedObjects.Add(targetObject);

        Debug.Log("Photo saved as item: " + photoName);
    }
}
