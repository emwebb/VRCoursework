using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {
    public float sensitivityX = 30F;
    public float sensitivityY = 30F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public KeyCode escapeKey = KeyCode.Escape;

    bool lockMode = true;

    public void Look(float mouseX, float mouseY) {
        float rotationX = mouseX * sensitivityX * Time.deltaTime;
        float rotationY = mouseY * sensitivityY * Time.deltaTime;

        transform.parent.Rotate(transform.parent.up * rotationX);
        transform.Rotate(new Vector3(-rotationY, 0, 0));
        float lookheight = transform.localEulerAngles.x;

        if (lookheight > 0 && lookheight < 90 - 15) {}
        else if (lookheight > 270 + 15 && lookheight < 360) {}
        else {
            transform.Rotate(new Vector3(rotationY, 0, 0));
        }
    }

    public void Pause() {
        this.lockMode = false;
    }
    public void Unpause() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        
    }
}
