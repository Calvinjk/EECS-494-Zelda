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
        } else if (other.tag == "RightDoor") {
            // Move Camera
            newCamPos.x = oldCamPos.x + 16.0f;
            moveCamRight = true;
        } else if (other.tag == "UpDoor") {
            // Move Camera
            newCamPos.y = oldCamPos.y + 11.0f;
            moveCamUp = true;
        } else if (other.tag == "DownDoor") {
            // Move Camera
            newCamPos.y = oldCamPos.y - 11.0f;
            moveCamDown = true;
        } 
    }

    void FixedUpdate() {
        if (moveCamLeft) {
            if (oldCamPos.x >= newCamPos.x) {
                oldCamPos.x -= 1.0f * cameraSpeed;
                Camera.main.transform.position = oldCamPos;
                // Stop Link Movement
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            } else {
                Camera.main.transform.position = newCamPos;
                oldCamPos = newCamPos;
                moveCamLeft = false;
                // Move Link
                Vector3 newLinkPos = transform.position;
                newLinkPos.x -= 2f;
                transform.position = newLinkPos;
				LinkStats stats = (LinkStats)GetComponent(typeof(LinkStats));
				stats.returnPos = transform.position;
			}
        } else if (moveCamRight){
            if (oldCamPos.x <= newCamPos.x) {
                oldCamPos.x += 1.0f * cameraSpeed;
                Camera.main.transform.position = oldCamPos;
                // Stop Link Movement
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            } else {
                Camera.main.transform.position = newCamPos;
                oldCamPos = newCamPos;
                moveCamRight = false;
                // Move Link
                Vector3 newLinkPos = transform.position;
                newLinkPos.x += 2f;
                transform.position = newLinkPos;
				LinkStats stats = (LinkStats)GetComponent(typeof(LinkStats));
				stats.returnPos = transform.position;
			}
        } else if (moveCamUp) {
            if (oldCamPos.y <= newCamPos.y) {
                oldCamPos.y += 1.0f * cameraSpeed;
                Camera.main.transform.position = oldCamPos;
                // Stop Link Movement
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            }
            else {
                Camera.main.transform.position = newCamPos;
                oldCamPos = newCamPos;
                moveCamUp = false;
                // Move Link
                Vector3 newLinkPos = transform.position;
                newLinkPos.y += 2f;
                transform.position = newLinkPos;
				LinkStats stats = (LinkStats)GetComponent(typeof(LinkStats));
				stats.returnPos = transform.position;
			}
        }
        else if (moveCamDown) {
            if (oldCamPos.y >= newCamPos.y) {
                oldCamPos.y -= 1.0f * cameraSpeed;
                Camera.main.transform.position = oldCamPos;
                // Stop Link Movement
                GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            }
            else{
                Camera.main.transform.position = newCamPos;
                oldCamPos = newCamPos;
                moveCamDown = false;
                // Move Link
                Vector3 newLinkPos = transform.position;
                newLinkPos.y -= 2f;
                transform.position = newLinkPos;
								LinkStats stats = (LinkStats)GetComponent(typeof(LinkStats));
								stats.returnPos = transform.position;
						}
        }

    }
}
