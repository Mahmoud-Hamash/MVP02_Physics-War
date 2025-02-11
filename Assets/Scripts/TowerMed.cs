using UnityEngine;

public class TowerMed : MonoBehaviour
{
    public int hitsToDestroy = 3; // Number of hits required to destroy the tower
    private int currentHits = 0; // Tracks how many hits the tower has taken

    public GameObject impactEffectPrefab; // Prefab for smoke/fire VFX
    public float destructionDelay = 5f; // Time before the tower is destroyed after collapsing
    public float explosionForce = 10f; // Force applied to planks during destruction

    private bool isDestroyed = false; // Flag to ensure destruction happens only once

    public void RegisterHit(Vector3 impactPoint, Transform impactedPlank)
    {
        if (isDestroyed) return; // If already destroyed, ignore further hits
        Debug.Log("HUBO UN IMPACTO");

        currentHits++; // Increment hit count

        // Spawn impact VFX at the hit location and make it a child of the impacted plank
        SpawnImpactEffect(impactPoint, impactedPlank);

        if (currentHits >= hitsToDestroy)
        {
            Collapse(impactPoint); // Destroy the tower if hit threshold is reached
        }
    }

    private void SpawnImpactEffect(Vector3 impactPoint, Transform impactedPlank)
    {
        if (impactEffectPrefab != null)
        {
            // Instantiate the VFX at the impact point and set its parent to the impacted plank
            GameObject impactEffect = Instantiate(impactEffectPrefab, impactPoint, Quaternion.identity);
            impactEffect.transform.SetParent(impactedPlank); // Make it a child of the hit plank

            // Optionally reset local position and rotation for proper alignment
            impactEffect.transform.localPosition = Vector3.zero; // Adjust as needed
            impactEffect.transform.localRotation = Quaternion.identity;
        }
    }

    private void Collapse(Vector3 explosionPoint)
    {
        isDestroyed = true; // Mark as destroyed to prevent further actions

        // Iterate through all child planks and apply destruction logic
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Enable physics
                rb.useGravity = true;  // Enable gravity

                // Apply explosion force
                Vector3 direction = child.position - explosionPoint;
                direction.Normalize();
                rb.AddForce(direction * explosionForce, ForceMode.Impulse);
            }
        }

        // Schedule destruction of the tower after a delay
        Invoke(nameof(DestroyTower), destructionDelay);
    }

    private void DestroyTower()
    {
        Destroy(gameObject); // Destroy the entire tower (including all children)
    }
}
