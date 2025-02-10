using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public Vector3 moveAxis = Vector3.forward; // Axis of movement (set in Inspector)
    public float moveDistance = 2f; // How far the arrow moves
    public float moveSpeed = 2f; // Speed of movement

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPos + moveAxis.normalized * offset;
    }
}