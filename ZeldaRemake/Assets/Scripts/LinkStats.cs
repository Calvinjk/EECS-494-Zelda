using UnityEngine;
using System.Collections;

public class LinkStats : MonoBehaviour {
    public int maxHealth = 6;
    public int currentHealth = 6;
    public int numRupees = 0;
    public int numKeys = 0;
    public bool invincible = false;
    public float invicibilityDuration = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I)){
            invincible = !invincible;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Rupee")
        {
            numRupees++;
            Destroy(coll.gameObject);
        }

        else if (coll.gameObject.tag == "Key")
        {
            numKeys++;
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
                    sprite.sortingOrder = 2;
                }
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Stalfos" || coll.gameObject.tag == "Gel" || coll.gameObject.tag == "Goriya" || coll.gameObject.tag == "GoriyaBoomerang")
        {
            takeDamage(1);
        }
				if (coll.gameObject.tag == "BladeTrap") {
					takeDamage(2);
				}
    }

    void takeDamage(int damage)
    {
        if (!invincible)
        {
            LinkMovement moveScript = GetComponent<LinkMovement>();
            moveScript.knockBack();
            currentHealth -= damage;
            if (currentHealth == 0)
            {
                Application.LoadLevel(Application.loadedLevelName);
            }

        }
    }

}
