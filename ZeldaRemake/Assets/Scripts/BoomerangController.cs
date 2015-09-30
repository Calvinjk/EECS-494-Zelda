using UnityEngine;
using System.Collections;

public class BoomerangController : WeaponController {
	public float speed = 1.0f;
	public float rotationSpeed = 1.0f;
	public float maxDistance = 5.0f;
	private GameObject thrower;
	private bool returning = false;
	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, rotationSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(startPos, transform.position) >= maxDistance && !returning) {
			returning = true;
		}
    else if (returning)
		{
			Vector3 dir = thrower.transform.position - transform.position;
			dir.Normalize();
			GetComponent<Rigidbody>().velocity = dir * speed;
		}
	}

	public void init(char direction, GameObject thwr) {
		thrower = thwr;
		startPos = thrower.transform.position;
		switch (direction)
		{
			case 'n':
				transform.position += new Vector3(0, 1, 0);
				GetComponent<Rigidbody>().velocity = new Vector3(0, 1 * speed, 0);
				setDirection('s');
				break;
			case 'e':
				transform.position += new Vector3(1, 0, 0);
				GetComponent<Rigidbody>().velocity = new Vector3(1 * speed, 0, 0);
				setDirection('w');
				break;
			case 'w':
				transform.position += new Vector3(-1, 0, 0);
				GetComponent<Rigidbody>().velocity = new Vector3(-1 * speed, 0, 0);
				setDirection('e');
				break;
			case 's':
				transform.position += new Vector3(0, -1, 0);
				GetComponent<Rigidbody>().velocity = new Vector3(0, -1 * speed, 0);
				setDirection('n');
				break;
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (!returning && coll.gameObject.tag != "Room" && coll.gameObject.tag != "block" && coll.gameObject.tag != "BombItem" && coll.gameObject.tag != "Rupee" && coll.gameObject.tag != "Heart" && coll.gameObject.tag != "BigRupee") {
			returning = true;
		}
  }
}
