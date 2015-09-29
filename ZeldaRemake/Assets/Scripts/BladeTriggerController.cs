using UnityEngine;
using System.Collections;

public class BladeTriggerController : MonoBehaviour {
	private BladeTrapController bladeTrap;
	public char direction;
	private bool found = false;

	// Use this for initialization
	void Start () {
		bladeTrap = (BladeTrapController) GetComponentInParent(typeof(BladeTrapController));
	}
	
	// Update is called once per frame
	void Update () {
		if (found)
			bladeTrap.triggeredLink(direction);
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Link") {
			found = true;
		}
	}

	void OnTriggerExit(Collider coll) {
		if (coll.gameObject.tag == "Link")
			found = false;
	}
}
