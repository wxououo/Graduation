using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    public bool requiresZoom = false;

    private MouseLook cameraController;

    void Start()
    {

        cameraController = Camera.main.GetComponent<MouseLook>();
    }

    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        if (requiresZoom)
        {
            if (cameraController != null && cameraController.HasAdjustedCamera)
            {
                Pickup();
            }
            else
            {
                Debug.Log("no");
            }
        }
        else
        {
            Pickup();
        }
    }
}