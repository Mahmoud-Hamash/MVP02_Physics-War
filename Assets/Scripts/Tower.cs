using UnityEngine;

public class Tower : MonoBehaviour
{
    public bool isImpacted = false; // Flag to prevent multiple iterations
    public float destructionDelay = 5f; // Time before the tower is destroyed
    public float explosionForce = 10f; // Force applied during collapse

    [Header("Explosion Effects")]
    public GameObject explosionEffectPrefab; // Particle effect prefab
    public AudioClip explosionSound; // Sound effect for explosion
    public float explosionVolume = 1f; // Volume of the sound

    private AudioSource audioSource;

    private void Start()
    {
        // Add an AudioSource component dynamically if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // Make the sound 3D
    }

    public void Collapse(Vector3 explosionPoint)
    {
        if (isImpacted) return; // Skip if already impacted

        isImpacted = true; // Mark as impacted

        // Play SFX and VFX at the explosion point
        //PlayExplosionEffects(explosionPoint); --ROMITA enable when you have the assets

        // Iterate through all child objects
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

    private void PlayExplosionEffects(Vector3 explosionPoint)
    {
        // Instantiate particle effect
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, explosionPoint, Quaternion.identity);
        }

        // Play sound effect
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound, explosionVolume);
        }
    }

    private void DestroyTower()
    {
        Destroy(gameObject); // Destroy the entire tower (including all children)
    }
}
