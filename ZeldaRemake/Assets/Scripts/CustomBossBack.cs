using UnityEngine;
using System.Collections;

public class CustomBossBack : MonoBehaviour {
	CustomBossStats stats;

	// Use this for initialization
	void Start () {
		stats = (CustomBossStats) transform.parent.GetComponent(typeof(CustomBossStats));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Sword" || coll.gameObject.tag == "Bow") {
			stats.takeDamage(1);
		}
	}
}
