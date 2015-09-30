using UnityEngine;
using System.Collections;
using System;

public class KeeseStatsBlocked : MonoBehaviour {
  public int maxHealth = 1;
  public int currentHealth = 1;
  public float velocityFactor = 5.0f;
	private float curVel = 0f;
	private Vector3 nextBlock = Vector3.zero;
	private float distToNext = 0f;
	public float acceleration = 0.1f;
	public float deceleration = 0.2f;
	private Vector3 direction;
	public float decelDist = 1.5f;
	private float timePassed = 0f;
	public float waitTime = 2f;
	private bool waiting = false;
	public GameObject keyPrefab;
	public GameObject rm;

  // Use this for initialization
  void Start () {
		alignWithGrid();
  }
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed >= waitTime && waiting) {
			timePassed = 0;
			waiting = false;
		}
		if (!waiting && distToNext <= 0)
		{
			chooseDest();
			distToNext = Mathf.Abs(Vector3.Distance(transform.position, nextBlock));
			direction = (nextBlock - transform.position).normalized;
			GetComponent<Rigidbody>().velocity = direction * curVel;
		}
	}

  void FixedUpdate()
  {
		if (distToNext >= decelDist) {
			if (curVel < velocityFactor) {
				curVel += acceleration;
				GetComponent<Rigidbody>().velocity = direction * curVel;
			}
			distToNext = Mathf.Abs(Vector3.Distance(transform.position, nextBlock));
		}
		else if (distToNext <= decelDist && distToNext > 0.1)
		{
			if (curVel - deceleration > 1)
				curVel -= deceleration;
			GetComponent<Rigidbody>().velocity = direction * curVel;
			distToNext = Mathf.Abs(Vector3.Distance(transform.position, nextBlock));
		}

		else if (distToNext <= 0.1 && !waiting) {
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			curVel = 0;
			distToNext = 0;
			alignWithGrid();
			nextBlock = Vector3.zero;
			waiting = true;
			timePassed = 0;
		}
		else
		{
			curVel = 1f;
		}
	}

	void OnTriggerEnter(Collider coll)
  {
    if (coll.gameObject.tag == "Sword" || coll.gameObject.tag == "Arrow" || coll.gameObject.tag == "Bomb")
    {
			takeDamage(1);
    }
		else if (coll.gameObject.tag == "Boomerang") {
			takeDamage(1);

		}
    else if (coll.gameObject.tag == "EnemyWall" || coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Lock" || coll.gameObject.tag == "UpDoor" || coll.gameObject.tag == "RightDoor" || coll.gameObject.tag == "LeftDoor" || coll.gameObject.tag == "DownDoor")
    {
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			alignWithGrid();
			nextBlock = Vector3.zero;
			timePassed = 0;
			waiting = true;
    }
  }

	void takeDamage(int damage) {
		currentHealth--;
		if (currentHealth == 0)
		{
			BlockedRoomManager script = (BlockedRoomManager)rm.GetComponent(typeof(BlockedRoomManager));
			script.killedEnemy(this.gameObject);
		}
	}

	void alignWithGrid()
	{
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

	void chooseDest() {
		Vector3 roomPos;
		roomPos.x = Mathf.Floor(rm.transform.position.x);
		roomPos.y = Mathf.Floor(rm.transform.position.y);
		int minX = -5;
		int maxX = 6;
		int minY = -3;
		int maxY = 3;
		nextBlock.x = roomPos.x + (float) UnityEngine.Random.Range(minX, maxX);
		nextBlock.y = roomPos.y + (float)UnityEngine.Random.Range(minY, maxY);
  }
}
