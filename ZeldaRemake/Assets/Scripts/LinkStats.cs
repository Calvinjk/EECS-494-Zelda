using UnityEngine;
using System.Collections;

public class LinkStats : MonoBehaviour {
  public int maxHealth = 6;
  public int currentHealth = 6;
  public int numRupees = 0;
  public int numKeys = 0;
  public int numBombs = 5;
  public bool invincible = false;
  public float invincDuration = 1;
  private float duration;
  public bool hasBoomerang = false;
  public bool hasBow = false;
	private LinkMovement movement;

	// Use this for initialization
	void Start () {
		movement = (LinkMovement) GetComponent(typeof(LinkMovement));
	}
	
	// Update is called once per frame
	void Update () {
		if (invincible) {
			duration -= Time.deltaTime;
			if (duration <= 0) {
				invincible = false;
				duration = 0;
				GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);

			}
		}

    if (Input.GetKeyDown(KeyCode.I)){
      invincible = !invincible;
			if (invincible)
				duration = 10000000;
			else duration = 0;
		}
  }

  void OnCollisionEnter(Collision coll)
  {
    if (coll.gameObject.tag == "Rupee")
    {
      numRupees++;
      Destroy(coll.gameObject);
    }
		else if (coll.gameObject.tag == "BigRupee") {
			numRupees += 5;
			Destroy(coll.gameObject);
		}
		else if (coll.gameObject.tag == "Heart")
		{
			currentHealth += 2;
			if (currentHealth > maxHealth)
				currentHealth = maxHealth;
			Destroy(coll.gameObject);
		}
		else if (coll.gameObject.tag == "BigHeart")
		{
			currentHealth = maxHealth;
			Destroy(coll.gameObject);
		}
		else if (coll.gameObject.tag == "BombItem")
		{
			numBombs += 4;
			Destroy(coll.gameObject);
		}
		else if (coll.gameObject.tag == "Triforce") {
			transform.position = new Vector3(39.5f, 1.5f, 0f);
			Camera.main.transform.position = new Vector3(39.5f, 5f, -8f);
		}

		else if (coll.gameObject.tag == "Key")
    {
      numKeys++;
      Destroy(coll.gameObject);
    }

    else if (coll.gameObject.tag == "BoomerangReward") {
      hasBoomerang = true;
      Destroy(coll.gameObject);
    }

    else if (coll.gameObject.tag == "Lock")
    {
      if (numKeys > 0)
      {
        numKeys--;
        coll.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        SpriteRenderer[] sprites = coll.gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = 1;
        }
      }
    }
  }

  void OnTriggerEnter(Collider coll)
  {
    if (coll.gameObject.tag == "Stalfos" || coll.gameObject.tag == "Gel" || coll.gameObject.tag == "Keese" || coll.gameObject.tag == "Goriya")
    {
      takeDamage(1);
    }
		else if (coll.gameObject.tag == "GoriyaBoomerang") {
			BoomerangController script = (BoomerangController)coll.gameObject.GetComponent(typeof(BoomerangController));
			if (script.getDirection() != movement.currentDir) {
				takeDamage(1);
			}
		}
		else if (coll.gameObject.tag == "BossAttack")
		{
			takeDamage(2);
		}
		else if (coll.gameObject.tag == "BladeTrap") {
			takeDamage(2);
		}
		else if (coll.gameObject.tag == "WallMaster") {
			if (!invincible)
			{
				transform.position = new Vector3(39.5f, 1.5f, 0f);
				Camera.main.transform.position = new Vector3(39.5f, 5f, -8f);
			}
		}
		else if (coll.gameObject.tag == "Boomerang")
		{
			Destroy(coll.gameObject);
		}
	}

  void takeDamage(int damage)
  {
    if (!invincible)
    {
      LinkMovement moveScript = GetComponent<LinkMovement>();
      moveScript.knockBack();
      currentHealth -= damage;
			invincible = true;
			duration = invincDuration;
			GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
      if (currentHealth == 0)
      {
				Application.LoadLevel(Application.loadedLevelName);
      }

    }
  }
}
