using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour
{
    public static DontDestory Instance;
    private void Awake()
    {

        // 檢查是否已有實例
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持物件不被銷毀
        }
        else
        {
            // 如果已有實例，刪除重複的物件
            Destroy(gameObject);
            return;
        }
    }
    }