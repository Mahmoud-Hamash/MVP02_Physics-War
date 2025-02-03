using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrebuchetController : MonoBehaviour
{
    public Rigidbody weight;
    public GameObject pumpkin;
    public Material trailMaterial;
    public float trailDuration = 2f;

    public TextMeshProUGUI counterweightText;
    public TextMeshProUGUI pumpkinWeightText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI forceText;

    public GameObject objectToActivate; // Object to activate when pumpkin is released

    private TrailRenderer trail;
    private Rigidbody pumpkinRb;
    private Vector3 lastVelocity;
    private Vector3 currentAcceleration;

    void Start()
    {
        // Add a TrailRenderer to the pumpkin
        trail = pumpkin.AddComponent<TrailRenderer>();
        trail.time = trailDuration;
        trail.startWidth = 0.2f;
        trail.endWidth = 0.05f;
        trail.material = trailMaterial;
        trail.startColor = Color.yellow;
        trail.endColor = Color.red;
        trail.enabled = false; // Disable until launch

        pumpkinRb = pumpkin.GetComponent<Rigidbody>();

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // Ensure it's initially inactive
        }
    }

    void Update()
    {
        // Update UI Texts
        counterweightText.text = "Counter weight: " + weight.mass + " kg";
        pumpkinWeightText.text = "Projectile Weight: " + pumpkinRb.mass + " kg";

        float speed = pumpkinRb.velocity.magnitude;
        speedText.text = "Speed: " + speed.ToString("F2") + " m/s";

        float force = pumpkinRb.mass * currentAcceleration.magnitude;
        forceText.text = "Force: " + force.ToString("F2") + " N";

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Release the weight
            weight.isKinematic = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // Launch the pumpkin
            HingeJoint hingeToDestroy = pumpkin.GetComponent<HingeJoint>();
            if (hingeToDestroy != null)
            {
                Destroy(hingeToDestroy);
            }

            // Enable the trail
            trail.Clear();
            trail.enabled = true;

            // Activate the specified object
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
            }
        }
    }

    void FixedUpdate()
    {
        // Calculate acceleration based on physics update
        currentAcceleration = (pumpkinRb.velocity - lastVelocity) / Time.fixedDeltaTime;
        lastVelocity = pumpkinRb.velocity;
    }
}
