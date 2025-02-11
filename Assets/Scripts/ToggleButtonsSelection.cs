using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class ToggleButtonsSelection : MonoBehaviour
{
    public Material defaultMaterial; // Default material for this letter
    public Material selectedMaterial; // Material when selected
    private Renderer _renderer;
    public string selectedToogleButton = "";
    private float speedSelected = 0f;
    private float speedDeselected = 35f;
    

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        
    }

    public void OnSelectEntered(GameObject obj)
    {
        SingleToogleButton buttonSelected = obj.GetComponent<SingleToogleButton>();
        selectedToogleButton = buttonSelected.optionName;
        HighlightButton(buttonSelected);
    }

    public void ResetAllToogles()
    {
        foreach (var toogleButton in FindObjectsOfType<SingleToogleButton>())
        {

                SetDefaultMaterial(toogleButton._renderer);
                toogleButton.ChangeSpeed3DModel(speedDeselected);
        }
    }

    private void SetDefaultMaterial(Renderer _renderer)
    {
        _renderer.material = defaultMaterial;
    }
    
    public void HighlightButton(SingleToogleButton buttonSelected)
    {
        // Reset all other letters first
        ResetAllToogles();

        // Change material of this letter
        buttonSelected._renderer.material = selectedMaterial;
        buttonSelected.ChangeSpeed3DModel(speedSelected);

    }
}