using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuWarButton;
    public GameObject mainMenuTrainingButton;
    public GameObject selectionWeaponUIs;
    public GameObject errorMessageUI;

    public ToggleButtonsSelection groupToogle;
    // Method to load a new scene
    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadWarScene()
    {
        LoadNewScene("Rommy Props");
    }

    public void DisplayWeapons()
    {
        Debug.Log("Weapons displayed");
        mainMenuWarButton.SetActive(false);
        mainMenuTrainingButton.SetActive(false);
        selectionWeaponUIs.SetActive(true);
        
    }
    
    public void BackToMainMenu()
    {
        Debug.Log("Display buttons Main");
        selectionWeaponUIs.SetActive(false);
        mainMenuWarButton.SetActive(true);
        mainMenuTrainingButton.SetActive(true);
        groupToogle.ResetAllToogles();
        
    }
    

    public void LoadWeaponScene()
    {
        if (groupToogle.selectedToogleButton == null) // Check if no button is selected
        {
            Debug.Log("NO OPTION SELECTED");
            errorMessageUI.SetActive(true);
            Invoke("HideErrorMessage", 3f);
        }
        else
        {
            switch (groupToogle.selectedToogleButton) // Use enum for comparison
            {
                case WeaponType.TREBUCHET:
                    LoadNewScene("RommyScene");
                    break;
                case WeaponType.SLINGSHOT:
                    LoadNewScene("Rommy Props");
                    break;
            }
        }
    }

    private void HideErrorMessage()
    {
        errorMessageUI.SetActive(false);
    }
    
    
}
