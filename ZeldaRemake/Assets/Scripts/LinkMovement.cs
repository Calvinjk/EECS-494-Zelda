using UnityEngine;
using System.Collections;

public class LinkMovement : MonoBehaviour {

    public float velocityFactor = 1.0f;
    public GameObject swordPrefab;
    public GameObject swordInstance;
    public int swordCooldown = 30;
    public int maxCooldown = 15;
    public char currentDir = 's';

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (swordCooldown > 0)
        {
            horizontalInput = 0.0f;
            verticalInput = 0.0f;
        }

        //link should not be able to move diagonally
        if (horizontalInput != 0.0f)
            verticalInput = 0.0f;

        if (horizontalInput > 0)
            currentDir = 'e';
        else if (horizontalInput < 0)
            currentDir = 'w';
        else if (verticalInput > 0)
            currentDir = 'n';
        else if (verticalInput < 0)
            currentDir = 's';

        Vector3 newVelocity = new Vector3(horizontalInput, verticalInput, 0);
        GetComponent<Rigidbody>().velocity = newVelocity * velocityFactor;

        //animate
        GetComponent<Animator>().SetFloat("vertical_vel", newVelocity.y);
        GetComponent<Animator>().SetFloat("horizontal_vel", newVelocity.x);

        /*
        if (newVelocity.magnitude == 0)
            GetComponent<Animator>().speed = 0.00000001f;
        else
            GetComponent<Animator>().speed = 1.0f;
            */

        Combat();
    }

    void Combat()
    {
        if (swordCooldown > 0)
            swordCooldown--;
        else if (swordInstance != null)
            Destroy(swordInstance);

        if (Input.GetKeyDown(KeyCode.Space) && swordInstance == null)
        {
            swordInstance = Instantiate(swordPrefab, transform.position, Quaternion.identity) as GameObject;
            swordCooldown = maxCooldown;

            if (currentDir == 'n')
                swordInstance.transform.position += new Vector3(0, 1, 0);
            else if (currentDir == 'e')
            {
                swordInstance.transform.position += new Vector3(1, 0, 0);
                swordInstance.transform.Rotate(new Vector3(0, 0, 1), 270);
            }
            else if (currentDir == 'w')
            {
                swordInstance.transform.position += new Vector3(-1, 0, 0);
                swordInstance.transform.Rotate(new Vector3(0, 0, 1), 90);
            }
            else if (currentDir == 's')
            {
                swordInstance.transform.position += new Vector3(0, -1, 0);
                swordInstance.transform.Rotate(new Vector3(0, 0, 1), 180);
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Rupee")
        {
            Destroy(coll.gameObject);
        }
    }
}
