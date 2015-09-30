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
	public float damageTime = 0.5f;
	private float damageTimePassed = 0;
	private bool damaged = false;
	private bool invincible = false;
	public float attackShift = 2f;
	public GameObject bigHeartPrefab;

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
		if (damaged) {
			damageTimePassed += Time.deltaTime;
			if (damageTimePassed >= damageTime) {
				damageTimePassed = 0;
				damaged = false;
				invincible = false;
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
			}
		}
	}

	void attack() {
		Vector3 direction1 = (link.transform.position - transform.position) + new Vector3(0, attackShift, 0);
		Vector3 direction2 = link.transform.position - transform.position;
		Vector3 direction3 = (link.transform.position - transform.position) + new Vector3(0, -attackShift, 0);
		direction1.Normalize();
		direction2.Normalize();
		direction3.Normalize();
		fire1 = Instantiate(firePrefab, transform.position , Quaternion.identity) as GameObject;
		fire2 = Instantiate(firePrefab, transform.position, Quaternion.identity) as GameObject;
		fire3 = Instantiate(firePrefab, transform.position, Quaternion.identity) as GameObject;
		fire1.GetComponent<Rigidbody>().velocity = direction1 * fireVelocity;
		fire2.GetComponent<Rigidbody>().velocity = direction2 * fireVelocity;
		fire3.GetComponent<Rigidbody>().velocity = direction3 * fireVelocity;
	}

	public void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Sword" || coll.gameObject.tag == "Arrow") {
			takeDamage(1);
		}
		if (coll.gameObject.tag == "Bomb")
		{
			takeDamage(3);
		}
	}

	public void takeDamage(int damage) {
		if (!invincible)
		{
			currentHealth -= damage;
			GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
			damaged = true;
			invincible = true;
			damageTimePassed = 0;
			if (currentHealth <= 0)
			{

				GameObject door = GameObject.Find("BossBlockedDoor");
				door.GetComponent<BoxCollider>().enabled = false;
				door.GetComponent<SpriteRenderer>().sortingOrder = 0;
				Instantiate(bigHeartPrefab, transform.position, Quaternion.identity);
				BossRoom script = (BossRoom)room.GetComponent(typeof(BossRoom));
				script.killedEnemy(this.gameObject);
			}
		}
	}

}
