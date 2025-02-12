using UnityEngine;
using System.Collections.Generic;

public class TowerMed : MonoBehaviour
{
    public int hitsToDestroy = 3; // Number of hits required to destroy the tower
    private int currentHits = 0; // Tracks how many hits the tower has taken

    public GameObject impactEffectPrefab; // Prefab for smoke/fire VFX
    public float destructionDelay = 5f; // Time before the tower is destroyed after collapsing
    public float explosionForce = 10f; // Force applied to planks during destruction
    
    [Header("Explosion Effects")]
    public GameObject explosionEffectPrefab; // Particle effect prefab
    public AudioClip explosionSound; // Sound effect for explosion
    public AudioClip impactSound; // Sound effect for explosion
    public float explosionVolume = 1f; // Volume of the sound

    private AudioSource audioSource;

    private bool isDestroyed = false; // Flag to ensure destruction happens only once

    private HashSet<int> processedProjectiles = new HashSet<int>(); // Tracks processed projectile instance IDs

    void Start()
    {
        // Add an AudioSource component dynamically if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // Make the sound 3D
    }

    public void RegisterHit(Vector3 impactPoint, Transform impactedPlank, int projectileID)
    {
        if (isDestroyed) return; // If already destroyed, ignore further hits

        // Check if this projectile has already been processed
        if (processedProjectiles.Contains(projectileID)) return;

        // Add this projectile ID to the set of processed projectiles
        processedProjectiles.Add(projectileID);

        Debug.Log($"Impact registered! Current hits: {currentHits + 1}/{hitsToDestroy}");
        Debug.Log(impactSound);
        Debug.Log(audioSource);
        audioSource.PlayOneShot(impactSound, explosionVolume); 

        currentHits++; // Increment hit count

        // Spawn impact VFX at the precise hit location relative to the plank
        SpawnImpactEffect(impactPoint, impactedPlank);

        if (currentHits >= hitsToDestroy)
        {
            // Play SFX and VFX at the explosion point
            PlayExplosionEffects(impactPoint); 
            Collapse(impactPoint); // Destroy the tower if hit threshold is reached
        }
    }

    private void SpawnImpactEffect(Vector3 impactPoint, Transform impactedPlank)
    {
        if (impactEffectPrefab != null)
        {
            // Instantiate the VFX at the world-space impact point
            GameObject impactEffect = Instantiate(impactEffectPrefab, impactPoint, Quaternion.identity);

            // Parent it to the impacted plank
            impactEffect.transform.SetParent(impactedPlank);

            // Convert world-space position to local space relative to the plank
            impactEffect.transform.localPosition = impactedPlank.InverseTransformPoint(impactPoint);

            Debug.Log($"Spawned VFX at local position {impactEffect.transform.localPosition} on {impactedPlank.name}");
        }
    }

    private void Collapse(Vector3 explosionPoint)
    {
        isDestroyed = true; // Mark as destroyed to prevent further actions

        Debug.Log("Tower is collapsing!");

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
    
    private void PlayExplosionEffects(Vector3 explosionPoint)
    {
        // Instantiate particle effect
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, explosionPoint, Quaternion.identity); // remember to configure the object to destroy after the effect
        }

        // Play sound effect
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound, explosionVolume);
        }
    }

}
