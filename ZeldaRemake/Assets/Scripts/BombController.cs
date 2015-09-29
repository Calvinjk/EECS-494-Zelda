using UnityEngine;
using System.Collections;
using System;

public class BombController : MonoBehaviour {
	public float fuseTimer = 1f;
	public float explosionTimer = 1f;
	private float timePassed = 0f;
	private bool detonated = false;

	// Use this for initialization
	void Start () {
		alignWithGrid();
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed >= fuseTimer && !detonated) {
			timePassed = 0;
			GetComponent<BoxCollider>().enabled = true;
			detonated = true;
			Component[] fires = GetComponentsInChildren<SpriteRenderer>();
			foreach (SpriteRenderer fire in fires) {
				fire.enabled = true;
			}
			GetComponent<SpriteRenderer>().enabled = false;
		}
		else if (timePassed >= explosionTimer && detonated)
		{
			Destroy(this.gameObject);
		}
	}

	void alignWithGrid()
	{
		Vector3 newPos = transform.position;
		float xOffset = newPos.x % 1f;
		float yOffset = newPos.y % 1f;
		double deciY = newPos.y - Math.Truncate(newPos.y);
		double deciX = newPos.x - Math.Truncate(newPos.x);
		if (deciY <= 0.5)
		{
			newPos.y -= yOffset;
		}
		else
		{
			newPos.y += (1f - yOffset);
		}


		if (deciX <= 0.5)
		{
			newPos.x -= xOffset;
		}
		else
		{
			newPos.x += (1f - xOffset);
		}

		transform.position = newPos;
	}
}
