using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class LetterInteraction : MonoBehaviour
{
    public Material defaultMaterial; // Default material for this letter
    public Material selectedMaterial; // Material when selected
    public GameObject uiCanvas; // The parent GameObject of the UI Canvas
    public TextMeshProUGUI uiText; // TextMeshPro component for displaying content
    public string letterContent; // Content specific to this letter
    public GameObject childLetter;
    public GameObject touchEffectPrefab; // Particle effect prefab
    public AudioClip touchSound; 

    private Renderer _renderer;
    private Renderer _childRenderer;
    private AudioSource audioSource;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (childLetter != null)
        {
            _childRenderer = childLetter.GetComponent<Renderer>();
        }
        else
        {
            _childRenderer = _renderer;
        }
        SetDefaultMaterial();

        // Ensure the UI Canvas is hidden at the start
        if (uiCanvas != null)
        {
            uiCanvas.SetActive(false);
        }
        // Add an AudioSource component dynamically if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // Make the sound 3D
    }

    public void OnSelectEntered()
    {
        HighlightLetter();
        PlayTouchEffects();

        // Display the UI Canvas and update its content
        if (uiCanvas != null)
        {
            uiCanvas.SetActive(true); // Show the canvas
            if (uiText != null)
            {
                uiText.text = letterContent; // Update the text content
            }
        }
    }

    private void ResetAllLetters()
    {
        foreach (var letter in FindObjectsOfType<LetterInteraction>())
        {
            if (letter != this) // Skip resetting the currently selected letter
            {
                letter.SetDefaultMaterial();
                if (letter.uiCanvas != null)
                {
                    letter.uiCanvas.SetActive(false); // Hide other letters' UI Canvas
                }
            }
        }
    }

    public void SetDefaultMaterial()
    {
        _renderer.material = defaultMaterial;
        _childRenderer.material = defaultMaterial;
    }
    
    public void HighlightLetter()
    {
        // Reset all other letters first
        ResetAllLetters();

        // Change material of this letter
        _renderer.material = selectedMaterial;
        _childRenderer.material = selectedMaterial;
        
    }
    
    private void PlayTouchEffects()
    {
        // Instantiate particle effect
        if (touchEffectPrefab != null)
        {
            Instantiate(touchEffectPrefab, transform.position, Quaternion.identity); // remember to configure the object to destroy after the effect
        }

        // Play sound effect
        if (touchSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(touchSound);
        }
    }
}