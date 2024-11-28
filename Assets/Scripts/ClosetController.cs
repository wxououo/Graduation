using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetController : MonoBehaviour
{
    private bool isOpened = false;
    public Transform pivot; // Reference to the pivot GameObject
    private Quaternion closedRotation; // Store as a Quaternion
    private Quaternion openedRotation; // Opened rotation as a Quaternion
    public float rotationSpeed = 100f; // Speed for rotation
    public bool isLeftDoor = false; // Set this to true for the left door, false for the right door
    private float precisionThreshold = 1f; // Precision tolerance for stopping rotation

    void Start()
    {
        // Record the pivot's initial rotation
        closedRotation = pivot.localRotation; // Using Quaternion for smoother rotation

        // Set the target opened rotation based on the pivot's initial rotation
        if (isLeftDoor)
        {
            openedRotation = Quaternion.Euler(closedRotation.eulerAngles.x, closedRotation.eulerAngles.y - 90f, closedRotation.eulerAngles.z); // Left door opens outward
        }
        else
        {
            openedRotation = Quaternion.Euler(closedRotation.eulerAngles.x, closedRotation.eulerAngles.y + 90f, closedRotation.eulerAngles.z); // Right door opens outward
        }

    }

    void OnMouseDown()
    {
        Debug.Log("Door clicked, toggling door.");
        StopAllCoroutines(); // Stop any ongoing movement
        if (isOpened)
        {
            StartCoroutine(CloseDoor());
        }
        else
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        isOpened = true;

        
        // Smoothly rotate the door using the pivot
        while (Quaternion.Angle(pivot.localRotation, openedRotation) > precisionThreshold)
        {
            pivot.localRotation = Quaternion.RotateTowards(pivot.localRotation, openedRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        // Snap to the final rotation to avoid overshooting
        pivot.localRotation = openedRotation;

        
    }

    IEnumerator CloseDoor()
    {
        isOpened = false;


        // Smoothly rotate the door back to the closed state
        while (Quaternion.Angle(pivot.localRotation, closedRotation) > precisionThreshold)
        {
            pivot.localRotation = Quaternion.RotateTowards(pivot.localRotation, closedRotation, Time.deltaTime * rotationSpeed);
            yield return null;
        }

        // Snap to the final closed rotation
        pivot.localRotation = closedRotation;

    }
}
