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
  public GameObject HUD;
    LinkMovement linkMovement;

    // Use this for initialization

    void Start () {
        linkMovement = (LinkMovement)GetComponent(typeof(LinkMovement));

        HUD.transform.Find("RupeeCount1").GetComponent<UnityEngine.UI.Text>().text = "X" + numRupees.ToString();
        HUD.transform.Find("RupeeCount2").GetComponent<UnityEngine.UI.Text>().text = "X" + numRupees.ToString();
        HUD.transform.Find("KeyCount1").GetComponent<UnityEngine.UI.Text>().text = "X" + numKeys.ToString();
        HUD.transform.Find("KeyCount2").GetComponent<UnityEngine.UI.Text>().text = "X" + numKeys.ToString();
        HUD.transform.Find("BombCount1").GetComponent<UnityEngine.UI.Text>().text = "X" + numBombs.ToString();
        HUD.transform.Find("BombCount2").GetComponent<UnityEngine.UI.Text>().text = "X" + numBombs.ToString();
        HUD.transform.Find("HeartCount1").GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString() + "/" + maxHealth.ToString();
        HUD.transform.Find("HeartCount2").GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString() + "/" + maxHealth.ToString();
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

  void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.tag == "Rupee") {           
            numRupees++;
            HUD.transform.Find("RupeeCount1").GetComponent<UnityEngine.UI.Text>().text = "X" + numRupees.ToString();
            HUD.transform.Find("RupeeCount2").GetComponent<UnityEngine.UI.Text>().text = "X" + numRupees.ToString();
            Destroy(coll.gameObject);
        }
		else if (coll.gameObject.tag == "BigRupee") {
			numRupees += 5;
            HUD.transform.Find("RupeeCount1").GetComponent<UnityEngine.UI.Text>().text = "X" + numRupees.ToString();
            HUD.transform.Find("RupeeCount2").GetComponent<UnityEngine.UI.Text>().text = "X" + numRupees.ToString();
            Destroy(coll.gameObject);
		}
		else if (coll.gameObject.tag == "Heart")
		{
			currentHealth += 2;
			if (currentHealth > maxHealth)
				currentHealth = maxHealth;
            HUD.transform.Find("HeartCount1").GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString() + "/" + maxHealth.ToString();
            HUD.transform.Find("HeartCount2").GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString() + "/" + maxHealth.ToString();
            Destroy(coll.gameObject);
		}
		else if (coll.gameObject.tag == "BigHeart")
		{
			currentHealth = maxHealth;
            HUD.transform.Find("HeartCount1").GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString() + "/" + maxHealth.ToString();
            HUD.transform.Find("HeartCount2").GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString() + "/" + maxHealth.ToString();
            Destroy(coll.gameObject);
		}
		else if (coll.gameObject.tag == "BombItem")
		{
            numBombs += 4;
            HUD.transform.Find("BombCount1").GetComponent<UnityEngine.UI.Text>().text = "X" + numBombs.ToString();
            HUD.transform.Find("BombCount2").GetComponent<UnityEngine.UI.Text>().text = "X" + numBombs.ToString();
            Destroy(coll.gameObject);

            //If no secondary weapon is equipped, equip the bomb you just picked up
            if (linkMovement.itemB == "") {
                linkMovement.itemB = "bomb";
                HUD.transform.Find("BombSprite1").GetComponent<UnityEngine.UI.Image>().enabled = true;
            }
		}
		else if (coll.gameObject.tag == "Triforce") {
			transform.position = new Vector3(39.5f, 1.5f, 0f);
			Camera.main.transform.position = new Vector3(39.5f, 5f, -8f);
		}

		else if (coll.gameObject.tag == "Key") {
            numKeys++;
            HUD.transform.Find("KeyCount1").GetComponent<UnityEngine.UI.Text>().text = "X" + numKeys.ToString();
            HUD.transform.Find("KeyCount2").GetComponent<UnityEngine.UI.Text>().text = "X" + numKeys.ToString();
            Destroy(coll.gameObject);
        }

        else if (coll.gameObject.tag == "BoomerangReward") {
            hasBoomerang = true;
            Destroy(coll.gameObject);

            //If no secondary weapon is equipped, equip the boomerang you just picked up
            if (linkMovement.itemB == "")
            {
                linkMovement.itemB = "boom";
                HUD.transform.Find("BoomerangSprite1").GetComponent<UnityEngine.UI.Image>().enabled = true;
            }
        }

        else if (coll.gameObject.tag == "BowReward")
        {
            hasBow = true;
            Destroy(coll.gameObject);

            //If no secondary weapon is equipped, equip the bow you just picked up
            if (linkMovement.itemB == "")
            {
                linkMovement.itemB = "bow";
                HUD.transform.Find("BowSprite1").GetComponent<UnityEngine.UI.Image>().enabled = true;
            }
        }

        else if (coll.gameObject.tag == "Lock") {
          if (numKeys > 0) {
            numKeys--;
                HUD.transform.Find("KeyCount1").GetComponent<UnityEngine.UI.Text>().text = "X" + numKeys.ToString();
                HUD.transform.Find("KeyCount2").GetComponent<UnityEngine.UI.Text>().text = "X" + numKeys.ToString();
                coll.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            SpriteRenderer[] sprites = coll.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sprite in sprites) {
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
			if (script.getDirection() != linkMovement.currentDir) {
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
            HUD.transform.Find("HeartCount1").GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString() + "/" + maxHealth.ToString();
            HUD.transform.Find("HeartCount2").GetComponent<UnityEngine.UI.Text>().text = currentHealth.ToString() + "/" + maxHealth.ToString();
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
