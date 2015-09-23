using UnityEngine;
using System.Collections;

public class GoriyaStats : EnemyStats
{
    public int maxHealth = 3;
    public int currentHealth = 3;
    public float velocityFactor = 1.0f;
    public float chanceToChangeDirection = 0.02f;
    private float direction;
    private char dirChar;
    private GameObject boomerangInstance;
    public GameObject boomerangPrefab;
    private float timePassed = 0f;
    private bool throwing = false;
    public float timeBetweenThrows = 5f;
    private bool boomerangLeaving = false;
    public float boomerangSpeed = 1.0f;
    public float boomerangReturnSpeed = 1.0f;
    public float boomerangRotationSpeed = 1.0f;
    public float maxBoomerangDistance = 5.0f;
    
    // Use this for initialization
    void Start()
    {
        direction = Random.value;
        changeDirection();
    }

    void Update() {
        timePassed += Time.deltaTime;
        if (timePassed >= timeBetweenThrows) {
            throwBoomerang();
            timePassed = 0;
        }
    }

    void FixedUpdate() {
        if (!throwing && Random.value < chanceToChangeDirection)
        {
            direction = Random.value;
            changeDirection();
        }

        //Boomerang Logic
        if (throwing) {
            if (boomerangInstance == null) {
                boomerangInstance = Instantiate(boomerangPrefab, transform.position, Quaternion.identity) as GameObject;
                boomerangInstance.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, boomerangRotationSpeed); //Gives boomerangs rotation

                switch(dirChar) {
                    case 'n':
                        boomerangInstance.transform.position += new Vector3(0, 1, 0);
                        boomerangInstance.GetComponent<Rigidbody>().velocity = new Vector3(0, 1 * boomerangSpeed, 0);
                        break;
                    case 'e':
                        boomerangInstance.transform.position += new Vector3(1, 0, 0);
                        boomerangInstance.GetComponent<Rigidbody>().velocity = new Vector3(1 * boomerangSpeed, 0, 0);
                        break;
                    case 'w':
                        boomerangInstance.transform.position += new Vector3(-1, 0, 0);
                        boomerangInstance.GetComponent<Rigidbody>().velocity = new Vector3(-1 * boomerangSpeed, 0, 0);
                        break;
                    case 's':
                        boomerangInstance.transform.position += new Vector3(0, -1, 0);
                        boomerangInstance.GetComponent<Rigidbody>().velocity = new Vector3(0, -1 * boomerangSpeed, 0);
                        break;
                }
            }

            // Return boomerang
            if (boomerangInstance != null && Vector3.Distance(boomerangInstance.transform.position, transform.position) >= maxBoomerangDistance) {
                boomerangLeaving = false;
            }

            if (boomerangInstance != null && !boomerangLeaving) {
                if (Vector3.Distance(boomerangInstance.transform.position, transform.position) < 0.4) {
                    Destroy(boomerangInstance);
                    throwing = false;
                    timePassed = 0;
                }
                //Direction vector
                Vector3 dir = transform.position - boomerangInstance.transform.position;
                dir.Normalize();
                boomerangInstance.transform.position += dir * boomerangReturnSpeed;
            }
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Sword" || coll.gameObject.tag == "Arrow")
        {
            currentHealth--;
            if (currentHealth == 0)
            {
                RoomManager script = (RoomManager)room.GetComponent(typeof(RoomManager));
                script.killedEnemy(this.gameObject);
            }
        }
				if (coll.gameObject.tag == "Bomb")
				{
					currentHealth-=2;
					if (currentHealth <= 0)
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

    void throwBoomerang()
    {
        throwing = true;
        boomerangLeaving = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        /*
        boomerangInstance = Instantiate(boomerangPrefab, transform.position, Quaternion.identity) as GameObject;
        BoomerangController controller = (BoomerangController)boomerangInstance.GetComponent(typeof(BoomerangController));
        controller.setDirection(dirChar);
        */
    }

}