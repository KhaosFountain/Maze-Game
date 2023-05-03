using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// camera position: 120.9  17.9 66
public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public EscapeMenuController escapeMenuController;

    float xRotation = 0f;

    void Update()
    {
        if (escapeMenuController != null && escapeMenuController.escapeMenu.activeSelf)
        {
            // Don't move the camera if the Escape Menu is active
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
