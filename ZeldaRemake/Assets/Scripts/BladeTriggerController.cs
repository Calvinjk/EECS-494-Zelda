using UnityEngine;
using System.Collections;

public class BladeTriggerController : MonoBehaviour {
	private BladeTrapController bladeTrap;
	public char direction;

	// Use this for initialization
	void Start () {
		bladeTrap = (BladeTrapController) GetComponentInParent(typeof(BladeTrapController));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Link") {
			bladeTrap.triggeredLink(direction);
		}
	}
}
