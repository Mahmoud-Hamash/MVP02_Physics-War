using UnityEngine;

public class HoverMaterialChange : MonoBehaviour
{
    public Material hoverMaterial; // The material to use when hovering
    private Material originalMaterial; // The original material of the object
    public Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    /*void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player's hand
        if (other.CompareTag("PlayerHand"))
        {
            if (objectRenderer != null && hoverMaterial != null)
            {
                objectRenderer.material = hoverMaterial;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the player's hand
        if (other.CompareTag("PlayerHand"))
        {
            if (objectRenderer != null && originalMaterial != null)
            {
                objectRenderer.material = originalMaterial;
            }
        }
    }*/

    public void changeHoverMaterial()
    {
        objectRenderer.material = hoverMaterial;
    }
    
    public void changeOriginalMaterial()
    {
        objectRenderer.material = originalMaterial;
    }    
}
