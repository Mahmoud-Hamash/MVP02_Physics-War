using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 45.0f; // Rotation speed in degrees per second

    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}