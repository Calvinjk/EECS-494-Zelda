using UnityEngine;
using System.Collections;

public class GelStats : EnemyStats {
    public int maxHealth = 1;
    public int currentHealth = 1;
    public float velocityFactor = 1.0f;
    public float chanceToChangeDirection = 0.02f;
    private float direction;
    public float movementPeriod = 0.5f;
    private float timePassed = 0f;
    private bool waiting = false;

    // Use this for initialization
    void Start () {
        direction = Random.value;
        changeDirection();
    }
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if (timePassed >= movementPeriod)
        {
            if (waiting)
            {

                if (Random.value < chanceToChangeDirection)
                {
                    direction = Random.value;
                }
                changeDirection();
                waiting = false;
            }
            else
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                waiting = true;
            }
            timePassed = 0;
        }
	}


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Sword")
        {
            currentHealth--;
            if (currentHealth == 0)
            {
                RoomManager script = (RoomManager)room.GetComponent(typeof(RoomManager));
                script.killedEnemy(this.gameObject);
            }
        }
        else if (coll.gameObject.tag == "block" || coll.gameObject.tag == "Lock" || coll.gameObject.tag == "UpDoor" || coll.gameObject.tag == "RightDoor" || coll.gameObject.tag == "LeftDoor" || coll.gameObject.tag == "DownDoor")
        {
            direction = (direction + 0.25f) % 1;
            changeDirection();
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
