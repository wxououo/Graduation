using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlbumOpener : MonoBehaviour
{
    public GameObject AlbumFrame;
    public void OpenAlbum()
    {
        if (AlbumFrame != null)
        {
            bool isActive = AlbumFrame.activeSelf;
            AlbumFrame.SetActive(!isActive);
        }
    }
}
