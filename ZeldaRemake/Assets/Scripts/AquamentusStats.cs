using UnityEngine;
using System.Collections;

public class AquamentusStats : EnemyStats {
	public int maxHealth = 6;
	public int currentHealth = 6;
	private float timePassed = 0f;
	public float timeBetweenAttacks = 5f;
	private GameObject link;
	private GameObject fire1, fire2, fire3;
	public GameObject firePrefab;
	public float fireVelocity = 3f;

	// Use this for initialization
	void Start () {
		link = GameObject.Find("Link");
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed >= timeBetweenAttacks)
		{
			attack();
			timePassed = 0;
		}
	}

	void attack() {
		Vector3 direction = link.transform.position - transform.position;
		direction.Normalize();
		fire1 = Instantiate(firePrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
		fire2 = Instantiate(firePrefab, transform.position, Quaternion.identity) as GameObject;
		fire3 = Instantiate(firePrefab, transform.position + new Vector3(0, -1, 0), Quaternion.identity) as GameObject;
		fire1.GetComponent<Rigidbody>().velocity = direction * fireVelocity;
		fire2.GetComponent<Rigidbody>().velocity = direction * fireVelocity;
		fire3.GetComponent<Rigidbody>().velocity = direction * fireVelocity;

	}

	public void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Sword") {
			currentHealth--;
			if (currentHealth == 0) {
				Destroy(this.gameObject);
			}
		}
	}

}
