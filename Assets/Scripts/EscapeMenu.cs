using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public Gun gunScript; // Reference to the gun script

    private bool isMenuActive = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;

        // Activate or deactivate the escape menu UI here

        if (isMenuActive)
        {
            // Disable the gun script
            gunScript.enabled = false;
        }
        else
        {
            // Re-enable the gun script
            gunScript.enabled = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Player Has Quit");
    }
}
