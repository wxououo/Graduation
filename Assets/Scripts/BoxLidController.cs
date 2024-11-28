using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLidController : MonoBehaviour
{
    private const string BoxLidStateKey = "BoxLidOpen"; // PlayerPrefs key for box lid state
    public Transform lid; // Reference to the lid's Transform component
    private Collider boxCollider; // Reference to the box collider

    private void Start()
    {
        // Get the box collider component attached to the parent or the box itself
        boxCollider = GetComponent<Collider>(); // Assuming the script is attached to the box object

        if (PlayerPrefs.GetInt(BoxLidStateKey, 0) == 1) // Check if the box lid should be open
        {
            OpenLid();
        }
    }

    public void ResetLidState()
    {
        PlayerPrefs.SetInt("BoxLidOpen", 0);
        PlayerPrefs.Save();
        lid.localRotation = Quaternion.identity; // 蓋子回到初始位置
        if (boxCollider != null)
        {
            boxCollider.enabled = true; // 恢復碰撞
        }
    }
    private void OpenLid()
    {
        // Set the lid's rotation to -130 degrees on the X-axis
        lid.localRotation = Quaternion.Euler(-130f, 0f, 0f);
        // Disable the box collider
        if (boxCollider != null)
        {
            boxCollider.enabled = false; // Disable the collider when the lid is opened
            Debug.Log("Box collider disabled.");
        }
        // Ensure the open state is saved persistently
        PlayerPrefs.SetInt(BoxLidStateKey, 1);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        //PlayerPrefs.SetInt(BoxLidStateKey, 0); // Reset the box lid state when leaving the scene
        //PlayerPrefs.Save();
    }
}
