using UnityEngine;
using System.Collections;

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


	// Use this for initialization
	void Start () {
		knockbackDist = maxKnockbackDist;
		direction = Random.value;
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
			}
		}
	}

  void FixedUpdate()
  {
    if (Random.value < chanceToChangeDirection)
    {
      direction = Random.value;
      changeDirection();
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

		else if (coll.gameObject.tag == "block" || coll.gameObject.tag == "Lock" || coll.gameObject.tag == "UpDoor" || coll.gameObject.tag == "RightDoor" || coll.gameObject.tag == "LeftDoor" || coll.gameObject.tag == "DownDoor")
    {
      direction = (direction + 0.25f) % 1;
      changeDirection();
    }
  }

	public void takeDamage(int damage, GameObject coll = null)
	{
		currentHealth -= damage;
		GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
		damaged = true;
		damageTimePassed = 0;
		if (currentHealth <= 0)
		{
			BossRoom script = (BossRoom)room.GetComponent(typeof(BossRoom));
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

	void changeDirection()
	{
		if (direction < 0.25)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * velocityFactor;
			dirChar = 'n';
		}
		else if (direction < 0.5)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * velocityFactor;
			dirChar = 'e';
		}
		else if (direction < 0.75)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * velocityFactor;
			dirChar = 's';
		}
		else if (direction <= 1)
		{
			GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * velocityFactor;
			dirChar = 'w';
		}
	}
}
