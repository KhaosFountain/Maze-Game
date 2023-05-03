using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuController : MonoBehaviour
{
    public GameObject escapeMenu;
    public GameObject gun;

    private Gun gunScript;

    void Start()
    {
        gunScript = gun.GetComponent<Gun>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            ToggleEscapeMenu();
        }
    }

    public void ToggleEscapeMenu()
    {
        bool menuIsActive = escapeMenu.activeSelf;
        escapeMenu.SetActive(!menuIsActive);

        if (menuIsActive)
        {
            // Hide the cursor and lock it in place when the Escape Menu is closed
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Re-enable the gun script
            gunScript.enabled = true;
        }
        else
        {
            // Show the cursor and unlock it when the Escape Menu is opened
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Disable the gun script
            gunScript.enabled = false;
        }
    }

}
