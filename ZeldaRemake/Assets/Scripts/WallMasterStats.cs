using UnityEngine;
using System.Collections;
using System;

public class WallMasterStats : EnemyStats {
  public int maxHealth = 3;
  public int currentHealth = 3;
  public float velocityFactor = 1.0f;
  public float chanceToChangeDirection = 0.02f;
  private float direction;
	public float damageTime = 0.5f;
	private float damageTimePassed = 0;
	private bool damaged = false;
	private float knockbackDist;
	public float maxKnockbackDist = 3;
	public float knockbackFactor = 5f;
	private Vector3 knockbackPos;
	private char dirChar;
	private bool invincible = false;
	public float stunTime = 0.5f;
	private bool stunned = false;
	private float stunTimePassed = 0;
	public char firstDir;
	public char secondDir;
	private Vector3 startPos;
	private int step = 0;
	private Vector3 prevVelocity;
	public GameObject heartPrefab;
	public GameObject rupeePrefab;
	public GameObject bigRupeePrefab;
	public GameObject bombItemPrefab;



	// Use this for initialization
	void Start () {
		knockbackDist = maxKnockbackDist;
		alignWithGrid();
		startPos = transform.position;
  }

	// Update is called once per frame
	void Update()
	{
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
		if (knockbackDist < maxKnockbackDist)
		{
			knockbackDist = Mathf.Abs(Vector3.Distance(knockbackPos, transform.position));
			if (knockbackDist >= maxKnockbackDist)
			{
				knockbackDist = maxKnockbackDist;
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Rigidbody>().velocity = prevVelocity;
			}
		}
		if (stunned)
		{
			stunTimePassed += Time.deltaTime;
			if (stunTimePassed >= stunTime)
			{
				stunned = false;
				stunTimePassed = 0;
				GetComponent<Rigidbody>().velocity = prevVelocity;
			}
		}
		else if (step == 0)
		{
			changeDirection(firstDir);
			++step;
		}
		else if (step == 1 && Vector3.Distance(transform.position, startPos) >= 1)
		{
			alignWithGrid();
			startPos = transform.position;
			changeDirection(secondDir);
			++step;
		}
		else if (step == 2 && Vector3.Distance(transform.position, startPos) >= 3)
		{
			alignWithGrid();
			startPos = transform.position;
			changeDirection(firstDir, -1);
			++step;
		}
		else if (step == 3 && Vector3.Distance(transform.position, startPos) >= 1)
		{
			WallMasterRoom script = (WallMasterRoom)room.GetComponent(typeof(WallMasterRoom));
			script.despawnEnemy(this.gameObject);
		}
	}

		void FixedUpdate()
  {

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
			prevVelocity = GetComponent<Rigidbody>().velocity;
			stunned = true;
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			Destroy(coll.gameObject);
		}
  }

	public void takeDamage(int damage, GameObject coll = null)
	{
		if (!invincible)
		{
			currentHealth -= damage;
			GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
			damaged = true;
			invincible = true;
			damageTimePassed = 0;
			if (currentHealth <= 0)
			{
				dropItem();
				WallMasterRoom script = (WallMasterRoom) room.GetComponent(typeof(WallMasterRoom));
				script.killedEnemy(this.gameObject);
			}
			char dir;

			if (coll != null)
			{
				dir = findDirection(coll);
				if (dir == 'n' && (dirChar == 'n' || dirChar == 's'))
				{
					knockbackDist = 0;
					knockbackPos = transform.position;
					prevVelocity = GetComponent<Rigidbody>().velocity;
					GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * knockbackFactor;
				}
				else if (dir == 's' && (dirChar == 'n' || dirChar == 's'))
				{
					knockbackDist = 0;
					knockbackPos = transform.position;
					prevVelocity = GetComponent<Rigidbody>().velocity;
					GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * knockbackFactor;
				}
				else if (dir == 'e' && (dirChar == 'e' || dirChar == 'w'))
				{
					knockbackDist = 0;
					knockbackPos = transform.position;
					prevVelocity = GetComponent<Rigidbody>().velocity;
					GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * knockbackFactor;
				}
				else if (dir == 'w' && (dirChar == 'e' || dirChar == 'w'))
				{
					knockbackDist = 0;
					knockbackPos = transform.position;
					prevVelocity = GetComponent<Rigidbody>().velocity;
					GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * knockbackFactor;
				}
			}
		}
	}

	char findDirection(GameObject coll)
	{
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

	void changeDirection(char dir, int reverse = 1)
	{
		if (dir == 'n')
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * velocityFactor * reverse;
			dirChar = 'n';
		}
		else if (dir == 'e')
		{
			GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * velocityFactor * reverse;
			dirChar = 'e';
		}
		else if (dir == 's')
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * velocityFactor * reverse;
			dirChar = 's';
		}
		else if (dir == 'w')
		{
			GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * velocityFactor * reverse;
			dirChar = 'w';
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

	void dropItem() {
		if (UnityEngine.Random.Range(0, 3) == 0)
		{
			int itemNum = UnityEngine.Random.Range(0, 3);
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
