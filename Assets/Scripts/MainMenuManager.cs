using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuWarButton;
    public GameObject mainMenuTrainingButton;
    public GameObject selectionWeaponUIs;
    public GameObject errorMessageUI;
    public AudioClip touchSound; 

    public ToggleButtonsSelection groupToogle;
    private AudioSource audioSource;
    private float SFXVolume = 0.7f;

    private void Start()
    {
        // Add an AudioSource component dynamically if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // Make the sound 3D
    }
    
    // Method to load a new scene
    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadWarScene()
    {
        // Touch sound
        audioSource.PlayOneShot(touchSound, SFXVolume);
        LoadNewScene("GameScene");
    }

    public void DisplayWeapons()
    {
        Debug.Log("Weapons displayed");
        // Touch sound
        audioSource.PlayOneShot(touchSound, SFXVolume);
        
        mainMenuWarButton.SetActive(false);
        mainMenuTrainingButton.SetActive(false);
        selectionWeaponUIs.SetActive(true);
        
    }
    
    public void BackToMainMenu()
    {
        Debug.Log("Display buttons Main");
        // Touch sound
        audioSource.PlayOneShot(touchSound, SFXVolume);
        
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
            // Touch sound
            audioSource.PlayOneShot(touchSound, SFXVolume);
            
            switch (groupToogle.selectedToogleButton) // Use enum for comparison
            {
                case WeaponType.TREBUCHET:
                    LoadNewScene("MainScene");
                    break;
                case WeaponType.SLINGSHOT:
                    LoadNewScene("MainScene");
                    break;
            }
        }
    }

    private void HideErrorMessage()
    {
        errorMessageUI.SetActive(false);
    }
    
    
}
