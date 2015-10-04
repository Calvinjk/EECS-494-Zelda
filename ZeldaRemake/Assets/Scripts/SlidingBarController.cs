using UnityEngine;
using System.Collections;

public class SlidingBarController : MonoBehaviour {

    public bool ____________;
    public bool triggered = true;
    public bool activated = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Link") {
            triggered = true;
            activated = true;
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.tag == "Link") {
            triggered = false;
        }
    }

    void OnTriggerStay(Collider coll) {
        if (coll.tag == "Shoes") {
            triggered = true;
        }
    }
}
