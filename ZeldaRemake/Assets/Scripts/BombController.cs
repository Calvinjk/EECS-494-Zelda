using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {
	public float fuseTimer = 1f;
	public float explosionTimer = 1f;
	private float timePassed = 0f;
	private bool detonated = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timePassed += Time.deltaTime;
		if (timePassed >= fuseTimer && !detonated) {
			timePassed = 0;
			GetComponent<BoxCollider>().enabled = true;
			detonated = true;
		}
		else if (timePassed >= explosionTimer && detonated)
		{
			Destroy(this.gameObject);
		}
	}
}
