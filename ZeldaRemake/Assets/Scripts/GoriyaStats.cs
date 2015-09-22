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

    // Use this for initialization
    void Start()
    {
        direction = Random.value;
        changeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= timeBetweenThrows)
        {
            throwBoomerang();
            timePassed = 0;
        }
    }

    void FixedUpdate()
    {
        if (!throwing && Random.value < chanceToChangeDirection)
        {
            direction = Random.value;
            changeDirection();
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
        
        else if (coll.gameObject  == boomerangInstance && throwing && !boomerangLeaving)
        {
            Destroy(boomerangInstance);
            throwing = false;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "GoriyaBoomerang")
        {
            boomerangLeaving = false;
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
        boomerangInstance = Instantiate(boomerangPrefab, transform.position, Quaternion.identity) as GameObject;
        BoomerangController controller = (BoomerangController)boomerangInstance.GetComponent(typeof(BoomerangController));
        controller.setDirection(dirChar);
    }

}