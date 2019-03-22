using UnityEngine;
using System.Collections;

/// Controls the camera look functionality.
public class MouseLook : MonoBehaviour {
    /// The horizontal sensitivity.
    public float sensitivityX = 60;

    /// The vertical sensitivity.
    public float sensitivityY = 30;

    /// Called to adjust the look direction of the camera.
    public void Look(float mouseX, float mouseY) {

        // Compute the change in rotation required.
        float rotationX = mouseX * sensitivityX * Time.deltaTime;
        float rotationY = mouseY * sensitivityY * Time.deltaTime;

        // apply the rotation.
        transform.parent.Rotate(transform.parent.up * rotationX);
        transform.Rotate(new Vector3(-rotationY, 0, 0));
        float lookheight = transform.localEulerAngles.x;

        // Undo the vertical rotation if attempting to look too high or low.
        if (lookheight > 0 && lookheight < 90 - 15) {}
        else if (lookheight > 270 + 15 && lookheight < 360) {}
        else {
            transform.Rotate(new Vector3(rotationY, 0, 0));
        }
    }

    /// Lock the cursor to the screen and hide it on pause.
    public void Pause() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// Free the mouse cursor and unhide it on unpause.
    public void Unpause() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// Reload the angle of the camera.
    public void Reload() {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
