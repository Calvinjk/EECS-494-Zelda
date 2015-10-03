using UnityEngine;
using System.Collections;

public class SlidingBarTrap : MonoBehaviour {

    public float    speed           = 0.25f;
    public float    maxDist         = 1.5f;
    public char     dir;
    public bool     ____________;
    public Vector3  startPos;
    public Vector3  endPos;

    private SlidingBarController pressurePlate;    

	// Use this for initialization
	void Start () {
        pressurePlate = (SlidingBarController)GetComponentInParent(typeof(SlidingBarController));
        startPos = transform.position;
        endPos = transform.position;

        //Initialize endPos
        switch (dir) {
            case 'n':
                endPos.y = startPos.y + maxDist;
                break;
            case 's':
                endPos.y = startPos.y - maxDist;
                break;
            case 'e':
                endPos.x = startPos.x + maxDist;
                break;
            case 'w':
                endPos.x = startPos.x - maxDist;
                break;
            default:
                print("Error in sliding bar trap move direction");
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    public void Move() {
        //Make sure bars are not in one of two resting positions
        if ((pressurePlate.triggered && transform.position != startPos) || (!pressurePlate.triggered && transform.position != endPos)) { 
            switch (dir) {
                case 'n':
                    if (!pressurePlate.triggered) {
                        if (transform.position.y < endPos.y) {
                            transform.position = new Vector3(transform.position.x, transform.position.y + (0.1f * speed), 0);
                        } else {
                            transform.position = endPos;
                        }
                    } else {
                        if (transform.position.y > startPos.y) {
                            transform.position = new Vector3(transform.position.x, transform.position.y - (0.1f * speed), 0);
                        } else {
                            transform.position = startPos;
                        }
                    }
                    break;
                case 's':
                    if (!pressurePlate.triggered) {
                        if (transform.position.y > endPos.y) {
                            transform.position = new Vector3(transform.position.x, transform.position.y - (0.1f * speed), 0);
                        } else {
                            transform.position = endPos;
                        }
                    } else {
                        if (transform.position.y < startPos.y) {
                            transform.position = new Vector3(transform.position.x, transform.position.y + (0.1f * speed), 0);
                        } else {
                            transform.position = startPos;
                        }
                    }
                    break;
                case 'e':
                    //NOT IMPLEMENTED
                    break;
                case 'w':
                    //NOT IMPLEMENTED
                    break;
                default:
                    print("Error in sliding bar trap move direction");
                    break;
            }
        }
    }
}
