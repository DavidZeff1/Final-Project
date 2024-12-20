using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject instructionsPanel; 

    public void PlayGame()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.ResetInventory();
        }

        SceneManager.LoadScene("Level 1 Scene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true); 
        }
        else
        {
            Debug.LogError("Instructions panel reference is missing in the Inspector!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && instructionsPanel.activeSelf)
        {
            instructionsPanel.SetActive(false);  
        }
    }
}

