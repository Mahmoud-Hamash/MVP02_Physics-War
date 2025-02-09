using UnityEngine;

public class PotionInteraction : MonoBehaviour
{
    public GameObject formulaCanvas; // Assign the formula UI Canvas here
    private bool isFormulaVisible = false;

    void Start()
    {
        // Ensure the formula is hidden at start
        if (formulaCanvas != null)
        {
            formulaCanvas.SetActive(false);
        }
    }

    public void OnSelectEntered()
    {
        // Toggle formula visibility when touched or grabbed
        isFormulaVisible = !isFormulaVisible;
        if (formulaCanvas != null)
        {
            formulaCanvas.SetActive(isFormulaVisible);
        }
    }
}