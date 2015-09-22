using UnityEngine;
using System.Collections;
using System;

public class LinkMovement : MonoBehaviour {

    public float velocityFactor = 1.0f;
    public GameObject swordPrefab;
    public GameObject swordInstance;
    public int swordCooldown = 30;
    public int maxCooldown = 15;
    public char currentDir = 's';
    private float knockbackDistance = 0f;
    public float maxKnockback = 10f;
    public float knockbackFactor = 1.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (swordCooldown > 0 || knockbackDistance > 0)
        {
            horizontalInput = 0.0f;
            verticalInput = 0.0f;
        }


        //link should not be able to move diagonally
        if (horizontalInput != 0.0f)
            verticalInput = 0.0f;

        // Grid movement variables
        //Vector3  newPos  = transform.position;
        //float   xOffset = newPos.x % 1;
        //float   yOffset = newPos.y % 1;

        // Grid movement currently only works for positive X and Y positions
        if (knockbackDistance <= 0)
        {
            if (horizontalInput > 0)
            {
                /*
                if (currentDir == 'n' || currentDir == 's' && yOffset != 0 && yOffset != 0.5) {
                    double deci = newPos.y - Math.Truncate(newPos.y);
                    if ((deci < 0.75 && deci > 0.5) || (deci < 0.25 && deci > 0)){
                        newPos.y -= yOffset;
                    } else {
                        newPos.y += (1 - yOffset);
                    }
                }
                */
                currentDir = 'e';
            }
            else if (horizontalInput < 0)
            {
                currentDir = 'w';
            }
            else if (verticalInput > 0)
            {
                currentDir = 'n';
            }
            else if (verticalInput < 0)
            {
                currentDir = 's';
            }
        }

        if (knockbackDistance > 0)
        {
            if (currentDir == 'e')
                horizontalInput = -knockbackFactor;
            else if (currentDir == 'w')
                horizontalInput = knockbackFactor;
            else if (currentDir == 's')
                verticalInput = knockbackFactor;
            else if (currentDir == 'n')
                verticalInput = -knockbackFactor;
            knockbackDistance--;
        }

        Vector3 newVelocity = new Vector3(horizontalInput, verticalInput, 0);
        GetComponent<Rigidbody>().velocity = newVelocity * velocityFactor;

        //animate
        if (knockbackDistance <= 0)
        {
            GetComponent<Animator>().SetFloat("vertical_vel", newVelocity.y);
            GetComponent<Animator>().SetFloat("horizontal_vel", newVelocity.x);
        }
        
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    

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

        if (Input.GetKeyDown(KeyCode.S) && swordInstance == null)
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

    public void knockBack()
    {
        knockbackDistance = maxKnockback;
    }

    
}
