using UnityEngine;
using System.Collections;

public class RoomMovement : MonoBehaviour {

    public float    cameraSpeed = 1.0f;
    public bool _________________________;
    public bool     moveCamUp       = false;
    public bool     moveCamDown     = false;
    public bool     moveCamLeft     = false;
    public bool     moveCamRight    = false;
    public Vector3  oldCamPos;
    public Vector3  newCamPos;

    void Awake() {
        oldCamPos = Camera.main.transform.position;
        newCamPos = oldCamPos;
    }

    
    void OnTriggerEnter(Collider other) {
        if (other.tag == "LeftDoor") {
            // Move Camera
            newCamPos.x = oldCamPos.x - 16.0f;
            moveCamLeft = true;

            // Move Link
            Vector3 newLinkPos = transform.position;
            newLinkPos.x -= 2f;
            transform.position = newLinkPos;
        } else if (other.tag == "RightDoor") {
            // Move Camera
            newCamPos.x = oldCamPos.x + 16.0f;
            moveCamRight = true;

            // Move Link
            Vector3 newLinkPos = transform.position;
            newLinkPos.x += 2f;
            transform.position = newLinkPos;
        } else if (other.tag == "UpDoor") {
            // Move Camera
            newCamPos.y = oldCamPos.y + 11.0f;
            moveCamUp = true;

            // Move Link
            Vector3 newLinkPos = transform.position;
            newLinkPos.y += 1.5f;
            transform.position = newLinkPos;
        } else if (other.tag == "DownDoor") {
            // Move Camera
            newCamPos.y = oldCamPos.y - 11.0f;
            moveCamDown = true;

            // Move Link
            Vector3 newLinkPos = transform.position;
            newLinkPos.y -= 1.5f;
            transform.position = newLinkPos;
        } 
    }

    void FixedUpdate() {
        if (moveCamLeft) {
            if (oldCamPos.x >= newCamPos.x) {
                oldCamPos.x -= 1.0f * cameraSpeed;
                Camera.main.transform.position = oldCamPos;
            } else {
                Camera.main.transform.position = newCamPos;
                oldCamPos = newCamPos;
                moveCamLeft = false;
            }
        } else if (moveCamRight){
            if (oldCamPos.x <= newCamPos.x) {
                oldCamPos.x += 1.0f * cameraSpeed;
                Camera.main.transform.position = oldCamPos;
            } else {
                Camera.main.transform.position = newCamPos;
                oldCamPos = newCamPos;
                moveCamRight = false;
            }
        } else if (moveCamUp) {
            if (oldCamPos.y <= newCamPos.y) {
                oldCamPos.y += 1.0f * cameraSpeed;
                Camera.main.transform.position = oldCamPos;
            }
            else {
                Camera.main.transform.position = newCamPos;
                oldCamPos = newCamPos;
                moveCamUp = false;
            }
        }
        else if (moveCamDown) {
            if (oldCamPos.y >= newCamPos.y) {
                oldCamPos.y -= 1.0f * cameraSpeed;
                Camera.main.transform.position = oldCamPos;
            }
            else{
                Camera.main.transform.position = newCamPos;
                oldCamPos = newCamPos;
                moveCamDown = false;
            }
        }
    }

}
