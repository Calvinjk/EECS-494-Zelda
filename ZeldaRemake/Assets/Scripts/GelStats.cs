using UnityEngine;
using System.Collections;
using System;

public class GelStats : EnemyStats {
  public int maxHealth = 1;
  public int currentHealth = 1;
  public float velocityFactor = 1.0f;
  public float chanceToChangeDirection = 0.02f;
  private float direction;
  public float movementTime = 0.5f;
  private float timePassed = 0f;
  private bool waiting = false;
	public float waitTime = 0.75f;

  // Use this for initialization
  void Start () {
    direction = UnityEngine.Random.value;
    changeDirection();
  }

	// Update is called once per frame
	void Update()
	{
		timePassed += Time.deltaTime;
		if (timePassed >= movementTime && !waiting) {
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			waiting = true;
			timePassed = 0;
			alignWithGrid();
		}
		else if (timePassed >= waitTime && waiting)
		{
			waiting = false;
			timePassed = 0;
			if (UnityEngine.Random.value <= chanceToChangeDirection)
				direction = UnityEngine.Random.value;
      alignWithGrid();
			changeDirection();
		}
	}
	/*
	void FixedUpdate() {
		if (!waiting)
		{
			Vector3 newPos = transform.position;
			float xOffset = newPos.x % 1f;
			float yOffset = newPos.y % 1f;
			if (UnityEngine.Random.value <= chanceToChangeDirection && ((xOffset == 0 && (yOffset > 0.95 || yOffset < 0.05)) || (yOffset == 0 && (xOffset > 0.95 || xOffset < 0.05))))
			{
				direction = UnityEngine.Random.value;
				alignWithGrid();
				changeDirection();
			}
		}
	}
	*/


  void OnTriggerEnter(Collider coll)
  {
    if (coll.gameObject.tag == "Sword" || coll.gameObject.tag == "Arrow" || coll.gameObject.tag == "Bomb")
    {
			takeDamage(1);
    }
		else if (coll.gameObject.tag == "Boomerang") {
			Destroy(coll.gameObject);
			takeDamage(1);
		}
    else if (coll.gameObject.tag == "EnemyWall" || coll.gameObject.tag == "block" || coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Lock" || coll.gameObject.tag == "UpDoor" || coll.gameObject.tag == "RightDoor" || coll.gameObject.tag == "LeftDoor" || coll.gameObject.tag == "DownDoor")
    {
			direction = (direction + 0.25f) % 1;
			changeDirection();
			alignWithGrid();
		}
	}

	void takeDamage(int damage) {
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			RoomManager script = (RoomManager)room.GetComponent(typeof(RoomManager));
			script.killedEnemy(this.gameObject);
		}
	}

  void changeDirection()
  { 
    if (direction < 0.25)
      GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * velocityFactor;
    else if (direction < 0.5)
      GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * velocityFactor;
    else if (direction < 0.75)
      GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * velocityFactor;
    else if (direction <= 1)
      GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * velocityFactor;
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
}
