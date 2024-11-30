using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumOpener : MonoBehaviour
{
    [SerializeField] private GameObject buttonToHide;
    public GameObject AlbumFrame;
    public void OpenAlbum()
    {
        if (AlbumFrame != null)
        {
            bool isActive = AlbumFrame.activeSelf;
            AlbumFrame.SetActive(!isActive);
            if (buttonToHide != null)
            {
                buttonToHide.SetActive(isActive);
            }
        }
    }
}
