using UnityEngine;
using System.Collections;

public class StalfosStats : MonoBehaviour {
    public int maxHealth = 3;
    public int currentHealth = 3;
    public float velocityFactor = 1.0f;
    public float chanceToChangeDirection = 0.02f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (Random.value < chanceToChangeDirection)
        {
            changeDirection();
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Sword")
        {
            currentHealth--;
            if (currentHealth == 0)
            {
                Destroy(this.gameObject);
            }
        }
        else if (coll.gameObject.tag == "block")
        {
            changeDirection();
        }
    }

    void changeDirection()
    {
        float newDirection = Random.value;
        if (newDirection < 0.25)
            GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * velocityFactor;
        else if (newDirection < 0.5)
            GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * velocityFactor;
        else if (newDirection < 0.75)
            GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * velocityFactor;
        else if (newDirection <= 1)
            GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * velocityFactor;
    }
}
