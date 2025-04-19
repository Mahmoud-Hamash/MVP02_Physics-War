using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private GameObject handle;
    [SerializeField] private TrebuchetControllerUpdated trebuchet;
    [SerializeField] private GameObject trebuchetPrefab;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private AudioSource leverSound;

    private bool _isOff = true;
    private bool _isMoving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        trebuchet = FindFirstObjectByType<TrebuchetControllerUpdated>();
    }

    public void OnSelected()
    {
        if (!_isMoving)
        {
            if (_isOff && trebuchet.IsLoaded())  
            {
                _isOff = false;  // Toggle before rotation starts
                leverSound.Play();
                StartCoroutine(RotateLever(45f));
                trebuchet.FireTrebuchet();
            }
            else if (_isOff)
            {
                Debug.Log("Fire attempt failed. Load the trebuchet with a projectile first.");
            }
            else 
            {
                _isOff = true;  // Toggle before rotation starts
                leverSound.Play();
                StartCoroutine(RotateLever(-45f));
                ResetTrebuchet();
            }
        }
    }

    private void ResetTrebuchet()
    {
        var trebuchetTransform = trebuchet.transform;
        
        // Destroy all children first
        foreach (Transform child in trebuchetTransform)
        {
            Destroy(child.gameObject);
        }
        
        Destroy(trebuchet.gameObject);
        
        // Instantiate a new trebuchet at the same position & rotation
        trebuchet = Instantiate(trebuchetPrefab, trebuchetTransform.position, 
            trebuchetTransform.rotation).GetComponent<TrebuchetControllerUpdated>();
    }

    private IEnumerator RotateLever(float rotationDelta)
    {
        _isMoving = true;
        float elapsed = 0f;

        Quaternion startRotation = handle.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotationDelta);

        while (elapsed < rotationSpeed)
        {
            elapsed += Time.deltaTime;
            handle.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / rotationSpeed);
            yield return null; // Wait for the next frame
        }

        handle.transform.rotation = endRotation; // Ensure it ends at the exact position
        _isMoving = false;
    }
}
