using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TrebuchetControllerUpdated : MonoBehaviour
{
    public Rigidbody weight;
    public GameObject pumpkin;
    public Rigidbody pumpkinRb;
    public Material trailMaterial;
    public float trailDuration = 2f;

    public TextMeshProUGUI counterweightText;
    public TextMeshProUGUI pumpkinWeightText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI forceText;

    public GameObject objectToActivate; // Object to activate when pumpkin is released

    private TrailRenderer trail;
    private Vector3 lastVelocity;
    private Vector3 currentAcceleration;

    public float releaseDelay = 0.7f; // Adjust this to fine-tune the hinge release timing
    public float launchSpeed = 10f; // Adjust launch force

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

        // pumpkinRb = pumpkin.GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Update UI Texts if projectile is active
        if (pumpkin != null)
        {
            counterweightText.text = "Counter weight: " + weight.mass + " kg";
            pumpkinWeightText.text = "Projectile Weight: " + pumpkinRb.mass + " kg";
    
            float speed = pumpkinRb.velocity.magnitude;
            speedText.text = "Speed: " + speed.ToString("F2") + " m/s";
    
            float force = pumpkinRb.mass * currentAcceleration.magnitude;
            forceText.text = "Force: " + force.ToString("F2") + " N";
        }
        
        // Debug with "Space"
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     FireTrebuchet();
        // }
    }

    void FixedUpdate()
    {
        // Calculate acceleration based on physics update
        if (pumpkin != null)
        {
            currentAcceleration = (pumpkinRb.velocity - lastVelocity) / Time.fixedDeltaTime;
            lastVelocity = pumpkinRb.velocity;
        }
        
    }

    public void FireTrebuchet()
    {
        if (pumpkin.activeSelf) // Ready to fire
        {
            // Release the counterweight immediately
            weight.isKinematic = false;
    
            // Start coroutine to release the pumpkin after a short delay
            StartCoroutine(ReleasePumpkin());
        }
        else
        {
            Debug.Log("Firing attempt failed. Need to load projectile.");
        }
        
    }

    IEnumerator ReleasePumpkin()
    {
        yield return new WaitForSeconds(releaseDelay);

        // Ensure the pumpkin still exists before proceeding
        if (pumpkin == null || !pumpkin)
        {
            Debug.LogWarning("Pumpkin was destroyed before releasing!");
            yield break; // Stop the coroutine
        }

        // Remove the hinge to release the projectile
        HingeJoint hingeToDestroy = pumpkin.GetComponent<HingeJoint>();
        if (hingeToDestroy != null)
        {
            Destroy(hingeToDestroy);
        }

        // Ensure Rigidbody still exists
        if (pumpkinRb == null || !pumpkinRb)
        {
            Debug.LogWarning("Pumpkin Rigidbody was destroyed before release!");
            yield break;
        }

        // Get the current speed (magnitude of velocity)
        float speed = pumpkinRb.velocity.magnitude;

        // Compute the horizontal direction (XZ-plane)
        Vector3 horizontalDirection = new Vector3(pumpkinRb.velocity.x, 0f, pumpkinRb.velocity.z).normalized;

        // Compute new velocity components for a 45-degree launch
        Vector3 newVelocity = horizontalDirection * (speed * Mathf.Cos(45 * Mathf.Deg2Rad)) 
                              + Vector3.up * (speed * Mathf.Sin(45 * Mathf.Deg2Rad));

        // Apply the new velocity
        pumpkinRb.velocity = newVelocity;

        // Debug: Print the corrected launch angle
        float correctedAngle = Mathf.Atan2(newVelocity.y, Mathf.Sqrt(newVelocity.x * newVelocity.x + newVelocity.z * newVelocity.z)) * Mathf.Rad2Deg;
        Debug.Log("Corrected Launch Angle: " + correctedAngle + " degrees (1st check)");

        // Fire the projectile at 45 degrees forward
        float vx = pumpkinRb.velocity.x;
        float vy = pumpkinRb.velocity.y;
        float vz = pumpkinRb.velocity.z;

        float horizontalSpeed = Mathf.Sqrt(vx * vx + vz * vz); // Magnitude in the XZ plane
        float launchAngle = Mathf.Atan2(vy, horizontalSpeed) * Mathf.Rad2Deg;
        Debug.Log("Corrected Launch Angle: " + launchAngle + " degrees (2nd check)");

        // Ensure the trail still exists before modifying it
        if (trail != null)
        {
            trail.Clear();
            trail.enabled = true;
        }
        
        // Activate the specified object safely
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }

    public bool IsLoaded()
    {
        return pumpkin != null && pumpkin.activeSelf;
    }
}
