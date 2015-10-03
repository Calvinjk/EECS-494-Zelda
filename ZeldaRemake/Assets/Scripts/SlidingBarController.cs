using UnityEngine;
using System.Collections;

public class SlidingBarController : MonoBehaviour {

    public bool triggered = true;
    public bool ____________;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Link") {
            triggered = true;
        }
    }

    void OnTriggerExit(Collider coll) {
        if (coll.tag == "Link") {
            triggered = false;
        }
    }
}
