using UnityEngine;
using System.Collections;

public class LinkStats : MonoBehaviour {
    public int maxHealth = 6;
    public int currentHealth = 6;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Rupee")
        {
            Destroy(coll.gameObject);
        }

        else if (coll.gameObject.tag == "Stalfos")
        {
            currentHealth--;
            LinkMovement moveScript = GetComponent<LinkMovement>();
            moveScript.knockBack();
            if (currentHealth == 0)
            {
                Application.LoadLevel("Dungeon 2");
            }
        }
    }
}
