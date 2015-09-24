using UnityEngine;
using System.Collections;

public class StalfosStats : EnemyStats {
  public int maxHealth = 3;
  public int currentHealth = 3;
  public float velocityFactor = 1.0f;
  public float chanceToChangeDirection = 0.02f;
  private float direction;
	public float damageTime = 0.5f;
	private float damageTimePassed = 0;
	private bool damaged = false;

	// Use this for initialization
	void Start () {
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
			takeDamage(1);
    }

		if (coll.gameObject.tag == "Bomb")
		{
			takeDamage(2);
		}

		else if (coll.gameObject.tag == "block" || coll.gameObject.tag == "Lock" || coll.gameObject.tag == "UpDoor" || coll.gameObject.tag == "RightDoor" || coll.gameObject.tag == "LeftDoor" || coll.gameObject.tag == "DownDoor")
    {
      direction = (direction + 0.25f) % 1;
      changeDirection();
    }
  }

	void takeDamage(int damage) {
		currentHealth -= damage;
		GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
		damaged = true;
		damageTimePassed = 0;
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
}
