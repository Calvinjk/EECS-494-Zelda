using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChallengeRoomKey : MonoBehaviour {
	public GameObject enemyPrefab;
	public List<Vector3> pitPos;
	public GameObject pitPrefab;
	public Vector3 enemyPos;
	private GameObject door;

	// Use this for initialization
	void Start () {
		door = GameObject.Find("ChallengeBlockedDoor");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Link") {
			startChallenge();
		}
	}

	public void startChallenge() {

		door.GetComponent<SpriteRenderer>().sortingOrder = 2;
		door.GetComponent<BoxCollider>().isTrigger = false;
		Instantiate(enemyPrefab, enemyPos, Quaternion.identity);

		foreach (Vector3 pos in pitPos) {
			Instantiate(pitPrefab, pos, Quaternion.identity);
		}
    }



}
