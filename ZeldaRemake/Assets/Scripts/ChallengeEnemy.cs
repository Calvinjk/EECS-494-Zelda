﻿using UnityEngine;
using System.Collections;
using System;

public class ChallengeEnemy : MonoBehaviour {
  public int maxHealth = 100;
  public int currentHealth = 100;
  public float velocityFactor = 1.0f;
	public float damageTime = 0.5f;
	private float damageTimePassed = 0;
	private bool damaged = false;
	private float knockbackDist;
	public float maxKnockbackDist = 3;
	public float knockbackFactor = 5f;
	private Vector3 knockbackPos;
	public float stunTime = 0.5f;
	private bool stunned = false;
	private float stunTimePassed = 0;
	private Vector3 velocity;
	private Vector3 currentPos;
	private GameObject link;
	public GameObject heartPrefab;

	// Use this for initialization
	void Start () {
		link = GameObject.Find("Link");
		knockbackDist = maxKnockbackDist;
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

	void FixedUpdate()
	{
		if (knockbackDist >= maxKnockbackDist && !stunned)
		{
			if (Vector3.Distance(currentPos, transform.position) >= 1)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
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

		else if (knockbackDist < maxKnockbackDist && coll.gameObject.tag == "Pit") {
			takeDamage(100);
		}

		else if (coll.gameObject.tag == "Pit" || coll.gameObject.tag == "EnemyWall" || coll.gameObject.tag == "block" || coll.gameObject.tag == "Wall" || coll.gameObject.tag == "Lock" || coll.gameObject.tag == "UpDoor" || coll.gameObject.tag == "RightDoor" || coll.gameObject.tag == "LeftDoor" || coll.gameObject.tag == "DownDoor")
    {
			if (knockbackDist < maxKnockbackDist)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				knockbackDist = maxKnockbackDist;
				alignWithGrid();
				changeDirection();
			}
			else
			{
				alignWithGrid();
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				changeDirection();
			}
    }
  }

	void takeDamage(int damage, GameObject coll = null) {
		currentHealth -= damage;
		if (currentHealth <= 0)
		{
			GameObject door = GameObject.Find("ChallengeBlockedDoor");
			door.GetComponent<SpriteRenderer>().sortingOrder = -1;
			door.GetComponent<BoxCollider>().isTrigger = true;
			Instantiate(heartPrefab, transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
		else
		{
			damaged = true;
			damageTimePassed = 0;
			char dir;

			if (coll != null)
			{
				dir = findDirection(coll);
				if (dir == 'n')
				{
					knockbackDist = 0;
					knockbackPos = transform.position;
					GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * knockbackFactor;
				}
				else if (dir == 's')
				{
					knockbackDist = 0;
					knockbackPos = transform.position;
					GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * knockbackFactor;
				}
				else if (dir == 'e')
				{
					knockbackDist = 0;
					knockbackPos = transform.position;
					GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * knockbackFactor;
				}
				else if (dir == 'w')
				{
					knockbackDist = 0;
					knockbackPos = transform.position;
					GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * knockbackFactor;
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
		float xDist = link.transform.position.x - transform.position.x;
		float yDist = link.transform.position.y - transform.position.y;

		if (Mathf.Abs(xDist) > Mathf.Abs(yDist))
		{
			if (xDist < 0)
			{
				currentPos = transform.position;
				GetComponent<Rigidbody>().velocity = new Vector3(-velocityFactor, 0, 0);
			}
			else
			{
				currentPos = transform.position;
				GetComponent<Rigidbody>().velocity = new Vector3(velocityFactor, 0, 0);
			}
		}
		else
		{
			if (yDist < 0)
			{
				currentPos = transform.position;
				GetComponent<Rigidbody>().velocity = new Vector3(0, -velocityFactor, 0);
			}
			else
			{
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
}