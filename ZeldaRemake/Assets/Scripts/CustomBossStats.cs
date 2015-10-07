using UnityEngine;
using System.Collections;
using System;

public class CustomBossStats : EnemyStats {
  public int maxHealth = 10;
  public int currentHealth = 10;
  public float velocityFactor = 1.0f;
  private float direction;
	private char dirChar;
	public float damageTime = 0.5f;
	private float damageTimePassed = 0;
	private bool damaged = false;
	private bool invincible = false;
	private Vector3 velocity;
	private GameObject link;
	public float stepDistance = 1;
	private int stepsTaken = 0;
	public int stepsBetweenAttacks = 2;
	private Vector3 currentPos;
	public GameObject hammerPrefab;
	private GameObject hammerInstance;
	private bool attacking = false;
	public float attackTime = 1;
	private float attackTimePassed = 0;
	public GameObject pitPrefab;
	private GameObject pitInstance;


	// Use this for initialization
	void Start () {
		link = GameObject.Find("Link");
		alignWithGrid();
		changeDirection();
	}
	
	// Update is called once per frame
	void Update () {
		if (damaged)
		{
			damageTimePassed += Time.deltaTime;
			if (damageTimePassed >= damageTime)
			{
				damageTimePassed = 0;
				damaged = false;
				invincible = false;
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
			}
		}
		if (Vector3.Distance(currentPos, transform.position) >= stepDistance && !attacking) {
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			alignWithGrid();
			changeDirection();
			stepsTaken = (stepsTaken + 1) % stepsBetweenAttacks;
			if (stepsTaken == 0) {
				attack();
			}

		}
		if (attacking) {
			attackTimePassed += Time.deltaTime;
			if (attackTimePassed >= attackTime) {
				attacking = false;
				attackTimePassed = 0;
				alignWithGrid();
				changeDirection();
				if (hammerInstance != null)
				{
					Destroy(hammerInstance);
				}
			}
		}
	}

	void FixedUpdate() {
		
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "EnemyWall" || coll.gameObject.tag == "block" || coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Lock" || coll.gameObject.tag == "UpDoor" || coll.gameObject.tag == "RightDoor" || coll.gameObject.tag == "LeftDoor" || coll.gameObject.tag == "DownDoor")
		{
			alignWithGrid();
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			changeDirection();
		}
		else if (coll.gameObject.tag == "Bomb") {
			takeDamage(3);
		}
	}

	public void takeDamage(int damage, GameObject coll = null) {
		if (!invincible)
		{
			currentHealth -= damage;
			if (currentHealth <= 0)
			{
				if (hammerInstance != null)
					Destroy(hammerInstance);
				RoomManager script = (RoomManager)room.GetComponent(typeof(RoomManager));
				script.killedEnemy(this.gameObject);
			}
			else
			{
				GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
				damaged = true;
				invincible = true;
				damageTimePassed = 0;
			}
		}
	}

	char findDirection(GameObject coll) {
		char hitDir = coll.GetComponent<WeaponController>().getDirection();
		if (hitDir == 'n')
			return 's';
		else if (hitDir == 'e')
			return 'w';
		else if (hitDir == 's')
			return 'n';
		else if (hitDir == 'w')
			return 'e';
		else return 'x';
	}

	void changeDirection()
	{
		float xDist = link.transform.position.x - transform.position.x;
		float yDist = link.transform.position.y - transform.position.y;

		if (Mathf.Abs(xDist) > Mathf.Abs(yDist)) {
			if (xDist < 0) {
				dirChar = 'w';
				currentPos = transform.position;
				GetComponent<Rigidbody>().velocity = new Vector3(-velocityFactor, 0, 0);
			}
			else {
				dirChar = 'e';
				currentPos = transform.position;
				GetComponent<Rigidbody>().velocity = new Vector3(velocityFactor, 0, 0);
			}
		}
		else {
			if (yDist < 0) {
				dirChar = 's';
				currentPos = transform.position;
				GetComponent<Rigidbody>().velocity = new Vector3(0, -velocityFactor, 0);
			}
			else {
				dirChar = 'n';
				currentPos = transform.position;
				GetComponent<Rigidbody>().velocity = new Vector3(0, velocityFactor, 0);
			}
		}

	}

	void alignWithGrid (){
		Vector3 newPos = transform.position;
        float xOffset = newPos.x % 1f;
		float yOffset = newPos.y % 1f;
		double deciY = newPos.y - Math.Truncate(newPos.y);
		double deciX = newPos.x - Math.Truncate(newPos.x);
		if (deciY <= 0.5)
			{
				newPos.y -= yOffset;
			}
		else
			{
				newPos.y += (1f - yOffset);
			}
		

		if (deciX <= 0.5)
			{
				newPos.x -= xOffset;
			}
		else
		{
			newPos.x += (1f - xOffset);
		}

		transform.position = newPos;
	}

	void attack() {
		if (hammerInstance == null) {
			hammerInstance = Instantiate(hammerPrefab, transform.position, Quaternion.identity) as GameObject;
			pitInstance = Instantiate(pitPrefab, transform.position, Quaternion.identity) as GameObject;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			attacking = true;
			RoomManager script = (RoomManager)room.GetComponent(typeof(RoomManager));


			if (dirChar == 'n')
			{
				hammerInstance.transform.position += new Vector3(0.5f, 1, 0);
				pitInstance.transform.position += new Vector3(1f, 2f, 0);
			}
			else if (dirChar == 'e')
			{
				hammerInstance.transform.position += new Vector3(1, -0.5f, 0);
				hammerInstance.transform.Rotate(new Vector3(0, 0, 1), 270);
				pitInstance.transform.position += new Vector3(2f, -1f, 0);
			}
			else if (dirChar == 'w')
			{
				hammerInstance.transform.position += new Vector3(-1, 0.5f, 0);
				hammerInstance.transform.Rotate(new Vector3(0, 0, 1), 90);
				pitInstance.transform.position += new Vector3(-2f, 1f, 0);
			}
			else if (dirChar == 's')
			{
				hammerInstance.transform.position += new Vector3(-0.5f, -1, 0);
				hammerInstance.transform.Rotate(new Vector3(0, 0, 1), 180);
				pitInstance.transform.position += new Vector3(-1f, -2f, 0);
			}
			hammerInstance.GetComponent<WeaponController>().setDirection(dirChar);
			script.pits.Add(pitInstance);
		}
	}

}