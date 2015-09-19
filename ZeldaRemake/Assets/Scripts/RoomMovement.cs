using UnityEngine;
using System.Collections;

public class RoomMovement : MonoBehaviour {
    
    void OnTriggerEnter(Collider other) {
        if (other.tag == "LeftDoor") {
            // Move Camera
            Vector3 newCamPos = Camera.main.transform.position;
            newCamPos.x -= 16f;
            Camera.main.transform.position = newCamPos;

            // Move Link
            Vector3 newLinkPos = transform.position;
            newLinkPos.x -= 5f;
            transform.position = newLinkPos;
        } else if (other.tag == "RightDoor") {
            // Move Camera
            Vector3 newCamPos = Camera.main.transform.position;
            newCamPos.x += 16f;
            Camera.main.transform.position = newCamPos;

            // Move Link
            Vector3 newLinkPos = transform.position;
            newLinkPos.x += 5f;
            transform.position = newLinkPos;
        } else if (other.tag == "UpDoor") {
            // Move Camera
            Vector3 newCamPos = Camera.main.transform.position;
            newCamPos.y += 11f;
            Camera.main.transform.position = newCamPos;

            // Move Link
            Vector3 newLinkPos = transform.position;
            newLinkPos.y += 5f;
            transform.position = newLinkPos;
        } /* else if (other.tag == "DownDoor") {
            // Move Camera
            Vector3 newCamPos = Camera.main.transform.position;
            newCamPos.y -= 11f;
            Camera.main.transform.position = newCamPos;

            // Move Link
            Vector3 newLinkPos = transform.position;
            newLinkPos.y -= 5f;
            transform.position = newLinkPos;
        } */
    }

}
