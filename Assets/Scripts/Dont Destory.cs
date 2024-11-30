using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour
{
    public static DontDestory Instance;
    private void Awake()
    {

        // �ˬd�O�_�w�����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �O�����󤣳Q�P��
        }
        else
        {
            // �p�G�w����ҡA�R�����ƪ�����
            Destroy(gameObject);
            return;
        }
    }
    }