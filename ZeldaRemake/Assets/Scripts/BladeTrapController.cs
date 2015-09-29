using UnityEngine;
using System.Collections;

public class BladeTrapController : MonoBehaviour {
	private bool isTriggered = false;
	public float towardFactor = 3.0f;
	public float awayFactor = -1.0f;
	public float yDist = 3.25f;
	public float xDist = 5.75f;
	private bool reversed = false;
	private float timePassed = 0f;
	private char direction;
	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTriggered && !reversed) {
			timePassed += Time.deltaTime;

			if ((direction == 'n' || direction == 's') && timePassed * towardFactor >= yDist) {
				setMovement(direction, awayFactor);
				reversed = true;
				timePassed = 0;
			}
			else if ((direction == 'e' || direction == 'w') && timePassed * towardFactor >= xDist)
			{
				setMovement(direction, awayFactor);
				reversed = true;
				timePassed = 0;
			}
		}
		else if (isTriggered && reversed) {
			timePassed += Time.deltaTime;
			if ((direction == 'n' || direction == 's') && timePassed * awayFactor <= -yDist)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				reversed = false;
				isTriggered = false;
				timePassed = 0;
				transform.position = startPos;
			}
			else if ((direction == 'e' || direction == 'w') && timePassed * awayFactor <= -xDist)
			{
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				reversed = false;
				isTriggered = false;
				timePassed = 0;
				transform.position = startPos;
			}
		}
	}

	public void triggeredLink(char dir) {
		if (!isTriggered) {
			isTriggered = true;
			setMovement(dir, towardFactor);
    }
	}

	void setMovement(char dir, float factor) {
		direction = dir;
		if (dir == 'n')
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0) * factor;
		}
		else if (dir == 'e')
		{
			GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0) * factor;
		}
		else if (dir == 's')
		{
			GetComponent<Rigidbody>().velocity = new Vector3(0, -1, 0) * factor;
		}
		else if (dir == 'w')
		{
			GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0) * factor;
		}
	}

}
