using UnityEngine;
using System.Collections;
using System;

public class LinkMovement : MonoBehaviour {

  public float velocityFactor = 1.0f;
  public GameObject swordPrefab;
	public GameObject thrownSwordPrefab;  
  public GameObject boomPrefab;
  public GameObject bowPrefab;
  public GameObject arrowPrefab;
  public float boomerangSpeed = 1.0f;
  public float boomerangReturnSpeed = 1.0f;
  public float boomerangRotationSpeed = 1.0f;
  public float maxBoomerangDist = 1.0f;
  public float arrowSpeed = 1.0f;
  public int swordCooldown = 30;
  public int maxCooldown = 15;
  public int bowCooldown = 45;
	public float maxKnockback = 10f;
	public float knockbackFactor = 1.5f;
	public float thrownSwordSpeed = 5f;
	public bool ______________________;
  public char currentDir = 's';
  private float knockbackDistance = 0f;
  public GameObject swordInstance;
  public GameObject boomInstance;
  public GameObject bowInstance;
  public GameObject arrowInstance;
	public GameObject thrownSword;
  public Vector3 boomStart;
  public bool boomerangReturning = false;
	public GameObject bombPrefab;
	public char itemB = 's';
	public char itemA;
	private GameObject bombInstance;

  LinkStats linkStats;

  // Use this for initialization
  void Start () {
    linkStats = (LinkStats)GetComponent(typeof(LinkStats));
	}
	
	// Update is called once per frame
	void Update () {
    float horizontalInput = Input.GetAxis("Horizontal");
    float verticalInput = Input.GetAxis("Vertical");

    if (swordCooldown > 0 || knockbackDistance > 0 || bowCooldown > 0)
    {
      horizontalInput = 0.0f;
      verticalInput = 0.0f;
    }


    //link should not be able to move diagonally
    if (horizontalInput != 0.0f)
        verticalInput = 0.0f;

    // Grid movement variables
    Vector3  newPos  = transform.position;
    float   xOffset = newPos.x % 0.5f;
    float   yOffset = newPos.y % 0.5f;

    // Grid movement currently only works for positive X and Y positions
    if (knockbackDistance <= 0) {
      if (horizontalInput > 0) {
        double deci = newPos.y - Math.Truncate(newPos.y);
        if (currentDir == 'n' || currentDir == 's') {  
            if ((deci < 0.75 && deci > 0.5) || (deci < 0.25 && deci > 0)){
                newPos.y -= yOffset;
            } else {
                newPos.y += (0.5f - yOffset);
            }
        }
        transform.position = newPos;
        currentDir = 'e';
      }
      else if (horizontalInput < 0) {
        double deci = newPos.y - Math.Truncate(newPos.y);
        if (currentDir == 'n' || currentDir == 's') {
            if ((deci < 0.75 && deci > 0.5) || (deci < 0.25 && deci > 0)) {
                newPos.y -= yOffset;
            } else {
                newPos.y += (0.5f - yOffset);
            }
        }
        transform.position = newPos;
        currentDir = 'w';
      }
      else if (verticalInput > 0) {
        double deci = newPos.x - Math.Truncate(newPos.x);
        if (currentDir == 'e' || currentDir == 'w') {
            if ((deci < 0.75 && deci > 0.5) || (deci < 0.25 && deci > 0)) {
                newPos.x -= xOffset;
            } else {
                newPos.x += (0.5f - xOffset);
            }
        }
        transform.position = newPos;
        currentDir = 'n';
      }
      else if (verticalInput < 0) {
        double deci = newPos.x - Math.Truncate(newPos.x);
        if (currentDir == 'e' || currentDir == 'w') {
            if ((deci < 0.75 && deci > 0.5) || (deci < 0.25 && deci > 0)) {
                newPos.x -= xOffset;
            } else {
                newPos.x += (0.5f - xOffset);
            }
        }
        transform.position = newPos;
        currentDir = 's';
      }
    }

    if (knockbackDistance > 0) {
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

    // Return boomerang
    if (boomInstance != null && Vector3.Distance(boomInstance.transform.position, boomStart) >= maxBoomerangDist) {
      boomerangReturning = true;
    }

    if (boomInstance != null && boomerangReturning) {
      if(Vector3.Distance(boomInstance.transform.position, transform.position) < 0.4) {
          Destroy(boomInstance);
          boomerangReturning = false;
      }
      // Direction vector
      Vector3 dir = transform.position - boomInstance.transform.position;
      dir.Normalize();  //Make dir a unit vector
      boomInstance.transform.position += dir * boomerangReturnSpeed;
    }

    Combat();
  }

  void Combat()
  {
		if (swordCooldown > 0 && knockbackDistance <= 0)
			swordCooldown--;
		else if (swordInstance != null){
			Destroy(swordInstance);
			if (linkStats.currentHealth == linkStats.maxHealth && thrownSword == null)
				throwSword();
		}

    if (Input.GetKeyDown(KeyCode.S))
    {
			if (itemB == 's')
				swordAttack();

			else if (itemB == 'b')
				boomerangAttack();

			else if (itemB == 'w')
			{
				bowAttack();
			}
			else if (itemB == 'm')
			{
				bombAttack();
			}
		}

		if (Input.GetKeyDown(KeyCode.A)) {
			if (itemA == 's')
				swordAttack();

			else if (itemA == 'b')
				boomerangAttack();

			else if (itemA == 'w') {
				bowAttack();
			}
			else if (itemA == 'm') {
				bombAttack();
			}

		}

    if (bowCooldown > 0)
        bowCooldown--;
    else if (bowInstance != null)
        Destroy(bowInstance);
	}

	public void throwSword() {
		thrownSword = Instantiate(thrownSwordPrefab, transform.position, Quaternion.identity) as GameObject;

		if (currentDir == 'n'){
			thrownSword.transform.position += new Vector3(0, 1, 0);
			thrownSword.GetComponent<Rigidbody>().velocity = new Vector3(0, thrownSwordSpeed, 0);
		}

		else if (currentDir == 'e')
		{
			thrownSword.transform.position += new Vector3(1, 0, 0);
			thrownSword.transform.Rotate(new Vector3(0, 0, 1), 270);
			thrownSword.GetComponent<Rigidbody>().velocity = new Vector3(thrownSwordSpeed, 0, 0);
		}
		else if (currentDir == 'w')
		{
			thrownSword.transform.position += new Vector3(-1, 0, 0);
			thrownSword.transform.Rotate(new Vector3(0, 0, 1), 90);
			thrownSword.GetComponent<Rigidbody>().velocity = new Vector3(-thrownSwordSpeed, 0, 0);
		}
		else if (currentDir == 's')
		{
			thrownSword.transform.position += new Vector3(0, -1, 0);
			thrownSword.transform.Rotate(new Vector3(0, 0, 1), 180);
			thrownSword.GetComponent<Rigidbody>().velocity = new Vector3(0, -thrownSwordSpeed, 0);
		}
		thrownSword.GetComponent<WeaponController>().setDirection(currentDir);
	}

	public void knockBack()
  {
    knockbackDistance = maxKnockback;
		if (swordInstance)
			Destroy(swordInstance);
  }

	void swordAttack() {
		if (bombInstance == null && swordInstance == null && bowInstance == null && boomInstance == null) {
			swordInstance = Instantiate(swordPrefab, transform.position, Quaternion.identity) as GameObject;
			swordCooldown = maxCooldown;

			if (currentDir == 'n')
			{
				swordInstance.transform.position += new Vector3(0, 1, 0);
			}
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
			swordInstance.GetComponent<WeaponController>().setDirection(currentDir);
		}
	}

	void boomerangAttack() {
		if (bombInstance == null && swordInstance == null && thrownSword == null && bowInstance == null && boomInstance == null) {
			boomInstance = Instantiate(boomPrefab, transform.position, Quaternion.identity) as GameObject;
			boomInstance.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, boomerangRotationSpeed);
			boomStart = transform.position;

			if (currentDir == 'n')
			{
				boomInstance.transform.position += new Vector3(0, 1, 0);
				boomInstance.GetComponent<Rigidbody>().velocity = new Vector3(0, 1 * boomerangSpeed, 0);
			}
			else if (currentDir == 'e')
			{
				boomInstance.transform.position += new Vector3(1, 0, 0);
				boomInstance.transform.Rotate(new Vector3(0, 0, 1), 270);
				boomInstance.GetComponent<Rigidbody>().velocity = new Vector3(1 * boomerangSpeed, 0, 0);
			}
			else if (currentDir == 'w')
			{
				boomInstance.transform.position += new Vector3(-1, 0, 0);
				boomInstance.transform.Rotate(new Vector3(0, 0, 1), 90);
				boomInstance.GetComponent<Rigidbody>().velocity = new Vector3(-1 * boomerangSpeed, 0, 0);
			}
			else if (currentDir == 's')
			{
				boomInstance.transform.position += new Vector3(0, -1, 0);
				boomInstance.transform.Rotate(new Vector3(0, 0, 1), 180);
				boomInstance.GetComponent<Rigidbody>().velocity = new Vector3(0, -1 * boomerangSpeed, 0);
			}
		}
	}

	void bowAttack() {
		if (bombInstance == null && swordInstance == null && thrownSword == null && bowInstance == null && boomInstance == null) {
			bowInstance = Instantiate(bowPrefab, transform.position, Quaternion.identity) as GameObject;
			arrowInstance = Instantiate(arrowPrefab, transform.position, Quaternion.identity) as GameObject;
			bowCooldown = maxCooldown;

			if (currentDir == 'n')
			{
				bowInstance.transform.position += new Vector3(0, .6f, 0);
				bowInstance.transform.Rotate(new Vector3(0, 0, 1), 90);

				arrowInstance.transform.position += new Vector3(0, 1f, 0);
				arrowInstance.transform.Rotate(new Vector3(0, 0, 1), 90);
				arrowInstance.GetComponent<Rigidbody>().velocity = new Vector3(0, arrowSpeed, 0);
			}
			else if (currentDir == 'e')
			{
				bowInstance.transform.position += new Vector3(.6f, 0, 0);

				arrowInstance.transform.position += new Vector3(1f, 0, 0);
				arrowInstance.GetComponent<Rigidbody>().velocity = new Vector3(arrowSpeed, 0, 0);
			}
			else if (currentDir == 'w')
			{
				bowInstance.transform.position += new Vector3(-.6f, 0, 0);
				bowInstance.transform.Rotate(new Vector3(0, 0, 1), 180);

				arrowInstance.transform.position += new Vector3(-1f, 0, 0);
				arrowInstance.transform.Rotate(new Vector3(0, 0, 1), 180);
				arrowInstance.GetComponent<Rigidbody>().velocity = new Vector3(-arrowSpeed, 0, 0);
			}
			else if (currentDir == 's')
			{
				bowInstance.transform.position += new Vector3(0, -.6f, 0);
				bowInstance.transform.Rotate(new Vector3(0, 0, 1), 270);

				arrowInstance.transform.position += new Vector3(0, -1f, 0);
				arrowInstance.transform.Rotate(new Vector3(0, 0, 1), 270);
				arrowInstance.GetComponent<Rigidbody>().velocity = new Vector3(0, -arrowSpeed, 0);
			}
		}
	}

	void bombAttack()
	{
		if (bombInstance == null && swordInstance == null && thrownSword == null && bowInstance == null && boomInstance == null && linkStats.numBombs > 0)
		{
			bombInstance = Instantiate(bombPrefab, transform.position, Quaternion.identity) as GameObject;
			linkStats.numBombs--;
		}
	}  
}
