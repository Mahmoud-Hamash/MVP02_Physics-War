using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public float moveDistance = 2f; // Movement range
    public float moveSpeed = 2f; // Speed of movement

    private Vector3 startPos;

    void Start()
    {
        gameObject.SetActive(false);
        startPos = transform.position; // Store the initial position
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPos + transform.up * offset; // Move along the arrow's up
    }
}