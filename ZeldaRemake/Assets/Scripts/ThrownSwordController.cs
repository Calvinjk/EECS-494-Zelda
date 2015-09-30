using UnityEngine;
using System.Collections;

public class ThrownSwordController : WeaponController {
	private float maxTime = 4f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		maxTime -= Time.deltaTime;
		if (maxTime <= 0)
			Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "RightDoor" || coll.gameObject.tag == "LeftDoor" || coll.gameObject.tag == "DownDoor" || coll.gameObject.tag == "UpDoor" || coll.gameObject.tag == "Stalfos" || coll.gameObject.tag == "Gel" || coll.gameObject.tag == "Keese" || coll.gameObject.tag == "Goriya" || coll.gameObject.tag == "WallMaster" || coll.gameObject.tag == "Aquamentus" || coll.gameObject.tag == "BladeTrap" || coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Lock")
		{
			Destroy(this.gameObject);
		}
	}
}
