using UnityEngine;

public class LetterLookAtCamera : MonoBehaviour
{
    public Transform cameraTransform; // Assign the main camera transform here

    void Update()
    {
        // Calculate the direction from the text object to the camera
        Vector3 directionToCamera = cameraTransform.position - transform.position;
        directionToCamera.y = 0; // Keep the y component zero to constrain rotation to the Y-axis

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        // Apply the target rotation to the text object
        transform.rotation = targetRotation;
    }
}