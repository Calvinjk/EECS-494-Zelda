﻿using UnityEngine;
using System.Collections;
using System;

public class StalfosStats : EnemyStats {
  public int maxHealth = 3;
  public int currentHealth = 3;
  public float velocityFactor = 1.0f;
  public float chanceToChangeDirection = 0.02f;
  private float direction;
	private char dirChar;
	public float damageTime = 0.5f;
	private float damageTimePassed = 0;
	private bool damaged = false;
	private float knockbackDist;
	public float maxKnockbackDist = 3;
	public float knockbackFactor = 5f;
	private Vector3 knockbackPos;
	private bool invincible = false;
	public float stunTime = 0.5f;
	private bool stunned = false;
	private float stunTimePassed = 0;
	private Vector3 velocity;
	public GameObject heartPrefab;
	public GameObject rupeePrefab;
	public GameObject bigRupeePrefab;
	public GameObject bombItemPrefab;

	// Use this for initialization
	void Start () {
		knockbackDist = maxKnockbackDist;
    direction = UnityEngine.Random.value;
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
		if (knockbackDist < maxKnockbackDist) {
			knockbackDist = Mathf.Abs(Vector3.Distance(knockbackPos, transform.position));
			if (knockbackDist >= maxKnockbackDist) {
				knockbackDist = maxKnockbackDist;
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				alignWithGrid();
				changeDirection();
			}
		}
		if (stunned) {
			stunTimePassed += Time.deltaTime;
			if (stunTimePassed >= stunTime) {
				stunned = false;
				stunTimePassed = 0;
				alignWithGrid();
				changeDirection();
			}
		}
	}

	void FixedUpdate() {
		if (knockbackDist >= maxKnockbackDist && !stunned)
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

  void OnTriggerEnter(Collider coll)
  {
    if (coll.gameObject.tag == "Sword" || coll.gameObject.tag == "Arrow")
    {
			takeDamage(1, coll.gameObject);
    }

		else if (coll.gameObject.tag == "Bomb")
		{
			takeDamage(2, coll.gameObject);
		}

		else if (coll.gameObject.tag == "Boomerang")
		{
			stunned = true;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
		}

		else if (coll.gameObject.tag == "EnemyWall" || coll.gameObject.tag == "block" || coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Lock" || coll.gameObject.tag == "UpDoor" || coll.gameObject.tag == "RightDoor" || coll.gameObject.tag == "LeftDoor" || coll.gameObject.tag == "DownDoor")
    {
			if (knockbackDist < maxKnockbackDist)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				knockbackDist = maxKnockbackDist;
				alignWithGrid();
				direction = UnityEngine.Random.value;
				changeDirection();
			}
			else
			{
				alignWithGrid();
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				direction = (direction + 0.25f) % 1;
				changeDirection();
			}
    }
  }

	void takeDamage(int damage, GameObject coll = null) {
		if (!invincible)
		{
			currentHealth -= damage;
			if (currentHealth <= 0)
			{
				dropItem();
				RoomManager script = (RoomManager)room.GetComponent(typeof(RoomManager));
				script.killedEnemy(this.gameObject);
			}
			else
			{
				GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
				damaged = true;
				invincible = true;
				damageTimePassed = 0;
				char dir;

				if (coll != null)
				{
					dir = findDirection(coll);
					if (dir == 'n' && (dirChar == 'n' || dirChar == 's'))
					{
						knockbackDist = 0;
						knockbackPos = transform.position;
						GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * knockbackFactor;
					}
					else if (dir == 's' && (dirChar == 'n' || dirChar == 's'))
					{
						knockbackDist = 0;
						knockbackPos = transform.position;
						GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * knockbackFactor;
					}
					else if (dir == 'e' && (dirChar == 'e' || dirChar == 'w'))
					{
						knockbackDist = 0;
						knockbackPos = transform.position;
						GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * knockbackFactor;
					}
					else if (dir == 'w' && (dirChar == 'e' || dirChar == 'w'))
					{
						knockbackDist = 0;
						knockbackPos = transform.position;
						GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * knockbackFactor;
					}
				}

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
		if (direction < 0.25)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * velocityFactor;
			dirChar = 'n';
		}
		else if (direction < 0.5) { 
			GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * velocityFactor;
			dirChar = 'e';
		}
		else if (direction < 0.75) { 
			GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * velocityFactor;
			dirChar = 's';
		}
		else if (direction <= 1) { 
			GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * velocityFactor;
			dirChar = 'w';
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

	void dropItem()
	{
		if (UnityEngine.Random.Range(0, 2) == 0)
		{
			int itemNum = UnityEngine.Random.Range(0, 4);
			if (itemNum == 0)
			{
				Instantiate(heartPrefab, transform.position, Quaternion.identity);
			}
			else if (itemNum == 1)
			{
				Instantiate(rupeePrefab, transform.position, Quaternion.identity);
			}
			else if (itemNum == 2)
			{
				Instantiate(bigRupeePrefab, transform.position, Quaternion.identity);
			}
			else if (itemNum == 3)
			{
				Instantiate(bombItemPrefab, transform.position, Quaternion.identity);
			}
		}
	}
}