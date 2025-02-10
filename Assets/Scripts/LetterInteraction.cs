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

    private Renderer _renderer;
    private Renderer _childRenderer;

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
    }

    public void OnSelectEntered()
    {
        HighlightLetter();

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
}