using UnityEngine;
using System.Collections;

public class FloorMovement : MonoBehaviour {

    public float    moveSpeed   = 1.0f;
    public bool     roomCleared = false;
    public bool     ______________________;
    public bool     hasMoved    = false;
    public bool     moving      = false;
    public char     moveDir;
    public Vector3  dest;
    public Vector3  curPos;
    public RoomManager roomManager;

	// Use this for initialization
	void Start () {
        curPos = transform.position;
        roomManager = (RoomManager)GameObject.Find("Left4").GetComponent(typeof(RoomManager));
    }
	
	// Update is called once per frame
	void Update () {
	    if (roomManager.cleared) {
            roomCleared = true;
        }
	}

    void OnCollisionEnter(Collision coll) {
        if (!hasMoved && !moving && roomCleared) {
            if (coll.gameObject.tag == "Link") {
                LinkMovement script = (LinkMovement)coll.gameObject.GetComponent(typeof(LinkMovement));
                moveDir = script.currentDir;
                moving = true;
                dest = transform.position;
                switch (moveDir) {
                    case 'n':
                        dest.y = transform.position.y + 1f;
                        break;
                    case 's':
                        dest.y = transform.position.y - 1f;
                        break;
                    case 'e':
                        dest.x = transform.position.x + 1f;
                        break;
                    case 'w':
                        dest.x = transform.position.x - 1f;
                        break;
                }

                GameObject.Find("GelDoor").GetComponent<BoxCollider>().enabled = false;
                GameObject.Find("GelDoor").GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        }
    }

    void FixedUpdate() {
        if (moving && !hasMoved) {
            switch (moveDir) {
                case 'n':
                    if(transform.position.y <= dest.y) {
                        curPos.y += .1f * moveSpeed;
                        transform.position = curPos;
                    } else {
                        transform.position = dest;
                        moving = false;
                        hasMoved = true;
                    }
                    break;
                case 's':
                    if (transform.position.y >= dest.y) {
                        curPos.y -= .1f * moveSpeed;
                        transform.position = curPos;
                    } else {
                        transform.position = dest;
                        moving = false;
                        hasMoved = true;
                    }
                    break;
                case 'e':
                    if (transform.position.x <= dest.x) {
                        curPos.x += .1f * moveSpeed;
                        transform.position = curPos;
                    } else {
                        transform.position = dest;
                        moving = false;
                        hasMoved = true;
                    }
                    break;
                case 'w':
                    if (transform.position.x >= dest.x) {
                        curPos.x -= .1f * moveSpeed;
                        transform.position = curPos;
                    } else {
                        transform.position = dest;
                        moving = false;
                        hasMoved = true;
                    }
                    break;
            }
        }
    }
}
